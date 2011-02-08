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
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityService&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		public EntityService(IRepository<TEntity> repository)
		{
			this.Repository = repository;
		}

		/// <summary>
		/// Gets the repository.
		/// </summary>
		/// <value>The repository.</value>
		public IRepository<TEntity> Repository
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets the entity using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual TEntity GetBy(ISpecification<TEntity> specification)
		{
			return this.Repository.GetBy(specification);
		}

		/// <summary>
		/// Gets the entity using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public virtual TEntity GetById(int id)
		{
			return this.Repository.GetById(id);
		}

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll()
		{
			return this.Repository.GetAll();
		}

		/// <summary>
		/// Gets all entities using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll(ISpecification<TEntity> specification)
		{
			return this.Repository.GetAll(specification);
		}

		/// <summary>
		/// Gets the entities paged using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IPagedCollection<TEntity> GetPaged(ISpecification<TEntity> specification)
		{
			return this.Repository.GetPaged(specification);
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
			return this.Repository.Count();
		}

		/// <summary>
		/// Counts the entities using specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual int Count(ISpecification<TEntity> specification)
		{
			return this.Repository.Count(specification);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Insert(TEntity entity)
		{
			this.Repository.Insert(entity);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Update(TEntity entity)
		{
			this.Repository.Update(entity);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Delete(TEntity entity)
		{
			this.Repository.Delete(entity);
		}
	}
}