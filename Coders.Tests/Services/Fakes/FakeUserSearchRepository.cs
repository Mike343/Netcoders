using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeUserSearchRepository : IRepository<UserSearch>
	{
		public FakeUserSearchRepository()
		{
			var search = new UserSearch
			{
				Id = 1,
				UserId = 1,
				Title = "test",
				Created = DateTime.Now.AddDays(-1)
			};

			search.Updated = search.Created;

			this.Values = new List<UserSearch> { search };
		}

		private IList<UserSearch> Values
		{
			get;
			set;
		}

		public UserSearch GetBy(ISpecification<UserSearch> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserSearch GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserSearch> GetAll()
		{
			return this.Values;
		}

		public IList<UserSearch> GetAll(ISpecification<UserSearch> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserSearch> GetPaged(ISpecification<UserSearch> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserSearch>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserSearch> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserSearch entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserSearch entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserSearch entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}