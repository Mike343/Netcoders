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
using Coders.Models.TimeZones.Enums;
using Coders.Specifications;
#endregion

namespace Coders.Models.TimeZones
{
	public class TimeZoneSpecification : Specification<TimeZone>, ITimeZoneSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneSpecification"/> class.
		/// </summary>
		public TimeZoneSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public TimeZoneSpecification(Expression<Func<TimeZone, bool>> predicate)
			: base(predicate)
		{

		}

		/// <summary>
		/// Gets or sets the sort.
		/// </summary>
		/// <value>The sort.</value>
		public SortTimeZone Sort
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
		public override IQueryable<TimeZone> OrderEntities(IQueryable<TimeZone> entities, ISpecification<TimeZone> specification)
		{
			var spec = specification as ITimeZoneSpecification;

			if (spec == null)
			{
				return entities;
			}

			switch (spec.Sort)
			{
				case SortTimeZone.Title:
					return base.OrderBy(entities, x => x.Title, spec.Order);
				case SortTimeZone.Display:
					return base.OrderBy(entities, x => x.Display, spec.Order);
				default:
					return base.OrderBy(entities, x => x.Offset, spec.Order);
			}
		}
	}
}