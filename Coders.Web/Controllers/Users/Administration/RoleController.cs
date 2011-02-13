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
using System.Web.Mvc;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Extensions;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users.Administration
{
	[Authorize]
	public class RoleController : DefaultController
	{
		public RoleController(
			IAuditService<UserRole, UserRoleAudit> auditService,
			IUserService userService, 
			IUserRoleService userRoleService)
		{
			this.AuditService = auditService;
			this.UserService = userService;
			this.UserRoleService = userRoleService;
		}

		public IAuditService<UserRole, UserRoleAudit> AuditService
		{
			get;
			private set;
		}

		public IUserService UserService
		{
			get;
			private set;
		}

		public IUserRoleService UserRoleService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index()
		{
			var roles = this.UserRoleService.GetAll();
			var role = roles.FirstOrDefault();
			var privilege = new UserRolePrivilege();

			return privilege.CanView(role) ? base.View(Views.Index, roles) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortAudit sort, SortOrder order, int? page, int? id)
		{
			var query = new AuditQuery<UserRole>(sort, order, page, id);
			var audits = this.AuditService.GetPaged(query.Specification);
			var audit = audits.FirstOrDefault();
			var privilege = new AuditPrivilege();

			return privilege.CanView(audit) ? base.View(Views.History, audits) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var role = this.UserRoleService.Create();
			var privilege = new UserRolePrivilege();

			return privilege.CanCreate(role) ? base.View(Views.Create, new UserRoleCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(UserRoleCreateOrUpdate value, IList<UserRoleRelationUpdateValue> privileges)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var role = this.UserRoleService.Create();
			var privilege = new UserRolePrivilege();

			if (!privilege.CanCreate(role))
			{
				return NotAuthorized();
			}

			value.Validate();

			if (value.IsValid)
			{
				value.ValueToModel(role);

				this.UserRoleService.InsertOrUpdate(role, privileges);

				value = new UserRoleCreateOrUpdate(role);
				value.SuccessMessage(Messages.UserRoleCreated.FormatInvariant(role.Title));
			}
			else
			{
				value.CopyToModel(ModelState);
			}

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var role = this.UserRoleService.GetById(id);

			if (role == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			return privilege.CanUpdate(role) ? base.View(Views.Update, new UserRoleCreateOrUpdate(role)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(UserRoleCreateOrUpdate value, IList<UserRoleRelationUpdateValue> privileges)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var role = this.UserRoleService.GetById(value.Id);

			if (role == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			if (!privilege.CanUpdate(role))
			{
				return NotAuthorized();
			}

			value.Validate();

			if (value.IsValid)
			{
				value.ValueToModel(role);

				this.UserRoleService.InsertOrUpdate(role, privileges);

				value.SuccessMessage(Messages.UserRoleUpdated.FormatInvariant(role.Title));
			}
			else
			{
				value.CopyToModel(ModelState);
			}

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var role = this.UserRoleService.GetById(id);

			if (role == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			return privilege.CanDelete(role) ? base.View(Views.Delete, new UserRoleDelete(role)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(UserRoleDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var role = this.UserRoleService.GetById(value.Id);

			if (role == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			if (!privilege.CanDelete(role))
			{
				return NotAuthorized();
			}

			this.UserRoleService.Delete(role);

			return base.RedirectToRoute(UsersAdministrationRoutes.RoleIndex);
		}

		[HttpGet]
		public ActionResult Privilege(int id)
		{
			var user = this.UserService.GetById(id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			var roles = this.UserRoleService.GetAll();
			var privileges = this.UserRoleService.GetPrivileges(new UserRoleRelationUserSpecification(user.Id));
			var value = new UserRolePrivilegeUpdate();

			value.Initialize(user, roles, privileges);

			return base.View(Views.Privilege, value);
		}

		[HttpPost]
		public ActionResult Privilege(UserRolePrivilegeUpdate value)
		{
			var user = this.UserService.GetById(value.UserId);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			this.UserRoleService.UpdatePrivileges(user, value.Values);

			value.SuccessMessage(Messages.UserPrivilegeUpdated.FormatInvariant(user.Name));

			var roles = this.UserRoleService.GetAll();
			var privileges = this.UserRoleService.GetPrivileges(new UserRoleRelationUserSpecification(user.Id));

			value.Initialize(user, roles, privileges);
			value.SuccessMessage(Messages.UserPrivilegeUpdated.FormatInvariant(user.Name));

			return base.View(Views.Privilege, value);
		}
	}
}