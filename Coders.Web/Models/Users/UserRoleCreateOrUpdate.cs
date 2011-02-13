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
using Coders.Authentication;
using Coders.Extensions;
using Coders.Models.Users;
using Coders.Strings;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserRoleCreateOrUpdateValidatorCollection))]
	public class UserRoleCreateOrUpdate : Value<UserRole>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleCreateOrUpdate"/> class.
		/// </summary>
		public UserRoleCreateOrUpdate()
		{
			this.PrivilegesNames = Enum.GetNames(typeof(Privileges));
			this.PrivilegesValues = Enum.GetValues(typeof(Privileges)) as int[];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleCreateOrUpdate"/> class.
		/// </summary>
		/// <param name="role">The role.</param>
		public UserRoleCreateOrUpdate(UserRole role) 
			: this()
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}

			this.Id = role.Id;
			this.Title = role.Title;
			this.IsDefault = role.IsDefault;
			this.IsAdministrator = role.IsAdministrator;
			this.Privilege = role.Privilege;
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get;
			set;
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
		/// Gets or sets a value indicating whether this instance is default.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefault
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is administrator.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is administrator; otherwise, <c>false</c>.
		/// </value>
		public bool IsAdministrator
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the privilege.
		/// </summary>
		/// <value>The privilege.</value>
		public Privileges Privilege
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets the privileges names.
		/// </summary>
		/// <value>The privileges names.</value>
		public IList<string> PrivilegesNames
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the privileges values.
		/// </summary>
		/// <value>The privileges values.</value>
		public IList<int> PrivilegesValues
		{
			get;
			private set;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(UserRole entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Title = this.Title;
			entity.IsDefault = this.IsDefault;
			entity.IsAdministrator = this.IsAdministrator;
		}
	}

	public class UserRoleCreateOrUpdateValidatorCollection : AbstractValidator<UserRoleCreateOrUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleCreateOrUpdateValidatorCollection"/> class.
		/// </summary>
		public UserRoleCreateOrUpdateValidatorCollection()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Title));
		}
	}
}