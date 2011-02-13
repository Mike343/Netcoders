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
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users
{
	[Authorize]
	public class AvatarController : DefaultController
	{
		public AvatarController(IUserService userService, IUserAvatarService userAvatarService)
		{
			this.UserService = userService;
			this.UserAvatarService = userAvatarService;
		}

		public IUserService UserService
		{
			get;
			private set;
		}

		public IUserAvatarService UserAvatarService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(int? page)
		{
			var avatars = this.UserAvatarService.GetPaged(new UserAvatarUserSpecification(base.Identity.Id)
			{
				Page = page, 
				Limit = Setting.UserAvatarPageLimit.Value
			});

			var avatar = avatars.FirstOrDefault();
			var privilege = new UserAvatarPrivilege();

			return privilege.CanView(avatar) ? base.View(Views.Index, avatars) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Assign(int id)
		{
			var avatar = this.UserAvatarService.GetById(id);

			if (avatar == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserAvatarPrivilege();

			if (!privilege.CanUpdate(avatar))
			{
				return NotAuthorized();
			}

			var user = this.UserService.GetById(Identity.Id);

			this.UserAvatarService.AssignToUser(user, avatar);

			this.UserService.Update(user);

			return base.RedirectToRoute(UsersRoutes.AvatarIndex);
		}

		[HttpGet]
		public ActionResult Create()
		{
			var avatar = this.UserAvatarService.Create();
			var privilege = new UserAvatarPrivilege();

			return privilege.CanCreate(avatar) ? base.View(Views.Create, new UserAvatarCreate()) : NotAuthorized();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(UserAvatarCreate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var avatar = this.UserAvatarService.Create();
			var privilege = new UserAvatarPrivilege();

			if (!privilege.CanCreate(avatar))
			{
				return NotAuthorized();
			}

			value.File = Request.Files[0];

			if (!ModelState.IsValid)
			{
				return base.View(Views.Create, value);
			}

			this.UserAvatarService.Insert(avatar, value.File);

			return base.RedirectToRoute(UsersRoutes.AvatarIndex);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var avatar = this.UserAvatarService.GetById(id);

			if (avatar == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserAvatarPrivilege();

			return privilege.CanDelete(avatar) ? base.View(Views.Delete, new UserAvatarDelete(avatar)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(UserAvatarDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var avatar = UserAvatarService.GetById(value.Id);

			if (avatar == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new UserAvatarPrivilege();

			if (!privilege.CanDelete(avatar))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.Delete, value);
			}

			var user = this.UserService.GetById(Identity.Id);
			var matched = this.UserAvatarService.RemoveFromUserOnMatch(user, avatar);

			if (matched)
			{
				this.UserService.Update(user);
			}

			this.UserAvatarService.Delete(avatar);

			return base.RedirectToRoute(UsersRoutes.AvatarIndex);
		}
	}
}