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
using Coders.Models.Attachments;
#endregion

namespace Coders.Web.Models.Attachments
{
	public class AttachmentRuleDelete
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleDelete"/> class.
		/// </summary>
		public AttachmentRuleDelete()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleDelete"/> class.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public AttachmentRuleDelete(AttachmentRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}

			this.Id = rule.Id;
			this.Group = rule.Group;
			this.FileType = rule.FileType;
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		public string Group
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>
		/// The type of the file.
		/// </value>
		public string FileType
		{
			get;
			set;
		}
	}
}