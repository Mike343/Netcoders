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
using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Specifications;
using NHibernate;
using NHibernate.Linq;
#endregion

namespace Coders.Repositories
{
	public class NHibernateEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NHibernateEntityRepository&lt;TEntity&gt;"/> class.
		/// </summary>
		public NHibernateEntityRepository()
		{
			this.Session = ServiceLocator.Current.GetInstance<ISession>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NHibernateEntityRepository&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="session">The session.</param>
		public NHibernateEntityRepository(ISession session)
		{
			this.Session = session;
		}

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>The session.</value>
		public ISession Session
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
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			return specification.SatisfyEntityFrom(this.Session.Query<TEntity>());
		}

		/// <summary>
		/// Gets the entity using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public virtual TEntity GetById(int id)
		{
			return this.Session.Get<TEntity>(id);
		}

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll()
		{
			return this.Session.Query<TEntity>().ToList();
		}

		/// <summary>
		/// Gets all entities using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll(ISpecification<TEntity> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			return specification.SatisfyEntitiesFrom(this.Session.Query<TEntity>()).ToList();
		}

		/// <summary>
		/// Gets the entities paged using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual IPagedCollection<TEntity> GetPaged(ISpecification<TEntity> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			var items = specification.SatisfyEntitiesFrom(this.Session.Query<TEntity>())
				.Skip(specification.First)
				.Take(specification.LimitOrDefault)
				.ToList();

			return new PagedCollection<TEntity>(items, specification.PageOrDefault, specification.LimitOrDefault, items.Count());
		}

		/// <summary>
		/// Counts the entities.
		/// </summary>
		/// <returns></returns>
		public virtual int Count()
		{
			return this.Session.Query<TEntity>().Count();
		}

		/// <summary>
		/// Counts the entities using specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public virtual int Count(ISpecification<TEntity> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			return specification.SatisfyEntitiesFrom(this.Session.Query<TEntity>()).Count();
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Insert(TEntity entity)
		{
			this.Session.SaveOrUpdate(entity);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Update(TEntity entity)
		{
			this.Session.SaveOrUpdate(entity);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Delete(TEntity entity)
		{
			this.Session.Delete(entity);
		}
	}
}