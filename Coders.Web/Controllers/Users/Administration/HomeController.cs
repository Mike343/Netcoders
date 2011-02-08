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
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Controllers.Users.Administration.Queries;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users.Administration
{
	[Authorize]
	public class HomeController : DefaultController
	{
		public HomeController(
			IAuditService<User, UserAudit> auditService,
			IUserService userService)
		{
			this.AuditService = auditService;
			this.UserService = userService;
		}

		public IAuditService<User, UserAudit> AuditService
		{
			get;
			private set;
		}

		public IUserService UserService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(string status, SortUser sort, SortOrder order, int? page)
		{
			var query = new HomeQuery(status, sort, order, page);
			var users = UserService.GetPaged(query.Specification);
			var user = users.FirstOrDefault();
			var privilege = new UserPrivilege();

			return privilege.CanViewAny(user) ? View(Views.Index, users) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortAudit sort, SortOrder order, int? page, int? id)
		{
			var query = new AuditQuery<User>(sort, order, page, id);
			var audits = this.AuditService.GetPaged(query.Specification);
			var audit = audits.FirstOrDefault();
			var privilege = new AuditPrivilege();

			return privilege.CanView(audit) ? base.View(Views.History, audits) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var user = this.UserService.Create();
			var privilege = new UserPrivilege();

			return privilege.CanCreate(user) ? View(Views.Create, new UserAdminCreate()) : NotAuthorized();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(UserAdminCreate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var user = this.UserService.Create();
			var privilege = new UserPrivilege();

			if (!privilege.CanCreate(user))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			value.ValueToModel(user);

			this.UserService.Insert(user, value.Preference);

			var model = new UserAdminUpdate(user);

			model.SuccessMessage(Messages.UserCreated.FormatInvariant(user.Name));

			return base.View(Views.Update, model);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var user = this.UserService.GetById(id);

			if (user == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserPrivilege();

			return privilege.CanUpdateAny(user) ? View(Views.Update, new UserAdminUpdate(user)) : NotAuthorized();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(UserAdminUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var user = this.UserService.GetById(value.Id);

			if (user == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			// value to user
			value.ValueToModel(user);

			// update user
			this.UserService.Update(user);

			// update password if needed
			if (!string.IsNullOrEmpty(value.Password))
			{
				this.AuthenticationService.Update(user, value.Password);
			}

			// preference
			var preference = user.Preference;

			// value to preference
			value.ValueToPreference(preference);

			// update user preference
			this.UserService.UpdatePreference(preference);

			value.SuccessMessage(Messages.UserUpdated.FormatInvariant(user.Name));

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Reset(int id)
		{
			var user = this.UserService.GetById(id);

			if (user == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			var value = new UserAuthenticationReset();

			value.Initialize(user);

			return View(Views.Reset, value);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Reset(UserAuthenticationReset value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var user = this.UserService.GetById(value.Id);

			if (user == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdateAny(user))
			{
				return NotAuthorized();
			}

			value.Initialize(user);

			if (!ModelState.IsValid)
			{
				return View(Views.Reset, value);
			}

			this.AuthenticationService.Reset(user);

			var model = new UserAdminUpdate(user);

			model.SuccessMessage(Messages.UserPasswordReset.FormatInvariant(user.Name));

			return base.View(Views.Update, model);
		}
	}
}