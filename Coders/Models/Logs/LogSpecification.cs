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
using Coders.Models.Logs.Enums;
using Coders.Specifications;
#endregion

namespace Coders.Models.Logs
{
	public class LogSpecification : Specification<Log>, ILogSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogSpecification"/> class.
		/// </summary>
		public LogSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public LogSpecification(Expression<Func<Log, bool>> predicate)
			: base(predicate)
		{

		}

		/// <summary>
		/// Gets or sets the sort.
		/// </summary>
		/// <value>The sort.</value>
		public SortLog Sort
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
		public override IQueryable<Log> OrderEntities(IQueryable<Log> entities, ISpecification<Log> specification)
		{
			var spec = specification as ILogSpecification;

			if (spec == null)
			{
				return entities;
			}

			switch (spec.Sort)
			{
				case SortLog.Group:
					return base.OrderBy(entities, x => x.GroupKey, spec.Order);
				case SortLog.User:
					return base.OrderBy(entities, x => x.User.Name, spec.Order);
				default:
					return base.OrderBy(entities, x => x.Created, spec.Order);
			}
		}
	}
}
