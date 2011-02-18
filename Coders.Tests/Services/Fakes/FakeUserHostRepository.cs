using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeUserHostRepository : IRepository<UserHost>
	{
		public FakeUserHostRepository()
		{
			this.Values = new List<UserHost> { new UserHost { Id = 1, HostAddress = "2.8.4.16", User = new User { Id = 1 } }};
		}

		private IList<UserHost> Values
		{
			get;
			set;
		}

		public UserHost GetBy(ISpecification<UserHost> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserHost GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserHost> GetAll()
		{
			return this.Values;
		}

		public IList<UserHost> GetAll(ISpecification<UserHost> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserHost> GetPaged(ISpecification<UserHost> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserHost>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserHost> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserHost entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserHost entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserHost entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}