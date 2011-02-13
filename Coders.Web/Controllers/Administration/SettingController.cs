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
using Coders.Models.Settings;
using Coders.Models.Settings.Enums;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Extensions;
using Coders.Web.Models.Settings;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	public class SettingController : SecureDefaultController
	{
		public SettingController(
			IAuditService<Setting, SettingAudit> auditService, 
			ISettingService settingService)
		{
			this.AuditService = auditService;
			this.SettingService = settingService;
		}

		public IAuditService<Setting, SettingAudit> AuditService
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
		public ActionResult History(SortAudit sort, SortOrder order, int? page, int? id)
		{
			var query = new AuditQuery<Setting>(sort, order, page, id);
			var audits = this.AuditService.GetPaged(query.Specification);
			var audit = audits.FirstOrDefault();
			var privilege = new AuditPrivilege();

			return privilege.CanView(audit) ? base.View(Views.History, audits) : NotAuthorized();
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

			var setting = this.SettingService.Create();
			var privilege = new SettingPrivilege();

			if (!privilege.CanCreate(setting))
			{
				return NotAuthorized();
			}

			value.Validate();

			if (value.IsValid)
			{
				value.ValueToModel(setting);

				this.SettingService.InsertOrUpdate(setting);
				this.SettingService.Rebuild();

				value = new SettingCreateOrUpdate(setting);
				value.SuccessMessage(Messages.SettingCreated.FormatInvariant(setting.Title));
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

			value.Validate();

			if (value.IsValid)
			{
				value.ValueToModel(setting);

				this.SettingService.InsertOrUpdate(setting);
				this.SettingService.Rebuild();

				value.SuccessMessage(Messages.SettingUpdated.FormatInvariant(setting.Title));
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