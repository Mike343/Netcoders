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
using System.Collections.Generic;
using System.Linq;
using Coders.Authentication;
using Coders.Models.Users;
#endregion

namespace Coders.Web.Extensions
{
	public static class UserRoleExtension
	{
		/// <summary>
		/// Determines whether the privileges has the specified role
		/// </summary>
		/// <param name="privileges">The privileges.</param>
		/// <param name="role">The role.</param>
		/// <returns>
		/// 	<c>true</c> if permissions has the specified role; otherwise, <c>false</c>.
		/// </returns>
		public static bool Has(this IList<UserRoleRelation> privileges, string role)
		{
			return privileges.Count(x => x.Role.Title == role) > 0;
		}

		/// <summary>
		/// Determines whether the privileges has the specified role and privilege
		/// </summary>
		/// <param name="privileges">The privileges.</param>
		/// <param name="role">The role.</param>
		/// <param name="privilege">The privilege.</param>
		/// <returns>
		/// 	<c>true</c> if permissions has the specified role and permission; otherwise, <c>false</c>.
		/// </returns>
		public static bool Has(this IList<UserRoleRelation> privileges, string role, Privileges privilege)
		{
			return privileges.Count(x => x.Role.Title == role && (x.Privilege & privilege) == privilege) > 0;
		}

		/// <summary>
		/// Determines whether the privileges has the specified role and privilege
		/// </summary>
		/// <param name="privileges">The privileges.</param>
		/// <param name="role">The role.</param>
		/// <param name="privilege">The privilege.</param>
		/// <returns>
		/// 	<c>true</c> if the permissions has the specified role and permission; otherwise, <c>false</c>.
		/// </returns>
		public static bool Has(this IDictionary<string, UserRoleRelation> privileges, string role, int privilege)
		{
			return (privileges.ContainsKey(role) && ((int)privileges[role].Privilege & privilege) == privilege);
		}

		/// <summary>
		/// Determines whether the privileges has the specified role and privilege
		/// </summary>
		/// <param name="privileges">The privileges.</param>
		/// <param name="role">The role.</param>
		/// <param name="privilege">The privilege.</param>
		/// <returns>
		/// 	<c>true</c> if the permissions has the specified role and permission; otherwise, <c>false</c>.
		/// </returns>
		public static bool Has(this IDictionary<string, UserRoleRelation> privileges, string role, Privileges privilege)
		{
			return (privileges.ContainsKey(role) && (privileges[role].Privilege & privilege) == privilege);
		}

		/// <summary>
		/// Gets the title class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string GetTitleClass(this UserRoleRelationUpdate value)
		{
			if (value.Role.IsAdministrator)
			{
				return value.Selected ? "role role-administrator role-administrator-selected" : "role role-administrator";
			}

			return value.Selected ? "role role-selected" : "role";
		}

		/// <summary>
		/// Gets the check box class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="value2">The value2.</param>
		/// <returns></returns>
		public static string GetCheckBoxClass(this UserRoleRelationUpdate value, UserRoleRelationUpdateValue value2)
		{
			if (value.Role.IsAdministrator)
			{
				return value2.Selected ? "checkbox checkbox-administrator checkbox-administrator-selected" : "checkbox checkbox-administrator";
			}

			return value2.Selected ? "checkbox checkbox-selected" : "checkbox";
		}
	}
}