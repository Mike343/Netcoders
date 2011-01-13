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
using Coders.Models.Common.Enums;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users
{
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
		public ActionResult Index(SortUser sort, SortOrder order, int? page)
		{
			var users = this.UserService.GetPaged(new UserSpecification
			{
			    Page = page, 
				Limit = Setting.UserPageLimit.Value,
				Sort = sort,
				Order = order
			});

			return View(Views.Index, users);
		}

		[HttpGet]
		public ActionResult Detail(int id)
		{
			var user = this.UserService.GetById(id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			return View(Views.Detail, user);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(Views.Create, new UserCreate());
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(UserCreate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			var user = this.UserService.Create();

			value.ValueToModel(user);

			this.UserService.Insert(user, value.Preference);

			return Status(Messages.UserAccountCreated);
		}

		[HttpGet, Authorize]
		public ActionResult Update()
		{
			var user = this.UserService.GetById(Identity.Id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			return View(Views.Update, new UserUpdate(user));
		}

		[HttpPost, ValidateAntiForgeryToken, Authorize]
		public ActionResult Update(UserUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			var user = this.UserService.GetById(Identity.Id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			value.ValueToModel(user);

			this.UserService.Update(user);

			ApplicationSession.Destroy(Session);

			value.SuccessMessage(Messages.UserAccountUpdated);

			return View(Views.Update, value);
		}
	}
}