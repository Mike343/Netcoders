using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Countries;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeCountryRepository : IRepository<Country>
	{
		public FakeCountryRepository()
		{
			this.Values = new List<Country> { 
				new Country
				{
					Id = 1,
					Title = "Test",
					Slug = "Test2",
					Code = "Test3"
				}
			};
		}

		private IList<Country> Values
		{
			get;
			set;
		}

		public Country GetBy(ISpecification<Country> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public Country GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<Country> GetAll()
		{
			return this.Values;
		}

		public IList<Country> GetAll(ISpecification<Country> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<Country> GetPaged(ISpecification<Country> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<Country>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<Country> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(Country entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(Country entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(Country entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}