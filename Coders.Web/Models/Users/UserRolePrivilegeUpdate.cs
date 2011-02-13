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
using System.Collections.Generic;
using System.Linq;
using Coders.Authentication;
using Coders.Models.Users;
using Coders.Web.Extensions;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserRolePrivilegeUpdate : Value
	{
		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		/// <value>The user id.</value>
		public int UserId
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		public User User
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the names.
		/// </summary>
		/// <value>The names.</value>
		public IList<string> Names
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the values.
		/// </summary>
		/// <value>The values.</value>
		public IList<UserRoleRelationUpdate> Values
		{
			get; 
			set;
		}

		/// <summary>
		/// Initializes the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="roles">The roles.</param>
		/// <param name="privileges">The privileges.</param>
		public void Initialize(User user, IEnumerable<UserRole> roles, IEnumerable<UserRoleRelation> privileges)
		{
			this.UserId = user.Id;
			this.User = user;
			this.Names = Enum.GetNames(typeof(Privileges));
			this.Values = new List<UserRoleRelationUpdate>();

			var dictionary = privileges.ToDictionary(x => x.Role.Title);
			var values = Enum.GetValues(typeof(Privileges)) as int[];

			if (values == null)
			{
				return;
			}

			foreach (var role in roles)
			{
				var relation = new UserRoleRelationUpdate
				{
					RoleId = role.Id,
					Selected = dictionary.ContainsKey(role.Title),
					Role = role,
					Privileges = new List<UserRoleRelationUpdateValue>()
				};

				foreach (var privilege in values)
				{
					relation.Privileges.Add(
						new UserRoleRelationUpdateValue
						{
							Privilege = privilege,
							Selected = dictionary.Has(role.Title, privilege)
						}
					);
				}

				this.Values.Add(relation);
			}
		}
	}
}