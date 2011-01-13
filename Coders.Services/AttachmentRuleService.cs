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
using Coders.Models;
using Coders.Models.Attachments;
#endregion

namespace Coders.Services
{
	public class AttachmentRuleService : EntityService<AttachmentRule>, IAttachmentRuleService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleService"/> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		public AttachmentRuleService(IRepository<AttachmentRule> repository)
			: base(repository)
		{

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
	}
}