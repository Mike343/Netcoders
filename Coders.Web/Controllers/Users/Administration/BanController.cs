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
	public class BanController : SecureDefaultController
	{
		public BanController(IUserBanService userBanService)
		{
			this.UserBanService = userBanService;
		}

		public IUserBanService UserBanService
		{
			get; 
			private set;
		}

		[HttpGet]
		public ActionResult Index(int? page)
		{
			var bans = UserBanService.GetPaged(new UserBanSpecification
			{
				Page = page, 
				Limit = Setting.UserBanPageLimit.Value
			});

			var ban = bans.FirstOrDefault();
			var privilege = new UserBanPrivilege();

			return privilege.CanView(ban) ? View(Views.Index, bans) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var ban = UserBanService.Create();
			var privilege = new UserBanPrivilege();

			return privilege.CanCreate(ban) ? View(Views.Create, new UserBanCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(UserBanCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var ban = this.UserBanService.Create();
			var privilege = new UserBanPrivilege();

			if (!privilege.CanUpdate(ban))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			value.ValueToModel(ban);

			this.UserBanService.InsertOrUpdate(ban, value.Name);

			return RedirectToRoute(UsersAdministrationRoutes.BanUpdate, new { id = ban.Id });
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var ban = UserBanService.GetById(id);

			if (ban == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			return privilege.CanUpdate(ban) ? View(Views.Update, new UserBanCreateOrUpdate(ban)) : NotAuthorized();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(UserBanCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var ban = this.UserBanService.GetById(value.Id);

			if (ban == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			if (!privilege.CanUpdate(ban))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			value.ValueToModel(ban);

			this.UserBanService.InsertOrUpdate(ban);

			return RedirectToRoute(UsersAdministrationRoutes.BanUpdate, new { id = ban.Id });
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var ban = UserBanService.GetById(id);

			if (ban == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			return privilege.CanDelete(ban) ? View(Views.Delete, new UserBanDelete(ban)) : NotAuthorized();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(UserBanDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var ban = this.UserBanService.GetById(value.Id);

			if (ban == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			if (!privilege.CanDelete(ban))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Delete, value);
			}

			this.UserBanService.Delete(ban);

			return RedirectToRoute(UsersAdministrationRoutes.BanIndex);
		}
	}
}