﻿#region License
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
using System.Collections.Generic;
using System.Security.Principal;
#endregion

namespace Coders.Authentication
{
	public abstract class PrivilegePrincipal : IPrincipal
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PrivilegePrincipal"/> class.
		/// </summary>
		/// <param name="identity">The identity.</param>
		protected PrivilegePrincipal(IIdentity identity)
		{
			this.Identity = identity;
			this.Privileges = new Dictionary<string, PrivilegesValue>();
		}

		/// <summary>
		/// Gets the identity of the current principal.
		/// </summary>
		/// <value></value>
		/// <returns>The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.</returns>
		public IIdentity Identity
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets or sets the actions.
		/// </summary>
		/// <value>The actions.</value>
		public Dictionary<string, PrivilegesValue> Privileges
		{
			get;
			private set;
		}

		/// <summary>
		/// Determines whether the current principal belongs to the specified role.
		/// </summary>
		/// <param name="role">The name of the role for which to check membership.</param>
		/// <returns>
		/// true if the current principal is a member of the specified role; otherwise, false.
		/// </returns>
		public bool IsInRole(string role)
		{
			if (!this.Privileges.ContainsKey(role))
			{
				return false;
			}

			var perm = this.Privileges[role];

			return perm.AllowedTo(Authentication.Privileges.View);
		}

		/// <summary>
		/// Determines if the current user is in the specified role has the specified privilege.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <param name="privilege">The privilege.</param>
		/// <returns></returns>
		public bool AllowedTo(string role, Privileges privilege)
		{
			return this.Privileges.ContainsKey(role) && this.Privileges[role].AllowedTo(privilege);
		}

		/// <summary>
		/// Determines the role privileges for the current user.
		/// </summary>
		public abstract void DetermineRolePrivileges();
	}
}