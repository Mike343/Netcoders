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
using Coders.Models;
#endregion

namespace Coders.Extensions
{
	public static class IdentityExtension
	{
		/// <summary>
		/// Determines whether the specified identity is authenticated.
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if the specified identity is authenticated; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsAuthenticated(this UserIdentity identity)
		{
			return identity != null && identity.IsAuthenticated;
		}

		/// <summary>
		/// Determines whether the specified principal contains the specified role.
		/// </summary>
		/// <param name="principal">The principal.</param>
		/// <param name="role">The role.</param>
		/// <returns>
		/// 	<c>true</c> if the specified principal contains the specified role; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsRole(this PrivilegePrincipal principal, string role)
		{
			return principal != null && principal.Privileges != null && principal.Privileges.ContainsKey(role);
		}

		/// <summary>
		/// Determines whether the specified principal is super.
		/// </summary>
		/// <param name="principal">The principal.</param>
		/// <returns>
		///   <c>true</c> if the specified principal is super; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSuper(this IPrincipal principal)
		{
			var prin = principal as PrivilegePrincipal;

			if (prin == null)
			{
				return false;
			}

			return prin.ContainsRole(Roles.Privileges) && prin.Privileges[Roles.Privileges].AllowedTo(Privileges.UpdateAny);
		}
	}
}