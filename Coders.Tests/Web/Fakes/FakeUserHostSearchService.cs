using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeUserHostSearchService : IUserHostSearchService
	{
		public UserHostSearch GetBy(ISpecification<UserHostSearch> specification)
		{
			return new UserHostSearch();
		}

		public UserHostSearch GetById(int id)
		{
			return id == 1 ? new UserHostSearch() : null;
		}

		public IList<UserHostSearch> GetAll()
		{
			return new List<UserHostSearch>();
		}

		public IList<UserHostSearch> GetAll(ISpecification<UserHostSearch> specification)
		{
			return new List<UserHostSearch>();
		}

		public IPagedCollection<UserHostSearch> GetPaged(ISpecification<UserHostSearch> specification)
		{
			return new PagedCollection<UserHostSearch>(new List<UserHostSearch>(), 1, 1, 0);
		}

		public IPagedCollection<UserHost> GetResults(UserHostSearch search, ISpecification<UserHostSearch> specification)
		{
			return new PagedCollection<UserHost>(new List<UserHost>(), 1, 1, 0);
		}

		public UserHostSearch Create()
		{
			return new UserHostSearch();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<UserHostSearch> specification)
		{
			return 0;
		}

		public void Insert(UserHostSearch entity)
		{

		}

		public void Update(UserHostSearch entity)
		{

		}

		public void Delete(UserHostSearch entity)
		{

		}

		public void DeleteExpired()
		{

		}
	}
}