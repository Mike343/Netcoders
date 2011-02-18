using System.Collections.Generic;
using System.Web;
using Coders.Collections;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeUserAvatarService : IUserAvatarService
	{
		public UserAvatar GetBy(ISpecification<UserAvatar> specification)
		{
			return new UserAvatar();
		}

		public UserAvatar GetById(int id)
		{
			return id == 1 ? new UserAvatar() : null;
		}

		public IList<UserAvatar> GetAll()
		{
			return new List<UserAvatar>();
		}

		public IList<UserAvatar> GetAll(ISpecification<UserAvatar> specification)
		{
			return new List<UserAvatar>();
		}

		public IPagedCollection<UserAvatar> GetPaged(ISpecification<UserAvatar> specification)
		{
			return new PagedCollection<UserAvatar>(new List<UserAvatar>(), 1, 10, 0);
		}

		public UserAvatar Create()
		{
			return new UserAvatar();
		}

		public int Count()
		{
			return 1;
		}

		public int Count(ISpecification<UserAvatar> specification)
		{
			return 1;
		}

		public void Insert(UserAvatar entity)
		{

		}

		public void Update(UserAvatar entity)
		{

		}

		public void Delete(UserAvatar entity)
		{

		}

		public void AssignToUser(User user, UserAvatar avatar)
		{

		}

		public bool RemoveFromUserOnMatch(User user, UserAvatar avatar)
		{
			return true;
		}

		public void Insert(UserAvatar avatar, HttpPostedFileBase file)
		{

		}
	}
}