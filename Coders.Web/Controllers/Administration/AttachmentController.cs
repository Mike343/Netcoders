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
using Coders.Models.Attachments.Enums;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Strings;
using Coders.Web.Controllers.Administration.Queries;
using Coders.Web.Extensions;
using Coders.Web.Models.Attachments;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Controllers.Administration
{
	public class AttachmentController : SecureDefaultController
	{
		public AttachmentController(
			IAuditService<Attachment, AttachmentAudit> auditService, 
			IAttachmentService attachmentService)
		{
			this.AuditService = auditService;
			this.AttachmentService = attachmentService;
		}

		public IAuditService<Attachment, AttachmentAudit> AuditService
		{
			get;
			private set;
		}

		public IAttachmentService AttachmentService
		{
			get; 
			set;
		}

		[HttpGet]
		public ActionResult Index(string type, string status, SortAttachment sort, SortOrder order, int? page)
		{
			var query = new AttachmentQuery(type, status, sort, order, page);
			var attachments = this.AttachmentService.GetPaged(query.Specification);
			var attachment = attachments.FirstOrDefault();
			var privilege = new AttachmentPrivilege();

			return privilege.CanView(attachment) ? base.View(Views.Index, attachments) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult History(SortAudit sort, SortOrder order, int? page, int? id)
		{
			var query = new AuditQuery<Attachment>(sort, order, page, id);
			var audits = this.AuditService.GetPaged(query.Specification);
			var audit = audits.FirstOrDefault();
			var privilege = new AuditPrivilege();

			return privilege.CanView(audit) ? base.View(Views.History, audits) : NotAuthorized();
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			var attachment = this.AttachmentService.GetById(id);

			if (attachment == null)
			{
				return HttpNotFound();
			}

			var privilege = new AttachmentPrivilege();

			return privilege.CanUpdate(attachment) ? base.View(Views.Update, new AttachmentUpdate(attachment)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Update(AttachmentUpdate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var attachment = this.AttachmentService.GetById(value.Id);

			if (attachment == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new AttachmentPrivilege();

			if (!privilege.CanUpdate(attachment))
			{
				return NotAuthorized();
			}

			value.Validate();

			if (value.IsValid)
			{
				value.ValueToModel(attachment);

				this.AttachmentService.Update(attachment);

				value.SuccessMessage(Messages.AttachmentUpdated);
			}
			else
			{
				value.CopyToModel(ModelState);
			}

			value.Initialize(attachment);

			return base.View(Views.Update, value);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var attachment = this.AttachmentService.GetById(id);

			if (attachment == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new AttachmentPrivilege();

			return privilege.CanDelete(attachment) ? base.View(Views.Delete, new AttachmentDelete(attachment)) : NotAuthorized();
		}

		[HttpPost]
		public ActionResult Delete(AttachmentDelete value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var attachment = this.AttachmentService.GetById(value.Id);

			if (attachment == null)
			{
				return base.HttpNotFound();
			}

			var privilege = new AttachmentPrivilege();

			if (!privilege.CanDelete(attachment))
			{
				return NotAuthorized();
			}

			this.AttachmentService.Delete(attachment, value.Soft);

			return base.RedirectToRoute(AdministrationRoutes.AttachmentIndex);
		}
	}
}