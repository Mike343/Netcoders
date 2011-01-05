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
using System.Web;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Users;
#endregion

namespace Coders.Services
{
	public class UserHostService : EntityService<UserHost>, IUserHostService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="userRepository">The user repository.</param>
		/// <param name="repository">The repository.</param>
		public UserHostService(
			IAuthenticationService authenticationService, 
			IUserRepository userRepository, 
			IUserHostRepository repository)
			: base(repository)
		{
			this.AuthenticationService = authenticationService;
			this.UserRepository = userRepository;
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
		/// Gets or sets the user repository.
		/// </summary>
		/// <value>The user repository.</value>
		public IUserRepository UserRepository
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the host address of the current user.
		/// </summary>
		/// <returns></returns>
		public string GetAddress()
		{
			var context = HttpContext.Current;

			if (context == null)
			{
				return "127.0.0.1";
			}

			var host = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

			if (string.IsNullOrEmpty(host))
			{
				host = context.Request.UserHostAddress;
			}

			return (string.IsNullOrEmpty(host)) ? "127.0.0.1" : host;
		}

		/// <summary>
		/// Captures the current user host address.
		/// </summary>
		public void Capture()
		{
			var identity = this.AuthenticationService.Identity;

			if (!identity.IsAuthenticated())
			{
				return;
			}

			// get current host address
			var address = this.GetAddress();

			// check if we already have a record of this address
			var host = this.GetBy(
				new UserHostUserSpecification(identity.Id).And(
					new UserHostAddressSpecification(address)
				)
			);

			if (host != null)
			{
				return;
			}

			// insert new address
			host = this.Create();

			host.HostAddress = address;
			host.User = this.UserRepository.GetById(identity.Id);

			this.Insert(host);
		}
	}
}