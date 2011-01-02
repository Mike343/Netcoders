using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Specifications;

namespace Coders.Repositories
{
	public class EntityRepository<T> : IRepository<T> where T : class, IEntity, new()
	{
		private ObjectContext _context;
		private IObjectSet<T> _objectSet;

		private ObjectContext Context
		{
			get
			{
				return _context ?? (_context = GetCurrentUnitOfWork<EntityUnitOfWork>().Context);
			}
		}

		private IObjectSet<T> ObjectSet
		{
			get
			{
				return _objectSet ?? (_objectSet = this.Context.CreateObjectSet<T>());
			}
		}

		public virtual T GetBy(ISpecification<T> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			return specification.SatisfyEntityFrom(this.ObjectSet);
		}

		public virtual T GetById(int id)
		{
			return this.ObjectSet.SingleOrDefault(x => x.Id == id);
		}

		public virtual IList<T> GetAll()
		{
			return this.ObjectSet.ToList();
		}

		public virtual IList<T> GetAll(ISpecification<T> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			return specification.SatisfyEntitiesFrom(this.ObjectSet).ToList();
		}

		public virtual IPagedCollection<T> GetPaged(ISpecification<T> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			var items = specification.SatisfyEntitiesFrom(this.ObjectSet)
				.Skip(specification.First)
				.Take(specification.LimitOrDefault)
				.ToList();

			return new PagedCollection<T>(items, specification.PageOrDefault, specification.LimitOrDefault, items.Count());
		}

		public virtual int Count()
		{
			return this.ObjectSet.Count();
		}

		public virtual int Count(ISpecification<T> specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			return specification.SatisfyEntitiesFrom(this.ObjectSet).Count();
		}

		public virtual void Insert(T entity)
		{
			this.ObjectSet.AddObject(entity);
		}

		public virtual void Update(T entity)
		{

		}

		public virtual void Delete(T entity)
		{
			this.ObjectSet.DeleteObject(entity);
		}

		private static TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : class, IUnitOfWork
		{
			return UnitOfWork.Current as TUnitOfWork;
		}
	}
}