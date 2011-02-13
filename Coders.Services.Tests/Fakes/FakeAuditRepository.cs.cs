using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Common;
using Coders.Specifications;

namespace Coders.Services.Tests.Fakes
{
	public class FakeAuditRepository : IRepository<Audit>
	{
		public FakeAuditRepository()
		{
			this.Values = new List<Audit>();
		}

		private IList<Audit> Values
		{
			get;
			set;
		}

		public Audit GetBy(ISpecification<Audit> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public Audit GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<Audit> GetAll()
		{
			return this.Values;
		}

		public IList<Audit> GetAll(ISpecification<Audit> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<Audit> GetPaged(ISpecification<Audit> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<Audit>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<Audit> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(Audit entity)
		{
			this.Values.Add(entity);
		}

		public void Update(Audit entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(Audit entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}