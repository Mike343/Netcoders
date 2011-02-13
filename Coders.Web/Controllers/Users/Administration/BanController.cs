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
using Coders.Models.Common.Enums;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
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
		public ActionResult Index(SortUserBan sort, SortOrder order, int? page)
		{
			var bans = this.UserBanService.GetPaged(new UserBanSpecification
			{
				Page = page, 
				Limit = Setting.UserBanPageLimit.Value,
				Sort = sort,
				Order = order
			});

			var ban = bans.FirstOrDefault();
			var privilege = new UserBanPrivilege();

			return privilege.CanView(ban) ? base.View(Views.Index, bans) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var ban = this.UserBanService.Create();
			var privilege = new UserBanPrivilege();

			return privilege.CanCreate(ban) ? base.View(Views.Create, new UserBanCreateOrUpdate()) : NotAuthorized();
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
				return base.View(Views.Create, value);
			}

			value.ValueToModel(ban);

			this.UserBanService.InsertOrUpdate(ban, value.Name);

			var model = new UserBanCreateOrUpdate(ban);

			model.SuccessMessage(Messages.UserBanCreated.FormatInvariant(ban.User.Name));

			return base.View(Views.Update, model);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var ban = this.UserBanService.GetById(id);

			if (ban == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			return privilege.CanUpdate(ban) ? base.View(Views.Update, new UserBanCreateOrUpdate(ban)) : NotAuthorized();
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
				return base.HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			if (!privilege.CanUpdate(ban))
			{
				return NotAuthorized();
			}

			value.Initialize(ban.User);

			if (!ModelState.IsValid)
			{
				return base.View(Views.Update, value);
			}

			value.ValueToModel(ban);

			this.UserBanService.InsertOrUpdate(ban);

			value.SuccessMessage(Messages.UserBanUpdated.FormatInvariant(ban.User.Name));

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var ban = this.UserBanService.GetById(id);

			if (ban == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			return privilege.CanDelete(ban) ? base.View(Views.Delete, new UserBanDelete(ban)) : NotAuthorized();
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
				return base.HttpNotFound();
			}

			var privilege = new UserBanPrivilege();

			if (!privilege.CanDelete(ban))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Delete, value);
			}

			this.UserBanService.Delete(ban);

			return RedirectToRoute(UsersAdministrationRoutes.BanIndex);
		}
	}
}