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
#endregion

namespace Coders.Models.Users
{
	public class UserRoleRelationUpdate
	{
		/// <summary>
		/// Gets or sets the role id.
		/// </summary>
		/// <value>The role id.</value>
		public int RoleId
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="UserRoleRelationUpdate"/> is selected.
		/// </summary>
		/// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
		public bool Selected
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>The role.</value>
		public UserRole Role
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the privileges.
		/// </summary>
		/// <value>The privileges.</value>
		public IList<UserRoleRelationUpdateValue> Privileges
		{
			get;
			set;
		}
	}
}