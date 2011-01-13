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
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Users;
#endregion

namespace Coders.Services
{
	public class UserBanService : EntityService<UserBan>, IUserBanService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="repository">The repository.</param>
		public UserBanService(
			IAuthenticationService authenticationService,
			IUserService userService,
			IRepository<UserBan> repository)
			: base(repository)
		{
			this.AuthenticationService = authenticationService;
			this.UserService = userService;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user service.
		/// </summary>
		/// <value>The user service.</value>
		public IUserService UserService
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns></returns>
		public override UserBan Create()
		{
			var ban = base.Create();

			ban.Created = DateTime.Now;

			return ban;
		}

		/// <summary>
		/// Checks if the current user is banned.
		/// </summary>
		/// <returns></returns>
		public UserBan Check()
		{
			var identity = this.AuthenticationService.Identity;

			return !identity.IsAuthenticated() ? null : this.GetBy(new UserBanUserSpecification(identity.Id));
		}

		/// <summary>
		/// Inserts or updates the specified user ban.
		/// </summary>
		/// <param name="ban">The ban.</param>
		public void InsertOrUpdate(UserBan ban)
		{
			this.InsertOrUpdate(ban, string.Empty);
		}

		/// <summary>
		/// Inserts or updates the specified user ban.
		/// </summary>
		/// <param name="ban">The ban.</param>
		/// <param name="name">The name.</param>
		public void InsertOrUpdate(UserBan ban, string name)
		{
			if (ban == null)
			{
				throw new ArgumentNullException("ban");
			}

			if (ban.Id > 0)
			{
				this.Update(ban);
			}
			else
			{
				var identity = this.AuthenticationService.Identity;

				if (!identity.IsAuthenticated())
				{
					return;
				}

				ban.User = this.UserService.GetBy(new UserNameSpecification(name));
				ban.Admin = this.UserService.GetById(identity.Id);

				this.Insert(ban);
			}
		}

		/// <summary>
		/// Deletes the expired bans.
		/// </summary>
		public void DeleteExpired()
		{
			var bans = base.GetAll(new UserBanExpiredSpecification());

			foreach (var ban in bans)
			{
				this.Delete(ban);
			}
		}
	}
}