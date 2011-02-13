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
using Coders.Web.Extensions;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users
{
	[Authorize]
	public class SearchController : DefaultController
	{
		public SearchController(IUserSearchService userSearchService)
		{
			this.UserSearchService = userSearchService;
		}

		public IUserSearchService UserSearchService
		{
			get; 
			private set;
		}

		[HttpGet]
		public ActionResult Index(int id, int? page)
		{
			var search = this.UserSearchService.GetById(id);

			if (search == null)
			{
				return HttpNotFound();
			}

			var users = this.UserSearchService.GetResults(search, new UserSearchSpecification
			{
				Page = page, 
				Limit = Setting.UserSearchPageLimit.Value
			});

			return View(Views.Index, users);
		}

		[HttpGet]
		public ActionResult Create()
		{
			var search = this.UserSearchService.Create();
			var privilege = new UserSearchPrivilege();

			if (!privilege.CanCreate(search))
			{
				return NotAuthorized();
			}

			var searches = this.UserSearchService.GetAll(new UserSearchUserSpecification(base.Identity.Id));
			var value = new UserSearchCreate();

			value.Initialize(searches);

			return View(Views.Create, value);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(UserSearchCreate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var search = this.UserSearchService.Create();
			var privilege = new UserSearchPrivilege();

			if (!privilege.CanCreate(search))
			{
				return NotAuthorized();
			}

			value.Validate();

			if (value.IsValid)
			{
				value.ValueToModel(search);

				this.UserSearchService.Insert(search);

				return RedirectToRoute(UsersRoutes.SearchIndex, new { id = search.Id });
			}

			value.CopyToModel(ModelState);

			var searches = this.UserSearchService.GetAll(new UserSearchUserSpecification(base.Identity.Id));

			value.Initialize(searches);

			return View(Views.Create, value);
		}
	}
}