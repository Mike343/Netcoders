using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeUserBanService : IUserBanService
	{
		public UserBan GetBy(ISpecification<UserBan> specification)
		{
			return new UserBan();
		}

		public UserBan GetById(int id)
		{
			return id == 1 ? new UserBan() : null;
		}

		public IList<UserBan> GetAll()
		{
			return new List<UserBan>();
		}

		public IList<UserBan> GetAll(ISpecification<UserBan> specification)
		{
			return new List<UserBan>();
		}

		public IPagedCollection<UserBan> GetPaged(ISpecification<UserBan> specification)
		{
			return new PagedCollection<UserBan>(new List<UserBan>(), 1, 1, 0);
		}

		public UserBan Create()
		{
			return new UserBan();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<UserBan> specification)
		{
			return 0;
		}

		public void Insert(UserBan entity)
		{

		}

		public void Update(UserBan entity)
		{

		}

		public void Delete(UserBan entity)
		{

		}

		public UserBan Check()
		{
			return new UserBan();
		}

		public void InsertOrUpdate(UserBan ban)
		{
			ban.User = new User { Name = string.Empty };
		}

		public void InsertOrUpdate(UserBan ban, string name)
		{
			ban.User = new User { Name = string.Empty };
		}

		public void DeleteExpired()
		{

		}
	}
}