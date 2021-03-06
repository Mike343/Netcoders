﻿#region License
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
using System.Linq;
using Coders.Extensions;
#endregion

namespace Coders.Specifications
{
	public class OrSpecification<TEntity> : CompositeSpecification<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OrSpecification&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
			: base(left, right)
		{

		}

		/// <summary>
		/// Satisfies the entity from.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public override TEntity SatisfyEntityFrom(IQueryable<TEntity> query)
		{
			return SatisfyEntitiesFrom(query).FirstOrDefault();
		}

		/// <summary>
		/// Satisfies the entities from.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public override IQueryable<TEntity> SatisfyEntitiesFrom(IQueryable<TEntity> query)
		{
			return query.Where(base.Left.Predicate.Or(base.Right.Predicate));
		}
	}
}