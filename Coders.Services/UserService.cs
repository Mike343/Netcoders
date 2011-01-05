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
using System;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Services
{
	public class UserService : EntityService<User>, IUserService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="textFormattingService">The text formatting service.</param>
		/// <param name="userHostService">The user host service.</param>
		/// <param name="userRoleService">The user role service.</param>
		/// <param name="repository">The repository.</param>
		public UserService(
			IAuthenticationService authenticationService,
			ITextFormattingService textFormattingService,
			IUserHostService userHostService,
			IUserRoleService userRoleService,
			IUserRepository repository) 
			: base(repository)
		{
			this.AuthenticationService = authenticationService;
			this.TextFormattingService = textFormattingService;
			this.UserHostService = userHostService;
			this.UserRoleService = userRoleService;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the text formatting service.
		/// </summary>
		/// <value>The text formatting service.</value>
		public ITextFormattingService TextFormattingService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user host service.
		/// </summary>
		/// <value>The user host service.</value>
		public IUserHostService UserHostService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user role service.
		/// </summary>
		/// <value>The user role service.</value>
		public IUserRoleService UserRoleService
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns></returns>
		public override User Create()
		{
			var user = base.Create();

			user.Status = UserStatus.Activated;
			user.Created = DateTime.Now;
			user.Updated = user.Created;
			user.LastVisit = user.Created;
			user.LastLogOn = user.Created;

			return user;
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(User entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			var salt = this.AuthenticationService.GeneratePassword(Setting.UserPasswordSaltLength.Value);

			entity.Slug = entity.Name.Slug();
			entity.Password = entity.Password.HashToSha1(salt).Hex();
			entity.HostAddress = this.UserHostService.GetAddress();
			entity.Salt = salt;
			entity.SignatureParsed = this.TextFormattingService.Parse(entity.Signature, false);

			base.Insert(entity);

			var roles = this.UserRoleService.GetAll(new UserRoleDefaultSpecification());

			foreach (var role in roles)
			{
				this.UserRoleService.InsertPrivilege(new UserRolePrivilege
				{
					Privilege = role.Privilege,
					User = entity,
					Role = role
				});
			}
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(User entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Slug = entity.Name.Slug();
			entity.SignatureParsed = this.TextFormattingService.Parse(entity.Signature, false);

			base.Update(entity);
		}
	}
}