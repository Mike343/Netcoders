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
using Coders.Models.Attachments;
using Coders.Models.Settings;
using Coders.Web.Models.Attachments;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	public class AttachmentRuleController : SecureDefaultController
	{
		public AttachmentRuleController(IAttachmentRuleService attachmentRuleService)
		{
			this.AttachmentRuleService = attachmentRuleService;
		}

		public IAttachmentRuleService AttachmentRuleService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(int? page)
		{
			var rules = AttachmentRuleService.GetPaged(new AttachmentRuleSpecification
			{
				Page = page, 
				Limit = Setting.AttachmentRulePageLimit.Value
			});

			var rule = rules.FirstOrDefault();
			var privilege = new AttachmentRulePrivilege();

			return privilege.CanView(rule) ? View(Views.Index, rules) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var rule = AttachmentRuleService.Create();
			var privilege = new AttachmentRulePrivilege();

			return privilege.CanCreate(rule) ? View(Views.Create, new AttachmentRuleCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(AttachmentRuleCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			var rule = AttachmentRuleService.Create();
			var privilege = new AttachmentRulePrivilege();

			if (!privilege.CanCreate(rule))
			{
				return NotAuthorized();
			}

			value.ValueToModel(rule);

			this.AttachmentRuleService.InsertOrUpdate(rule);

			return RedirectToRoute(AdministrationRoutes.AttachmentRuleUpdate, new { id = rule.Id });
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var rule = AttachmentRuleService.GetById(id);

			if (rule == null)
			{
				return HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			return privilege.CanUpdate(rule) ? View(Views.Update, new AttachmentRuleCreateOrUpdate(rule)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(AttachmentRuleCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			var rule = AttachmentRuleService.GetById(value.Id);

			if (rule == null)
			{
				return HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			if (!privilege.CanUpdate(rule))
			{
				return NotAuthorized();
			}

			value.ValueToModel(rule);

			this.AttachmentRuleService.InsertOrUpdate(rule);

			return RedirectToRoute(AdministrationRoutes.AttachmentRuleUpdate, new { id = rule.Id });
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var rule = this.AttachmentRuleService.GetById(id);

			if (rule == null)
			{
				return HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			return privilege.CanDelete(rule) ? base.View(Views.Delete, new AttachmentRuleDelete(rule)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(AttachmentRuleDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Delete, value);
			}

			var rule = AttachmentRuleService.GetById(value.Id);

			if (rule == null)
			{
				return HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			if (!privilege.CanDelete(rule))
			{
				return NotAuthorized();
			}

			this.AttachmentRuleService.Delete(rule);

			return RedirectToRoute(AdministrationRoutes.AttachmentRuleIndex);
		}
	}
}