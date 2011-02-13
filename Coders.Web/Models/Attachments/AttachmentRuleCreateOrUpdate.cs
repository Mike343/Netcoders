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
using Coders.Extensions;
using Coders.Models.Attachments;
using Coders.Strings;
using FluentValidation;
#endregion

namespace Coders.Web.Models.Attachments
{
	public class AttachmentRuleCreateOrUpdate : Value<AttachmentRule>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleCreateOrUpdate"/> class.
		/// </summary>
		public AttachmentRuleCreateOrUpdate()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleCreateOrUpdate"/> class.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public AttachmentRuleCreateOrUpdate(AttachmentRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}

			this.Id = rule.Id;
			this.Group = rule.Group;
			this.FileType = rule.FileType;
			this.FileExtension = rule.FileExtension;
			this.FileSize = rule.FileSize;
			this.FileWidth = rule.FileWidth;
			this.FileHeight = rule.FileHeight;
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
		/// Gets or sets the size of the file.
		/// </summary>
		/// <value>The size of the file.</value>
		public int FileSize
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the width of the file.
		/// </summary>
		/// <value>The width of the file.</value>
		public int FileWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of the file.
		/// </summary>
		/// <value>The height of the file.</value>
		public int FileHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the group.
		/// </summary>
		/// <value>The group.</value>
		public string Group
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>The type of the file.</value>
		public string FileType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the file extension.
		/// </summary>
		/// <value>The file extension.</value>
		public string FileExtension
		{
			get;
			set;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(AttachmentRule entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.FileSize = this.FileSize;
			entity.FileWidth = this.FileWidth;
			entity.FileHeight = this.FileHeight;
			entity.Group = this.Group;
			entity.FileType = this.FileType;
			entity.FileExtension = this.FileExtension;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new AttachmentRuleCreateOrUpdateValidatorCollection().Validate(this);
		}
	}

	public class AttachmentRuleCreateOrUpdateValidatorCollection : AbstractValidator<AttachmentRuleCreateOrUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleCreateOrUpdateValidatorCollection"/> class.
		/// </summary>
		public AttachmentRuleCreateOrUpdateValidatorCollection()
		{
			RuleFor(x => x.Group)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Group));

			RuleFor(x => x.FileType)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.ContentType));

			RuleFor(x => x.FileExtension)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.FileExtension));
		}
	}
}