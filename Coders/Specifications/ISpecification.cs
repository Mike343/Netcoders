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
using System.Linq;
#endregion

namespace Coders.Specifications
{
	public interface ISpecification<TEntity>
	{
		/// <summary>
		/// Gets or sets the page.
		/// </summary>
		/// <value>The page.</value>
		int? Page { get; set; }

		/// <summary>
		/// Gets the page or default.
		/// </summary>
		/// <value>The page or default.</value>
		int PageOrDefault { get; }

		/// <summary>
		/// Gets or sets the limit.
		/// </summary>
		/// <value>The limit.</value>
		int Limit { get; set; }

		/// <summary>
		/// Gets the limit or default.
		/// </summary>
		/// <value>The limit or default.</value>
		int LimitOrDefault { get; }

		/// <summary>
		/// Gets the first.
		/// </summary>
		/// <value>The first.</value>
		int First { get; }

		/// <summary>
		/// Gets the last.
		/// </summary>
		/// <value>The last.</value>
		int Last { get; }

		/// <summary>
		/// Satisfies the entity from.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		TEntity SatisfyEntityFrom(IQueryable<TEntity> query);

		/// <summary>
		/// Satisfies the entities from.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		IQueryable<TEntity> SatisfyEntitiesFrom(IQueryable<TEntity> query);
	}
}