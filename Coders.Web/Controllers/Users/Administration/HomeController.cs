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
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users.Administration
{
	[Authorize]
	public class HomeController : DefaultController
	{
		public HomeController(IUserService userService)
		{
			this.UserService = userService;
		}

		public IUserService UserService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(int? page)
		{
			var users = UserService.GetPaged(new UserSpecification
			{
				Page = page, 
				Limit = Setting.UserPageLimit.Value
			});

			var user = users.FirstOrDefault();
			var privilege = new UserPrivilege();

			return privilege.CanViewAny(user) ? View(Views.Index, users) : NotAuthorized();
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

			this.UserService.Insert(user);

			return RedirectToRoute(UsersAdministrationRoutes.HomeUpdate, new { id = user.Id });
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

			// preference
			var preference = user.Preference;

			// value to preference
			value.ValueToPreference(preference);

			// update user preference
			this.UserService.UpdatePreference(preference);

			return RedirectToRoute(UsersAdministrationRoutes.HomeUpdate, new { id = user.Id });
		}
	}
}