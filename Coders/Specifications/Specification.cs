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
using System.Linq.Expressions;
#endregion

namespace Coders.Specifications
{
	public class Specification<T> : ISpecification<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Specification&lt;T&gt;"/> class.
		/// </summary>
		public Specification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Specification&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public Specification(Expression<Func<T, bool>> predicate)
		{
			this.Func = null;
			this.Predicate = predicate;
		}

		/// <summary>
		/// Gets or sets the func.
		/// </summary>
		/// <value>The func.</value>
		public Func<T, bool> Func
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the predicate.
		/// </summary>
		/// <value>The predicate.</value>
		public Expression<Func<T, bool>> Predicate
		{
			get; 
			set;
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
		/// Ands the specified left.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns></returns>
		public Specification<T> And(ISpecification<T> left, ISpecification<T> right)
		{
			return new AndSpecification<T>(left, right);
		}

		/// <summary>
		/// Ors the specified left.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns></returns>
		public Specification<T> Or(ISpecification<T> left, ISpecification<T> right)
		{
			return new OrSpecification<T>(left, right);
		}

		/// <summary>
		/// Determines whether [is satisfied by] [the specified obj].
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if [is satisfied by] [the specified obj]; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool IsSatisfiedBy(T entity)
		{
			return this.Predicate.Compile().Invoke(entity);
		}
	}
}