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
using Coders.Specifications;
#endregion

namespace Coders.Models.Users
{
	public interface IUserRoleService : IEntityService<UserRole>
	{
		/// <summary>
		/// Gets the permissions using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		IList<UserRolePrivilege> GetPrivileges(ISpecification<UserRolePrivilege> specification);

		/// <summary>
		/// Inserts the privilege.
		/// </summary>
		/// <param name="privilege">The privilege.</param>
		void InsertPrivilege(UserRolePrivilege privilege);

		/// <summary>
		/// Updates the privileges for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="values">The values.</param>
		void UpdatePrivileges(User user, UserRolePrivilegeUpdate[] values);
	}
}