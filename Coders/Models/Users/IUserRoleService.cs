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
		IList<UserRoleRelation> GetPrivileges(ISpecification<UserRoleRelation> specification);

		/// <summary>
		/// Inserts or updates the specified role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <param name="privileges">The privileges.</param>
		void InsertOrUpdate(UserRole role, IList<UserRoleRelationUpdateValue> privileges);

		/// <summary>
		/// Inserts the privilege.
		/// </summary>
		/// <param name="relation">The relation.</param>
		void InsertPrivilege(UserRoleRelation relation);

		/// <summary>
		/// Updates the privileges for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="values">The values.</param>
		void UpdatePrivileges(User user, IList<UserRoleRelationUpdate> values);
	}
}