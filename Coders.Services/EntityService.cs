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
using Coders.Models;
using Coders.Specifications;
#endregion

namespace Coders.Services
{
	public class EntityService<TEntity> : IEntityService<TEntity>
		where TEntity : class, IEntity, new()
	{
		// private fields
		private readonly IRepository<TEntity> _repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityService&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		public EntityService(IRepository<TEntity> repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Gets the entity using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual TEntity GetBy(ISpecification<TEntity> specification)
		{
			return _repository.GetBy(specification);
		}

		/// <summary>
		/// Gets the entity using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public virtual TEntity GetById(int id)
		{
			return _repository.GetById(id);
		}

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll()
		{
			return _repository.GetAll();
		}

		/// <summary>
		/// Gets all entities using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll(ISpecification<TEntity> specification)
		{
			return _repository.GetAll(specification);
		}

		/// <summary>
		/// Gets the entities paged using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IPagedCollection<TEntity> GetPaged(ISpecification<TEntity> specification)
		{
			return _repository.GetPaged(specification);
		}

		/// <summary>
		/// Creates the entity.
		/// </summary>
		/// <returns></returns>
		public virtual TEntity Create()
		{
			return new TEntity();
		}

		/// <summary>
		/// Counts the entities.
		/// </summary>
		/// <returns></returns>
		public virtual int Count()
		{
			return _repository.Count();
		}

		/// <summary>
		/// Counts the entities using specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual int Count(ISpecification<TEntity> specification)
		{
			return _repository.Count(specification);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Insert(TEntity entity)
		{
			_repository.Insert(entity);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Update(TEntity entity)
		{
			_repository.Update(entity);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Delete(TEntity entity)
		{
			_repository.Delete(entity);
		}
	}
}