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
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Specifications;
#endregion

namespace Coders.Services
{
	public class UserRoleService : EntityService<UserRole>, IUserRoleService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleService"/> class.
		/// </summary>
		/// <param name="auditService">The audit service.</param>
		/// <param name="repository">The repository.</param>
		/// <param name="userRolePrivilegeRepository">The user role privilege repository.</param>
		public UserRoleService(
			IAuditService<UserRole, UserRoleAudit> auditService,
			IRepository<UserRole> repository,
			IRepository<UserRoleRelation> userRolePrivilegeRepository)
			: base(repository)
		{
			this.AuditService = auditService;
			this.UserRolePrivilegeRepository = userRolePrivilegeRepository;
		}

		/// <summary>
		/// Gets the audit service.
		/// </summary>
		public IAuditService<UserRole, UserRoleAudit> AuditService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user role privilege repository.
		/// </summary>
		/// <value>The user role privilege repository.</value>
		public IRepository<UserRoleRelation> UserRolePrivilegeRepository
		{
			get; 
			private set; 
		}

		/// <summary>
		/// Gets the permissions using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public IList<UserRoleRelation> GetPrivileges(ISpecification<UserRoleRelation> specification)
		{
			return this.UserRolePrivilegeRepository.GetAll(specification);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(UserRole entity)
		{
			entity.Slug = entity.Title.Slug();

			base.Insert(entity);

			this.AuditService.Audit(entity, AuditAction.Create);
		}

		/// <summary>
		/// Inserts the privilege.
		/// </summary>
		/// <param name="relation">The relation.</param>
		public void InsertPrivilege(UserRoleRelation relation)
		{
			this.UserRolePrivilegeRepository.Insert(relation);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(UserRole entity)
		{
			entity.Slug = entity.Title.Slug();

			base.Update(entity);

			this.AuditService.Audit(entity, AuditAction.Update);
		}

		/// <summary>
		/// Inserts or updates the specified role.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <param name="privileges">The privileges.</param>
		public void InsertOrUpdate(UserRole role, IList<UserRoleRelationUpdateValue> privileges)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}

			if (privileges == null)
			{
				throw new ArgumentNullException("privileges");
			}

			role.Privilege = (Privileges)privileges.Where(x => x.Selected).Aggregate(0, (value, x) => value | x.Privilege);

			if (role.Id > 0)
			{
				this.Update(role);
			}
			else
			{
				this.Insert(role);
			}
		}

		/// <summary>
		/// Updates the privileges for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="values">The values.</param>
		public void UpdatePrivileges(User user, IList<UserRoleRelationUpdate> values)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			foreach (var value in values)
			{
				var permission = this.UserRolePrivilegeRepository.GetBy(
					new UserRoleRelationUserSpecification(user.Id).And(
						new UserRoleRelationRoleSpecification(value.RoleId)
					)
				);

				if (value.Selected)
				{
					var action = value.Privileges.Where(x => x.Selected).Aggregate(0, (current, x) => current | x.Privilege);

					if (permission == null)
					{
						var role = this.GetById(value.RoleId);

						if (role == null)
						{
							return;
						}

						this.UserRolePrivilegeRepository.Insert(new UserRoleRelation
						{
							Privilege = (Privileges)action,
							User = user,
							Role = role
						});
					}
					else
					{
						permission.Privilege = (Privileges)action;

						this.UserRolePrivilegeRepository.Update(permission);
					}
				}
				else
				{
					if (permission != null)
					{
						this.UserRolePrivilegeRepository.Delete(permission);
					}
				}
			}
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(UserRole entity)
		{
			base.Delete(entity);

			this.AuditService.Audit(entity, AuditAction.Delete);
		}
	}
}