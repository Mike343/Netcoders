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

using Coders.Extensions;
using Coders.Models.Attachments;
using Coders.Models.Attachments.Enums;
using Coders.Models.Common.Enums;
using Coders.Models.Settings;
using Coders.Specifications;

#endregion

namespace Coders.Web.Controllers.Administration.Queries
{
	public class AttachmentQuery
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentQuery"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="status">The status.</param>
		/// <param name="sort">The sort.</param>
		/// <param name="order">The order.</param>
		/// <param name="page">The page.</param>
		public AttachmentQuery(string type, string status, SortAttachment sort, SortOrder order, int? page)
		{
			IAttachmentSpecification specification = null;

			if (!string.IsNullOrEmpty(type))
			{
				specification = new AttachmentFileTypeSpecification(type);
			}

			if (!string.IsNullOrEmpty(status))
			{
				if (specification == null)
				{
					specification = new AttachmentStatusSpecification(status.AsEnum<AttachmentStatus>());
				}
				else
				{
					var spec = new AndSpecification<Attachment>(
						specification, 
						new AttachmentStatusSpecification(status.AsEnum<AttachmentStatus>())
					);

					specification = spec as IAttachmentSpecification;
				}
			}

			if (specification == null)
			{
				specification = new AttachmentSpecification();
			}

			specification.Page = page;
			specification.Limit = Setting.AttachmentPageLimit.Value;
			specification.Sort = sort;
			specification.Order = order;

			this.Specification = specification;
		}

		/// <summary>
		/// Gets or sets the specification.
		/// </summary>
		/// <value>The specification.</value>
		public IAttachmentSpecification Specification
		{
			get;
			private set;
		}
	}
}