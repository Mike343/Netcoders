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
using Coders.Models.Logs;
using Coders.Models.Logs.Enums;
using Coders.Models.Settings;
using Coders.Models.Settings.Enums;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Models.Settings;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	public class SettingController : SecureDefaultController
	{
		public SettingController(
			ILogService logService, 
			ISettingService settingService)
		{
			this.LogService = logService;
			this.SettingService = settingService;
		}

		public ILogService LogService
		{
			get;
			private set;
		}

		public ISettingService SettingService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(string group, SortSetting sort, SortOrder order, int? page)
		{
			var query = new SettingIndexQuery(group, sort, order, page);
			var settings = this.SettingService.GetPaged(query.Specification);
			var setting = settings.FirstOrDefault();
			var privilege = new SettingPrivilege();

			return privilege.CanView(setting) ? View(Views.Index, settings) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortLog sort, SortOrder order, int? page, int? id)
		{
			var query = new HistoryQuery(Log.Settings, sort, order, page, id);
			var logs = this.LogService.GetPaged(query.Specification);
			var log = logs.FirstOrDefault();
			var privilege = new LogPrivilege();

			return privilege.CanView(log) ? base.View(Views.History, logs) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var setting = this.SettingService.Create();
			var privilege = new SettingPrivilege();

			return privilege.CanCreate(setting) ? base.View(Views.Create, new SettingCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(SettingCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Create, value);
			}

			var setting = this.SettingService.Create();
			var privilege = new SettingPrivilege();

			if (!privilege.CanCreate(setting))
			{
				return NotAuthorized();
			}

			value.ValueToModel(setting);

			this.SettingService.InsertOrUpdate(setting);
			this.SettingService.Rebuild();

			var model = new SettingCreateOrUpdate(setting);

			model.SuccessMessage(Messages.SettingCreated.FormatInvariant(setting.Title));

			return base.View(Views.Update, model);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var setting = this.SettingService.GetById(id);

			if (setting == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new SettingPrivilege();

			return privilege.CanUpdate(setting) ? base.View(Views.Update, new SettingCreateOrUpdate(setting)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(SettingCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Update, value);
			}

			var setting = this.SettingService.GetById(value.Id);

			if (setting == null)
			{
				return HttpNotFound();
			}

			var privilege = new SettingPrivilege();

			if (!privilege.CanUpdate(setting))
			{
				return NotAuthorized();
			}

			value.ValueToModel(setting);

			this.SettingService.InsertOrUpdate(setting);
			this.SettingService.Rebuild();

			value.SuccessMessage(Messages.SettingUpdated.FormatInvariant(setting.Title));

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var setting = this.SettingService.GetById(id);

			if (setting == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new SettingPrivilege();

			return privilege.CanDelete(setting) ? base.View(Views.Delete, new SettingDelete(setting)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(SettingDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Delete, value);
			}

			var setting = this.SettingService.GetById(value.Id);

			if (setting == null)
			{
				return HttpNotFound();
			}

			var privilege = new SettingPrivilege();

			if (!privilege.CanDelete(setting))
			{
				return NotAuthorized();
			}

			this.SettingService.Delete(setting);

			return base.RedirectToRoute(AdministrationRoutes.SettingIndex);
		}
	}
}