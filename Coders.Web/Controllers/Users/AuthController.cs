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
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Users
{
	public class AuthController : DefaultController
	{
		public AuthController(IUserService userService)
		{
			this.UserService = userService;
		}

		public IUserService UserService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult LogOn()
		{
			var value = new UserAuthentication
			{
				Redirect = Request["returnUrl"]
			};

			return base.View(Views.LogOn, value);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult LogOn(UserAuthentication value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return base.View(Views.LogOn, value);
			}

			var user = this.UserService.GetBy(new UserEmailAddressSpecification(value.EmailAddress));

			this.AuthenticationService.LogOn(user);

			ApplicationSession.Destroy(this.Session);

			var url = value.Redirect;

			if (base.Url.IsLocalUrl(url))
			{
				return base.Redirect(url);
			}

			return base.RedirectToRoute(CommonRoutes.Index);
		}

		[HttpGet]
		public ActionResult LogOff()
		{
			this.AuthenticationService.LogOff();

			return base.RedirectToRoute(CommonRoutes.Index);
		}

		[HttpGet]
		public ActionResult Reset()
		{
			return base.View(Views.Reset, new UserAuthenticationReset());
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Reset(UserAuthenticationReset value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Reset, value);
			}

			var user = this.UserService.GetBy(new UserEmailAddressSpecification(value.EmailAddress));

			this.AuthenticationService.Reset(user);

			return Status(Messages.UserAccountPasswordReset);
		}

		[HttpGet, Authorize]
		public ActionResult Update()
		{
			var user = this.UserService.GetById(Identity.Id);

			if (user == null)
			{
				return base.HttpNotFound();
			}

			return base.View(Views.Update, new UserAuthenticationUpdate { EmailAddress = user.EmailAddress });
		}

		[HttpPost, ValidateAntiForgeryToken, Authorize]
		public ActionResult Update(UserAuthenticationUpdate value)
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

			if (!ModelState.IsValid)
			{
				return base.View(Views.Update, value);
			}

			this.AuthenticationService.Update(user, value.NewPassword);
			this.AuthenticationService.LogOff();

			return base.RedirectToRoute(UsersRoutes.AuthLogOn);
		}
	}
}