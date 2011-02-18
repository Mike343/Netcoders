#region License
//	Author: Mike Geise - http://www.netcoders.net
//	Copyright (c) 2010, Mike Geise
//
//	Licensed under the Apache License, Version 2.0 (the "License");
//	you may not use this file except in compliance with the License.
//	You may obtain a copy of the License at
//
//		http://www.apache.org/licenses/LICENSE-2.0
//
//	Unless required by applicable law or agreed to in writing, software
//	distributed under the License is distributed on an "AS IS" BASIS,
//	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//	See the License for the specific language governing permissions and
//	limitations under the License.
#endregion

#region Using Directives
using System;
using System.Web;
using Coders.Authentication;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Web.Authentication;
using Coders.Web.Models.Users;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace Coders.Web
{
	public class ApplicationSession
	{
		// private constants
		private const string SessionKey = "Falcon.Core.Web.ApplicationSession";

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationSession"/> class.
		/// </summary>
		public ApplicationSession()
			: this(
			ServiceLocator.Current.GetInstance<IUserRoleService>(),
			ServiceLocator.Current.GetInstance<IUserHostService>(),
			ServiceLocator.Current.GetInstance<IRepository<User>>())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationSession"/> class.
		/// </summary>
		/// <param name="userRoleService">The user role service.</param>
		/// <param name="userHostService">The user host service.</param>
		/// <param name="userRepository">The user repository.</param>
		public ApplicationSession(
			IUserRoleService userRoleService, 
			IUserHostService userHostService, 
			IRepository<User> userRepository)
		{
			this.UserRoleService = userRoleService;
			this.UserHostService = userHostService;
			this.UserRepository = userRepository;
		}

		/// <summary>
		/// Gets or sets the user role service.
		/// </summary>
		/// <value>The user role service.</value>
		public IUserRoleService UserRoleService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user host service.
		/// </summary>
		/// <value>The user host service.</value>
		public IUserHostService UserHostService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the user repository.
		/// </summary>
		public IRepository<User> UserRepository
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates the user session.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="principal">The principal.</param>
		/// <param name="identity">The identity.</param>
		public void Create(HttpContextBase context, PrivilegePrincipal principal, WebUserIdentity identity)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}

			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}

			if (context.Session == null)
			{
				throw new InvalidOperationException("The http context session is null");
			}

			identity.Session = context.Session[SessionKey] as UserSession;

			if (identity.Session == null)
			{
				identity.Session = this.Create(identity.Name);

				context.Session[SessionKey] = identity.Session;

				this.UserHostService.Capture();
			}

			principal.DetermineRolePrivileges();
		}

		/// <summary>
		/// Destroys the specified session.
		/// </summary>
		/// <param name="session">The session.</param>
		public static void Destroy(HttpSessionStateBase session)
		{
			if (session == null)
			{
				return;
			}

			session[SessionKey] = null;
			session.Abandon();
		}

		/// <summary>
		/// Creates the user session for the user with the specified email address.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		private UserSession Create(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return Guest();
			}

			var user = this.UserRepository.GetBy(new UserEmailAddressSpecification(address));

			if (user == null)
			{
				return Guest();
			}

			// user privileges
			var privileges = this.UserRoleService.GetPrivileges(new UserRoleRelationUserSpecification(user.Id));

			// user session
			var session = new UserSession(privileges)
			{
				Id = user.Id,
				Name = user.Name,
				Slug = user.Slug,
				TimeZone = user.Preference.TimeZone.Title,
				Dst = user.Preference.Dst,
				TimeFormat = user.Preference.TimeFormat
			};

			user.HostAddress = this.UserHostService.GetAddress();
			user.LastVisit = DateTime.Now;

			this.UserRepository.Update(user);

			return session;
		}

		/// <summary>
		/// Gets the guest user session.
		/// </summary>
		/// <returns></returns>
		private static UserSession Guest()
		{
			return new UserSession
			{
				Name = User.Guest,
				Slug = User.Guest.Slug(),
				TimeZone = TimeZoneInfo.Utc.Id,
				Dst = UserPreferenceDaylightSavingTime.Auto,
				TimeFormat = UserPreferenceTimeFormat.Relative,
			};
		}
	}
}