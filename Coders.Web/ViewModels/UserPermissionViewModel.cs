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
#endregion

namespace Coders.Web.ViewModels
{
	public class UserPrivilegeViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserPrivilegeViewModel"/> class.
		/// </summary>
		public UserPrivilegeViewModel()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserPrivilegeViewModel"/> class.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="roles">The roles.</param>
		/// <param name="privileges">The privileges.</param>
		public UserPrivilegeViewModel(User user, IList<UserRole> roles, IEnumerable<UserRoleRelation> privileges)
		{
			this.User = user;
			this.Roles = roles;
			this.Privileges = privileges.ToDictionary(x => x.Role.Title);
			this.PermissionsNames = Enum.GetNames(typeof(Privileges));
			this.PermissionsValues = Enum.GetValues(typeof(Privileges)) as int[];
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
		/// Gets or sets the roles.
		/// </summary>
		/// <value>The roles.</value>
		public IList<UserRole> Roles
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the privileges.
		/// </summary>
		/// <value>The privileges.</value>
		public Dictionary<string, UserRoleRelation> Privileges
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the permissions names.
		/// </summary>
		/// <value>The permissions names.</value>
		public IList<string> PermissionsNames
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the permissions values.
		/// </summary>
		/// <value>The permissions values.</value>
		public IList<int> PermissionsValues
		{
			get;
			private set;
		}
	}
}