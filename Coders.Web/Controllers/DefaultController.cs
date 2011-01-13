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
using System.Web.Mvc;
using Coders.Authentication;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Web.ActionResults;
#endregion

namespace Coders.Web.Controllers
{
	public abstract class DefaultController : Controller
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultController"/> class.
		/// </summary>
		protected DefaultController()
		{
			this.AuthenticationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();
		}

		/// <summary>
		/// Gets the principal.
		/// </summary>
		/// <value>The principal.</value>
		public PrivilegePrincipal Principal
		{
			get
			{
				return this.AuthenticationService.Principal;
			}
		}

		/// <summary>
		/// Gets the identity.
		/// </summary>
		/// <value>The identity.</value>
		public UserIdentity Identity 
		{ 
			get
			{
				return this.AuthenticationService.Identity;
			}
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		protected IAuthenticationService AuthenticationService
		{
			get;
			private set;
		}

		/// <summary>
		/// Displays the specified status message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		protected static StatusResult Status(string message)
		{
			return Status(message, null);
		}

		/// <summary>
		/// Displays the specified status message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		protected static StatusResult Status(string message, params object[] parameters)
		{
			return parameters == null ? new StatusResult(message) : new StatusResult(message.FormatInvariant(parameters));
		}

		/// <summary>
		/// Displays the not authorized message.
		/// </summary>
		/// <returns></returns>
		protected static NotAuthorizedResult NotAuthorized()
		{
			return new NotAuthorizedResult();
		}
	}
}