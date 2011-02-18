using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeUserHostSearchRepository : IRepository<UserHostSearch>
	{
		public FakeUserHostSearchRepository()
		{
			var search = new UserHostSearch
			{
				Id = 1,
				UserId = 1,
				Title = "test",
				Created = DateTime.Now.AddDays(-1)
			};

			search.Updated = search.Created;

			this.Values = new List<UserHostSearch> { search };
		}

		private IList<UserHostSearch> Values
		{
			get;
			set;
		}

		public UserHostSearch GetBy(ISpecification<UserHostSearch> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserHostSearch GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserHostSearch> GetAll()
		{
			return this.Values;
		}

		public IList<UserHostSearch> GetAll(ISpecification<UserHostSearch> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserHostSearch> GetPaged(ISpecification<UserHostSearch> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserHostSearch>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserHostSearch> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserHostSearch entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserHostSearch entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserHostSearch entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}