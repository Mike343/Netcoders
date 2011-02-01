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
using Coders.Repositories.Extension;
using Coders.Specifications;
using NHibernate;
using NHibernate.Linq;
#endregion

namespace Coders.Repositories
{
	public class NHibernateRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NHibernateRepository&lt;TEntity&gt;"/> class.
		/// </summary>
		public NHibernateRepository()
		{
			this.Session = ServiceLocator.Current.GetInstance<ISession>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NHibernateRepository&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="session">The session.</param>
		public NHibernateRepository(ISession session)
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

			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var entity = specification.SatisfyEntityFrom(session.Query<TEntity>());

				transaction.Commit();

				return entity;
			}
		}

		/// <summary>
		/// Gets the entity using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public virtual TEntity GetById(int id)
		{
			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var entity = session.Get<TEntity>(id);

				transaction.Commit();

				return entity;
			}
		}

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns></returns>
		public virtual IList<TEntity> GetAll()
		{
			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var entities = session.Query<TEntity>()
					.FetchingStrategy()
					.ToList();

				transaction.Commit();

				return entities;
			}
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

			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var entities = specification.SatisfyEntitiesFrom(session.Query<TEntity>())
					.FetchingStrategy()
					.ToList();

				transaction.Commit();

				return entities;
			}
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

			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var entities = specification.SatisfyEntitiesFrom(session.Query<TEntity>());
				var count = entities.Count();

				var results = ((specification.PageOrDefault > 1) ? entities.Skip(specification.First) : entities)
					.Take(specification.LimitOrDefault)
					.FetchingStrategy()
					.ToList();

				transaction.Commit();

				return new PagedCollection<TEntity>(results, specification.PageOrDefault, specification.LimitOrDefault, count);
			}
		}

		/// <summary>
		/// Counts the entities.
		/// </summary>
		/// <returns></returns>
		public virtual int Count()
		{
			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var count = session.Query<TEntity>().Count();

				transaction.Commit();

				return count;
			}
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

			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				var count = specification.SatisfyEntitiesFrom(session.Query<TEntity>()).Count();

				transaction.Commit();

				return count;
			}
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Insert(TEntity entity)
		{
			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				session.SaveOrUpdate(entity);

				transaction.Commit();
			}
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Update(TEntity entity)
		{
			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				session.SaveOrUpdate(entity);

				transaction.Commit();
			}
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Delete(TEntity entity)
		{
			var session = this.Session;

			using (var transaction = session.BeginTransaction())
			{
				session.Delete(entity);

				transaction.Commit();
			}
		}
	}
}