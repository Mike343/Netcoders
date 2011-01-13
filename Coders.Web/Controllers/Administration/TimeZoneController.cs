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
using Coders.Models.TimeZones;
using Coders.Models.TimeZones.Enums;
using Coders.Web.Models.TimeZones;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	[Authorize]
	public class TimeZoneController : SecureDefaultController
	{
		public TimeZoneController(ITimeZoneService timeZoneService)
		{
			this.TimeZoneService = timeZoneService;
		}

		public ITimeZoneService TimeZoneService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(int? page)
		{
			var timeZones = this.TimeZoneService.GetPaged(new TimeZoneSpecification
			{
				Page = page,
				Limit = Setting.TimeZonePageLimit.Value,
				Sort = SortTimeZone.Offset,
				Order = SortOrder.Ascending
			});

			var timeZone = timeZones.FirstOrDefault();
			var privilege = new TimeZonePrivilege();

			return privilege.CanView(timeZone) ? View(Views.Index, timeZones) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var timeZone = TimeZoneService.Create();
			var privilege = new TimeZonePrivilege();

			return privilege.CanCreate(timeZone) ? View(Views.Create, new TimeZoneCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(TimeZoneCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			var timeZone = TimeZoneService.Create();
			var privilege = new TimeZonePrivilege();

			if (!privilege.CanCreate(timeZone))
			{
				return NotAuthorized();
			}

			value.ValueToModel(timeZone);

			this.TimeZoneService.InsertOrUpdate(timeZone);

			return RedirectToRoute(AdministrationRoutes.TimeZoneUpdate, new { id = timeZone.Id });
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var timeZone = TimeZoneService.GetById(id);

			if (timeZone == null)
			{
				return HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			return privilege.CanUpdate(timeZone) ? View(Views.Update, new TimeZoneCreateOrUpdate(timeZone)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(TimeZoneCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			var timeZone = TimeZoneService.GetById(value.Id);

			if (timeZone == null)
			{
				return HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			if (!privilege.CanUpdate(timeZone))
			{
				return NotAuthorized();
			}

			value.ValueToModel(timeZone);

			this.TimeZoneService.InsertOrUpdate(timeZone);

			return RedirectToRoute(AdministrationRoutes.TimeZoneUpdate, new { id = timeZone.Id });
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var timeZone = this.TimeZoneService.GetById(id);

			if (timeZone == null)
			{
				return HttpNotFound();
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

			if (!ModelState.IsValid)
			{
				return View(Views.Delete, value);
			}

			var timeZone = TimeZoneService.GetById(value.Id);

			if (timeZone == null)
			{
				return HttpNotFound();
			}

			var privilege = new TimeZonePrivilege();

			if (!privilege.CanDelete(timeZone))
			{
				return NotAuthorized();
			}

			this.TimeZoneService.Delete(timeZone);

			return RedirectToRoute(AdministrationRoutes.TimeZoneIndex);
		}
	}
}