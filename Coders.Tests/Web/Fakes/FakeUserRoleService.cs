using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeUserRoleService : IUserRoleService
	{
		public UserRole GetBy(ISpecification<UserRole> specification)
		{
			return new UserRole();
		}

		public UserRole GetById(int id)
		{
			return id == 1 ? new UserRole() : null;
		}

		public IList<UserRole> GetAll()
		{
			return new List<UserRole>();
		}

		public IList<UserRole> GetAll(ISpecification<UserRole> specification)
		{
			return new List<UserRole>();
		}

		public IList<UserRoleRelation> GetPrivileges(ISpecification<UserRoleRelation> specification)
		{
			return new List<UserRoleRelation>();
		}

		public IPagedCollection<UserRole> GetPaged(ISpecification<UserRole> specification)
		{
			return new PagedCollection<UserRole>(new List<UserRole>(), 1, 1, 0);
		}

		public UserRole Create()
		{
			return new UserRole();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<UserRole> specification)
		{
			return 0;
		}

		public void Insert(UserRole entity)
		{

		}

		public void Update(UserRole entity)
		{

		}

		public void Delete(UserRole entity)
		{

		}

		public void InsertOrUpdate(UserRole role, IList<UserRoleRelationUpdateValue> privileges)
		{

		}

		public void InsertPrivilege(UserRoleRelation relation)
		{

		}

		public void UpdatePrivileges(User user, IList<UserRoleRelationUpdate> values)
		{

		}
	}
}