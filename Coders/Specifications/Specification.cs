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
using Coders.Models.Common.Enums;
#endregion

namespace Coders.Specifications
{
	public class Specification<TEntity> : ISpecification<TEntity>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Specification&lt;TEntity&gt;"/> class.
		/// </summary>
		public Specification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Specification&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public Specification(Expression<Func<TEntity, bool>> predicate)
		{
			this.Predicate = predicate;
		}

		/// <summary>
		/// Gets or sets the page.
		/// </summary>
		/// <value>The page.</value>
		public int? Page
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the page or default.
		/// </summary>
		/// <value>The page or default.</value>
		public int PageOrDefault
		{
			get
			{
				return this.Page.HasValue ? this.Page.Value : 1;
			}
		}

		/// <summary>
		/// Gets or sets the limit.
		/// </summary>
		/// <value>The limit.</value>
		public int Limit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the limit or default.
		/// </summary>
		/// <value>The limit or default.</value>
		public int LimitOrDefault
		{
			get
			{
				return this.Limit > 0 ? this.Limit : int.MaxValue;
			}
		}

		/// <summary>
		/// Gets the first.
		/// </summary>
		/// <value>The first.</value>
		public int First
		{
			get
			{
				if (this.PageOrDefault == 1)
				{
					return (this.PageOrDefault * this.LimitOrDefault) - this.LimitOrDefault;
				}

				return ((this.PageOrDefault * this.LimitOrDefault) - this.LimitOrDefault) + 1;
			}
		}

		/// <summary>
		/// Gets the last.
		/// </summary>
		/// <value>The last.</value>
		public int Last
		{
			get
			{
				if (this.PageOrDefault == 1)
				{
					return (this.First + this.LimitOrDefault);
				}

				return (this.First + this.LimitOrDefault) - 1;
			}
		}

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		/// <value>The order.</value>
		public SortOrder Order
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the predicate.
		/// </summary>
		/// <value>The predicate.</value>
		public Expression<Func<TEntity, bool>> Predicate
		{
			get;
			private set;
		}

		/// <summary>
		/// Ands the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public AndSpecification<TEntity> And(Specification<TEntity> specification)
		{
			return new AndSpecification<TEntity>(this, specification);
		}

		/// <summary>
		/// Ors the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public OrSpecification<TEntity> Or(Specification<TEntity> specification)
		{
			return new OrSpecification<TEntity>(this, specification);
		}

		/// <summary>
		/// Satisfies the entity from.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public virtual TEntity SatisfyEntityFrom(IQueryable<TEntity> query)
		{
			return this.Predicate != null ? query.Where(Predicate).FirstOrDefault() : query.FirstOrDefault();
		}

		/// <summary>
		/// Satisfies the entities from.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> SatisfyEntitiesFrom(IQueryable<TEntity> query)
		{
			if (this.Predicate == null)
			{
				return this.OrderEntities(query, this);
			}

			return this.OrderEntities(query.Where(Predicate), this);
		}

		/// <summary>
		/// Orders the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> OrderEntities(IQueryable<TEntity> entities, ISpecification<TEntity> specification)
		{
			return entities;
		}

		/// <summary>
		/// Orders the specified entities.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="entities">The entities.</param>
		/// <param name="value">The value.</param>
		/// <param name="order">The order.</param>
		/// <returns></returns>
		public virtual IQueryable<TEntity> OrderBy<TValue>(IQueryable<TEntity> entities, Expression<Func<TEntity, TValue>> value, SortOrder order)
		{
			return (order == SortOrder.Descending) ? entities.OrderByDescending(value) : entities.OrderBy(value);
		}
	}
}