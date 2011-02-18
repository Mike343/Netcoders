using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeUserSearchService : IUserSearchService
	{
		public UserSearch GetBy(ISpecification<UserSearch> specification)
		{
			return new UserSearch();
		}

		public UserSearch GetById(int id)
		{
			return id == 1 ? new UserSearch() : null;
		}

		public IList<UserSearch> GetAll()
		{
			return new List<UserSearch>();
		}

		public IList<UserSearch> GetAll(ISpecification<UserSearch> specification)
		{
			return new List<UserSearch>();
		}

		public IPagedCollection<UserSearch> GetPaged(ISpecification<UserSearch> specification)
		{
			return new PagedCollection<UserSearch>(new List<UserSearch>(), 1, 1, 0);
		}

		public IPagedCollection<User> GetResults(UserSearch search, ISpecification<UserSearch> specification)
		{
			return new PagedCollection<User>(new List<User>(), 1, 1, 0);
		}

		public UserSearch Create()
		{
			return new UserSearch();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<UserSearch> specification)
		{
			return 0;
		}

		public void Insert(UserSearch entity)
		{

		}

		public void Update(UserSearch entity)
		{

		}

		public void Delete(UserSearch entity)
		{

		}

		public void Rebuild()
		{

		}

		public void DeleteExpired()
		{

		}
	}
}