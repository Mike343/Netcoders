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
using System.Web.Mvc;
using Coders.Extensions;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
using Coders.Web.ViewModels;
#endregion

namespace Coders.Web.Controllers.Users.Administration
{
	[Authorize]
	public class RoleController : DefaultController
	{
		public RoleController(
			IUserService userService, 
			IUserRoleService userRoleService)
		{
			this.UserService = userService;
			this.UserRoleService = userRoleService;
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
			var roles = UserRoleService.GetAll();
			var role = roles.FirstOrDefault();
			var privilege = new UserRolePrivilege();

			return privilege.CanView(role) ? View(Views.Index, roles) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var role = UserRoleService.Create();
			var privilege = new UserRolePrivilege();

			return privilege.CanCreate(role) ? View(Views.Create, new UserRoleCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(UserRoleCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			var role = UserRoleService.Create();
			var privilege = new UserRolePrivilege();

			if (!privilege.CanCreate(role))
			{
				return NotAuthorized();
			}

			value.ValueToModel(role);

			this.UserRoleService.InsertOrUpdate(role, value.Privileges);

			return RedirectToRoute(UsersAdministrationRoutes.RoleUpdate, new { id = role.Id });
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var role = UserRoleService.GetById(id);

			if (role == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			return privilege.CanUpdate(role) ? View(Views.Update, new UserRoleCreateOrUpdate(role)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(UserRoleCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			var role = UserRoleService.GetById(value.Id);

			if (role == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			if (!privilege.CanUpdate(role))
			{
				return NotAuthorized();
			}

			value.ValueToModel(role);

			this.UserRoleService.InsertOrUpdate(role, value.Privileges);

			return RedirectToRoute(UsersAdministrationRoutes.RoleUpdate, new { id = role.Id });
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var role = this.UserRoleService.GetById(id);

			if (role == null)
			{
				return HttpNotFound();
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

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			var role = UserRoleService.GetById(value.Id);

			if (role == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserRolePrivilege();

			if (!privilege.CanDelete(role))
			{
				return NotAuthorized();
			}

			this.UserRoleService.Delete(role);

			return RedirectToRoute(UsersAdministrationRoutes.RoleIndex);
		}

		[HttpGet]
		public ActionResult Privilege(int id)
		{
			var user = UserService.GetById(id);

			if (user == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			var roles = UserRoleService.GetAll();
			var permissions = this.UserRoleService.GetPrivileges(new UserRoleRelationUserSpecification(id));

			return View(Views.Privilege, new UserPrivilegeViewModel(user, roles, permissions));
		}

		[HttpPost]
		public ActionResult Privilege(int id, UserRoleRelationUpdate[] values)
		{
			var user = UserService.GetById(id);

			if (user == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			this.UserRoleService.UpdatePrivileges(user, values);

			return RedirectToRoute(UsersAdministrationRoutes.RolePrivilege, new { id = user.Id });
		}
	}
}