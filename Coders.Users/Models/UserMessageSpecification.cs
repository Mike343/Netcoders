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
using Coders.Specifications;
using Coders.Users.Models.Enums;
#endregion

namespace Coders.Users.Models
{
	public class UserSpecification : Specification<UserMessage>, IUserMessageSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserSpecification"/> class.
		/// </summary>
		public UserSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public UserSpecification(Expression<Func<UserMessage, bool>> predicate)
			: base(predicate)
		{

		}

		/// <summary>
		/// Gets or sets the sort.
		/// </summary>
		/// <value>The sort.</value>
		public SortUserMessage Sort
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
		public override IQueryable<UserMessage> OrderEntities(IQueryable<UserMessage> entities, ISpecification<UserMessage> specification)
		{
			var spec = specification as IUserMessageSpecification;

			if (spec == null)
			{
				return entities;
			}

			switch (spec.Sort)
			{
				case SortUserMessage.Title:
					return base.OrderBy(entities, x => x.Title, spec.Order);
				case SortUserMessage.Read:
					return base.OrderBy(entities, x => x.Read, spec.Order);
				case SortUserMessage.Replied:
					return base.OrderBy(entities, x => x.HasReceiverReplied, spec.Order);
				case SortUserMessage.Forwarded:
					return base.OrderBy(entities, x => x.HasReceiverForwarded, spec.Order);
				default:
					return base.OrderBy(entities, x => x.Created, spec.Order);
			}
		}
	}
}