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
	public class AttachmentUpdate : Value<Attachment>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentUpdate"/> class.
		/// </summary>
		public AttachmentUpdate()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentUpdate"/> class.
		/// </summary>
		/// <param name="attachment">The attachment.</param>
		public AttachmentUpdate(Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}

			this.Id = attachment.Id;
			this.FileName = attachment.FileName;
			this.Attachment = attachment;
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>
		/// The id.
		/// </value>
		public int Id
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the attachment.
		/// </summary>
		public Attachment Attachment
		{
			get; 
			private set;
		}

		/// <summary>
		/// Initializes the specified attachment.
		/// </summary>
		/// <param name="attachment">The attachment.</param>
		public void Initialize(Attachment attachment)
		{
			this.Attachment = attachment;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(Attachment entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.FileName = this.FileName;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new AttachmentUpdateValidatorCollection().Validate(this);
		}
	}

	public class AttachmentUpdateValidatorCollection : AbstractValidator<AttachmentUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentUpdateValidatorCollection"/> class.
		/// </summary>
		public AttachmentUpdateValidatorCollection()
		{
			RuleFor(x => x.FileName)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.FileName));
		}
	}
}