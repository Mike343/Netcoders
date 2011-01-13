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
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users.Administration
{
	[Authorize]
	public class HostSearchController : DefaultController
	{
		public HostSearchController(IUserHostSearchService userHostSearchService)
		{
			this.UserHostSearchService = userHostSearchService;
		}

		public IUserHostSearchService UserHostSearchService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(int id, int? page)
		{
			var search = this.UserHostSearchService.GetById(id);

			if (search == null)
			{
				return HttpNotFound();
			}

			var hosts = this.UserHostSearchService.GetResults(search, new UserHostSearchSpecification
			{
				Page = page, 
				Limit = Setting.UserHostSearchPageLimit.Value
			});

			var privilege = new UserHostSearchPrivilege();

			return privilege.CanViewAny(search) ? View(Views.Index, hosts) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var search = this.UserHostSearchService.Create();
			var privilege = new UserHostSearchPrivilege();

			if (!privilege.CanCreate(search))
			{
				return NotAuthorized();
			}

			var searches = this.UserHostSearchService.GetAll(new UserHostSearchUserSpecification(base.Identity.Id));
			var value = new UserHostSearchCreate();

			value.Initialize(searches);

			return View(Views.Create, value);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(UserHostSearchCreate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				var searches = this.UserHostSearchService.GetAll(new UserHostSearchUserSpecification(base.Identity.Id));

				value.Initialize(searches);

				return View(Views.Create, value);
			}

			var search = this.UserHostSearchService.Create();
			var privilege = new UserHostSearchPrivilege();

			if (!privilege.CanCreate(search))
			{
				return NotAuthorized();
			}

			value.ValueToModel(search);

			this.UserHostSearchService.Insert(search);

			return RedirectToRoute(UsersAdministrationRoutes.HostSearchIndex, new { id = search.Id });
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var search = this.UserHostSearchService.GetById(id);

			if (search == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserHostSearchPrivilege();

			return privilege.CanDelete(search) ? base.View(Views.Delete, new UserHostSearchDelete(search)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(UserHostSearchDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			var search = UserHostSearchService.GetById(value.Id);

			if (search == null)
			{
				return HttpNotFound();
			}

			var privilege = new UserHostSearchPrivilege();

			if (!privilege.CanDelete(search))
			{
				return NotAuthorized();
			}

			this.UserHostSearchService.Delete(search);

			return RedirectToRoute(UsersAdministrationRoutes.HostSearchCreate);
		}
	}
}