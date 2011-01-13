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
using System.Security.Principal;
using Coders.Authentication;
#endregion

namespace Coders.Web.Authentication
{
	public class WebPrivilegePrincipal : PrivilegePrincipal
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WebPrivilegePrincipal"/> class.
		/// </summary>
		/// <param name="identity">The identity.</param>
		public WebPrivilegePrincipal(IIdentity identity) 
			: base(identity)
		{

		}

		/// <summary>
		/// Determines the role privileges for the current user.
		/// </summary>
		public override void DetermineRolePrivileges()
		{
			var identity = this.Identity as WebUserIdentity;

			if (identity == null || identity.Session == null)
			{
				return;
			}

			var privileges = identity.Session.Privileges;

			if (privileges == null || privileges.Count <= 0)
			{
				return;
			}

			this.Privileges.Clear();

			foreach (var privilege in privileges)
			{
				this.Privileges.Add(privilege.Role.Title, new PrivilegesValue(privilege.Privilege));
			}
		}
	}
}