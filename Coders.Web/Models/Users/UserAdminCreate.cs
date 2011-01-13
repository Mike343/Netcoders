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
using Coders.Models.Users;
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserAdminCreate : UserCreate
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAdminCreate"/> class.
		/// </summary>
		public UserAdminCreate()
		{
			this.Statuses = Enum.GetNames(typeof(UserStatus));
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the signature.
		/// </summary>
		/// <value>The signature.</value>
		public string Signature
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is protected.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is protected; otherwise, <c>false</c>.
		/// </value>
		public bool IsProtected
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public UserStatus Status
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the statuses.
		/// </summary>
		/// <value>The statuses.</value>
		public IList<string> Statuses
		{
			get;
			private set;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(User entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.ValueToModel(entity);

			entity.Title = this.Title;
			entity.Signature = this.Signature;
			entity.IsProtected = this.IsProtected;
			entity.Status = this.Status;
		}
	}
}