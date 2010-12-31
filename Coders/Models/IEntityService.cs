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
using System.Collections.Generic;
using Coders.Collections;
using Coders.Specifications;
#endregion

namespace Coders.Models
{
	public interface IEntityService<T, in TSpecification>
		where T : class, IEntity, new()
		where TSpecification : class, ISpecification<T>, new()
	{
		/// <summary>
		/// Gets the entity using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		T GetBy(TSpecification specification);

		/// <summary>
		/// Gets the entity using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		T GetById(int id);

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns></returns>
		IList<T> GetAll();

		/// <summary>
		/// Gets all entities using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		IList<T> GetAll(TSpecification specification);

		/// <summary>
		/// Gets the entities paged using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		IPagedCollection<T> GetPaged(TSpecification specification);

		/// <summary>
		/// Creates the entity.
		/// </summary>
		/// <returns></returns>
		T Create();

		/// <summary>
		/// Counts the entities.
		/// </summary>
		/// <returns></returns>
		int Count();

		/// <summary>
		/// Counts the entities using specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		int Count(TSpecification specification);

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Insert(T entity);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Update(T entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Delete(T entity);
	}
}