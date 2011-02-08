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
using System.Linq;
using System.Linq.Expressions;
using Coders.Models.Attachments.Enums;
using Coders.Specifications;
#endregion

namespace Coders.Models.Attachments
{
	public class AttachmentSpecification : Specification<Attachment>, IAttachmentSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentSpecification"/> class.
		/// </summary>
		public AttachmentSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public AttachmentSpecification(Expression<Func<Attachment, bool>> predicate)
			: base(predicate)
		{

		}

		/// <summary>
		/// Gets the sort.
		/// </summary>
		/// <value>
		/// The sort.
		/// </value>
		public SortAttachment Sort
		{
			get; 
			set;
		}

		/// <summary>
		/// Sorts the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public override IQueryable<Attachment> OrderEntities(IQueryable<Attachment> entities, ISpecification<Attachment> specification)
		{
			var spec = specification as IAttachmentSpecification;

			if (spec == null)
			{
				return entities;
			}

			switch (spec.Sort)
			{
				case SortAttachment.Name:
					return base.OrderBy(entities, x => x.FileName, spec.Order);
				case SortAttachment.Type:
					return base.OrderBy(entities, x => x.FileType, spec.Order);
				case SortAttachment.Size:
					return base.OrderBy(entities, x => x.FileSize, spec.Order);
				case SortAttachment.Created:
					return base.OrderBy(entities, x => x.Created, spec.Order);
				default:
					return base.OrderBy(entities, x => x.Updated, spec.Order);
			}
		}
	}
}