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
using Coders.Models.Users;
using Coders.Strings;
using Coders.Web.Models.Users;
#endregion

namespace Coders.Web.Controllers.Users
{
	[Authorize]
	public class PreferenceController : DefaultController
	{
		public PreferenceController(IUserService userService)
		{
			this.UserService = userService;
		}

		public IUserService UserService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Update()
		{
			var user = this.UserService.GetById(Identity.Id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdate(user))
			{
				return NotAuthorized();
			}

			var value = new UserPreferenceUpdate(user.Preference);

			value.Initialize();

			return base.View(Views.Update, value);
		}

		[HttpPost]
		public ActionResult Update(UserPreferenceUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var user = this.UserService.GetById(Identity.Id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserPrivilege();

			if (!privilege.CanUpdate(user))
			{
				return NotAuthorized();
			}

			value.Initialize();

			if (!ModelState.IsValid)
			{
				return base.View(Views.Update, value);
			}

			value.ValueToModel(user.Preference);

			this.UserService.UpdatePreference(user.Preference);

			value.SuccessMessage(Messages.UserAccountPreferenceUpdated);

			return base.View(Views.Update, value);
		}
	}
}