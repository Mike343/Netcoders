using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Countries;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeCountryService : ICountryService
	{
		public Country GetBy(ISpecification<Country> specification)
		{
			return new Country();
		}

		public Country GetById(int id)
		{
			return id == 1 ? new Country() : null;
		}

		public IList<Country> GetAll()
		{
			return new List<Country> { new Country { Id = 1 } };
		}

		public IList<Country> GetAll(ISpecification<Country> specification)
		{
			return new List<Country> { new Country { Id = 1 } };
		}

		public IPagedCollection<Country> GetPaged(ISpecification<Country> specification)
		{
			return new PagedCollection<Country>(new List<Country> { new Country { Id = 1 } }, 1, 1, 1);
		}

		public Country Create()
		{
			return new Country();
		}

		public int Count()
		{
			return 1;
		}

		public int Count(ISpecification<Country> specification)
		{
			return 1;
		}

		public void Insert(Country entity)
		{

		}

		public void Update(Country entity)
		{

		}

		public void Delete(Country entity)
		{

		}

		public void InsertOrUpdate(Country country)
		{
	
		}
	}
}