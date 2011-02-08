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
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Models.Attachments;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	public class AttachmentRuleController : SecureDefaultController
	{
		public AttachmentRuleController(
			 IAuditService<AttachmentRule, AttachmentRuleAudit> auditService,
			IAttachmentRuleService attachmentRuleService)
		{
			this.AuditService = auditService;
			this.AttachmentRuleService = attachmentRuleService;
		}

		public IAuditService<AttachmentRule, AttachmentRuleAudit> AuditService
		{
			get;
			private set;
		}

		public IAttachmentRuleService AttachmentRuleService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Index(string group, int? page)
		{
			var query = new AttachmentRuleQuery(group, page);
			var rules = this.AttachmentRuleService.GetPaged(query.Specification);
			var rule = rules.FirstOrDefault();
			var privilege = new AttachmentRulePrivilege();

			return privilege.CanView(rule) ? base.View(Views.Index, rules) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortAudit sort, SortOrder order, int? page, int? id)
		{
			var query = new AuditQuery<AttachmentRule>(sort, order, page, id);
			var audits = this.AuditService.GetPaged(query.Specification);
			var audit = audits.FirstOrDefault();
			var privilege = new AuditPrivilege();

			return privilege.CanView(audit) ? base.View(Views.History, audits) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Create()
		{
			var rule = this.AttachmentRuleService.Create();
			var privilege = new AttachmentRulePrivilege();

			return privilege.CanCreate(rule) ? base.View(Views.Create, new AttachmentRuleCreateOrUpdate()) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Create(AttachmentRuleCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var rule = this.AttachmentRuleService.Create();
			var privilege = new AttachmentRulePrivilege();

			if (!privilege.CanCreate(rule))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Create, value);
			}

			value.ValueToModel(rule);

			this.AttachmentRuleService.InsertOrUpdate(rule);

			var model = new AttachmentRuleCreateOrUpdate(rule);

			model.SuccessMessage(Messages.AttachmentRuleCreated.FormatInvariant(rule.FileType, rule.Group));

			return base.View(Views.Update, model);
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var rule = this.AttachmentRuleService.GetById(id);

			if (rule == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			return privilege.CanUpdate(rule) ? base.View(Views.Update, new AttachmentRuleCreateOrUpdate(rule)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(AttachmentRuleCreateOrUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var rule = this.AttachmentRuleService.GetById(value.Id);

			if (rule == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			if (!privilege.CanUpdate(rule))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Update, value);
			}

			value.ValueToModel(rule);

			this.AttachmentRuleService.InsertOrUpdate(rule);

			value.SuccessMessage(Messages.AttachmentRuleUpdated.FormatInvariant(rule.FileType, rule.Group));

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var rule = this.AttachmentRuleService.GetById(id);

			if (rule == null)
			{
				return base.HttpNotFound();
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

			var rule = this.AttachmentRuleService.GetById(value.Id);

			if (rule == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new AttachmentRulePrivilege();

			if (!privilege.CanDelete(rule))
			{
				return NotAuthorized();
			}

			if (!ModelState.IsValid)
			{
				return View(Views.Delete, value);
			}

			this.AttachmentRuleService.Delete(rule);

			return base.RedirectToRoute(AdministrationRoutes.AttachmentRuleIndex);
		}
	}
}