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
using System.Web.Security;
using Coders.Authentication;
using Coders.Web.Models.Users;
#endregion

namespace Coders.Web.Authentication
{
	public class WebUserIdentity : UserIdentity
	{
		// private fields
		private UserSession _session;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebUserIdentity"/> class.
		/// </summary>
		/// <param name="ticket">The ticket.</param>
		/// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
		public WebUserIdentity(FormsAuthenticationTicket ticket, bool isAuthenticated)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}

			base.Name = ticket.Name;
			base.IsAuthenticated = isAuthenticated;
		}

		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		/// <value>The session.</value>
		public UserSession Session
		{
			get
			{
				return _session;
			}
			set
			{
				_session = value;

				if (_session != null)
				{
					base.Id = _session.Id;
				}
			}
		}

		/// <summary>
		/// Authenticates the specified token.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <returns></returns>
		public override bool Authenticate(IAuthenticationToken token)
		{
			return true;
		}
	}
}