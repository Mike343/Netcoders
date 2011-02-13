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
using Coders.Models.Users.Enums;
using Coders.Specifications;
#endregion

namespace Coders.Models.Users
{
	public class UserBanSpecification : Specification<UserBan>, IUserBanSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanSpecification"/> class.
		/// </summary>
		public UserBanSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public UserBanSpecification(Expression<Func<UserBan, bool>> predicate)
			: base(predicate)
		{

		}

		/// <summary>
		/// Gets or sets the sort.
		/// </summary>
		/// <value>The sort.</value>
		public SortUserBan Sort
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
		public override IQueryable<UserBan> OrderEntities(IQueryable<UserBan> entities, ISpecification<UserBan> specification)
		{
			var spec = specification as IUserBanSpecification;

			if (spec == null)
			{
				return entities;
			}

			switch (spec.Sort)
			{
				case SortUserBan.User:
					return base.OrderBy(entities, x => x.User.Name, spec.Order);
				case SortUserBan.Expires:
					return base.OrderBy(entities, x => x.Expire, spec.Order);
				default:
					return base.OrderBy(entities, x => x.Created, spec.Order);
			}
		}
	}
}