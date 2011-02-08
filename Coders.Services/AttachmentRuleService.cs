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
using System.Collections.Generic;
using Coders.Models.Attachments;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
#endregion

namespace Coders.Services
{
	public class AttachmentRuleService : EntityService<AttachmentRule>, IAttachmentRuleService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleService"/> class.
		/// </summary>
		/// <param name="auditService">The audit service.</param>
		/// <param name="attachmentRuleRepository">The attachment rule repository.</param>
		public AttachmentRuleService(
			IAuditService<AttachmentRule, AttachmentRuleAudit> auditService,
			IAttachmentRuleRepository attachmentRuleRepository)
			: base(attachmentRuleRepository)
		{
			this.AuditService = auditService;
			this.AttachmentRuleRepository = attachmentRuleRepository;
		}

		/// <summary>
		/// Gets the audit service.
		/// </summary>
		public IAuditService<AttachmentRule, AttachmentRuleAudit> AuditService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the attachment rule repository.
		/// </summary>
		public IAttachmentRuleRepository AttachmentRuleRepository
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets the groups.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetGroups()
		{
			return this.AttachmentRuleRepository.GetGroups();
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(AttachmentRule entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Insert(entity);

			this.AuditService.Audit(entity, AuditAction.Create);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(AttachmentRule entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Update(entity);

			this.AuditService.Audit(entity, AuditAction.Update);
		}

		/// <summary>
		/// Inserts or updates the specified attachment rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void InsertOrUpdate(AttachmentRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}

			if (rule.Id > 0)
			{
				this.Update(rule);
			}
			else
			{
				this.Insert(rule);
			}
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(AttachmentRule entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Delete(entity);

			this.AuditService.Audit(entity, AuditAction.Delete);
		}
	}
}