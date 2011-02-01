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
using Coders.Models.Countries;
using Coders.Models.Countries.Enums;
using Coders.Models.Logs;
using Coders.Models.Logs.Enums;
using Coders.Models.Settings;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Models.Countries;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	public class CountryController : SecureDefaultController
	{
		public CountryController(
			ILogService logService,
			ICountryService countryService)
		{
			this.LogService = logService;
			this.CountryService = countryService;
		}

		public ILogService LogService
		{
			get;
			private set;
		}

		public ICountryService CountryService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(SortCountry sort, SortOrder order, int? page)
		{
			var countries = this.CountryService.GetPaged(new CountrySpecification
			{
				Page = page, 
				Limit = Setting.CountryPageLimit.Value, 
				Sort = sort, 
				Order = order
			});

			var country = countries.FirstOrDefault();
			var privilege = new CountryPrivilege();

			return privilege.CanView(country) ? base.View(Views.Index, countries) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortLog sort, SortOrder order, int? page, int? id)
		{
			var query = new HistoryQuery(Log.Countries, sort, order, page, id);
			var logs = this.LogService.GetPaged(query.Specification);
			var log = logs.FirstOrDefault();
			var privilege = new LogPrivilege();

			return privilege.CanView(log) ? base.View(Views.History, logs) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var country = this.CountryService.Create();
			var privilege = new CountryPrivilege();

			return privilege.CanCreate(country) ? base.View(Views.Create, new CountryCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(CountryCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Create, value);
			}

			var country = this.CountryService.Create();
			var privilege = new CountryPrivilege();

			if (!privilege.CanCreate(country))
			{
				return NotAuthorized();
			}

			value.ValueToModel(country);

			this.CountryService.InsertOrUpdate(country);

			var model = new CountryCreateOrUpdate(country);

			model.SuccessMessage(Messages.CountryCreated.FormatInvariant(country.Title));

			return base.View(Views.Update, model);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var country = this.CountryService.GetById(id);

			if (country == null)
			{
				return HttpNotFound();
			}

			var privilege = new CountryPrivilege();

			return privilege.CanUpdate(country) ? base.View(Views.Update, new CountryCreateOrUpdate(country)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(CountryCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Update, value);
			}

			var country = this.CountryService.GetById(value.Id);

			if (country == null)
			{
				return HttpNotFound();
			}

			var privilege = new CountryPrivilege();

			if (!privilege.CanUpdate(country))
			{
				return NotAuthorized();
			}

			value.ValueToModel(country);

			this.CountryService.InsertOrUpdate(country);

			value.SuccessMessage(Messages.CountryUpdated.FormatInvariant(country.Title));

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var country = this.CountryService.GetById(id);

			if (country == null)
			{
				return HttpNotFound();
			}

			var privilege = new CountryPrivilege();

			return privilege.CanDelete(country) ? base.View(Views.Delete, new CountryDelete(country)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(CountryDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Delete, value);
			}

			var country = this.CountryService.GetById(value.Id);

			if (country == null)
			{
				return HttpNotFound();
			}

			var privilege = new CountryPrivilege();

			if (!privilege.CanDelete(country))
			{
				return NotAuthorized();
			}

			this.CountryService.Delete(country);

			return base.RedirectToRoute(AdministrationRoutes.CountryIndex);
		}
	}
}