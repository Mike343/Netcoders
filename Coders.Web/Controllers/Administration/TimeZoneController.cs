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
using Coders.Models.TimeZones;
using Coders.Models.TimeZones.Enums;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Models.TimeZones;
using Coders.Web.Routes;
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Web.Controllers.Administration
{
	[Authorize]
	public class TimeZoneController : SecureDefaultController
	{
		public TimeZoneController(
			IAuditService<TimeZone, TimeZoneAudit> auditService,
			ITimeZoneService timeZoneService)
		{
			this.AuditService = auditService;
			this.TimeZoneService = timeZoneService;
		}

		public IAuditService<TimeZone, TimeZoneAudit> AuditService
		{
			get;
			private set;
		}

		public ITimeZoneService TimeZoneService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(SortTimeZone sort, SortOrder order, int? page)
		{
			var timeZones = this.TimeZoneService.GetPaged(new TimeZoneSpecification
			{
				Page = page,
				Limit = Setting.TimeZonePageLimit.Value,
				Sort = sort,
				Order = order
			});

			var timeZone = timeZones.FirstOrDefault();
			var privilege = new TimeZonePrivilege();

			return privilege.CanView(timeZone) ? base.View(Views.Index, timeZones) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortAudit sort, SortOrder order, int? page, int? id)
		{
			var query = new AuditQuery<TimeZone>(sort, order, page, id);
			var audits = this.AuditService.GetPaged(query.Specification);
			var audit = audits.FirstOrDefault();
			var privilege = new AuditPrivilege();

			return privilege.CanView(audit) ? base.View(Views.History, audits) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var timeZone = this.TimeZoneService.Create();
			var privilege = new TimeZonePrivilege();

			return privilege.CanCreate(timeZone) ? base.View(Views.Create, new TimeZoneCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(TimeZoneCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var timeZone = this.TimeZoneService.Create();
			var privilege = new TimeZonePrivilege();

			if (!privilege.CanCreate(timeZone))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			value.ValueToModel(timeZone);

			this.TimeZoneService.InsertOrUpdate(timeZone);

			var model = new TimeZoneCreateOrUpdate(timeZone);

			model.SuccessMessage(Messages.TimeZoneCreated.FormatInvariant(timeZone.Title));

			return base.View(Views.Update, model);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var timeZone = this.TimeZoneService.GetById(id);

			if (timeZone == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			return privilege.CanUpdate(timeZone) ? base.View(Views.Update, new TimeZoneCreateOrUpdate(timeZone)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(TimeZoneCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var timeZone = this.TimeZoneService.GetById(value.Id);

			if (timeZone == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			if (!privilege.CanUpdate(timeZone))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			value.ValueToModel(timeZone);

			this.TimeZoneService.InsertOrUpdate(timeZone);

			value.SuccessMessage(Messages.TimeZoneUpdated.FormatInvariant(timeZone.Title));

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var timeZone = this.TimeZoneService.GetById(id);

			if (timeZone == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			return privilege.CanDelete(timeZone) ? base.View(Views.Delete, new TimeZoneDelete(timeZone)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(TimeZoneDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var timeZone = this.TimeZoneService.GetById(value.Id);

			if (timeZone == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			if (!privilege.CanDelete(timeZone))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Delete, value);
			}

			this.TimeZoneService.Delete(timeZone);

			return base.RedirectToRoute(AdministrationRoutes.TimeZoneIndex);
		}
	}
}