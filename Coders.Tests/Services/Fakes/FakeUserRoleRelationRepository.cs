using System.Collections.Generic;
using System.Linq;
using Coders.Authentication;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeUserRoleRelationRepository : IRepository<UserRoleRelation>
	{
		public FakeUserRoleRelationRepository()
		{
			this.Values = new List<UserRoleRelation>
			{
				new UserRoleRelation { Id = 1, Privilege = Privileges.Create, User = new User { Id = 1 }, Role = new UserRole { Id = 1, Title = "test" } }
			};
		}

		private IList<UserRoleRelation> Values
		{
			get;
			set;
		}

		public UserRoleRelation GetBy(ISpecification<UserRoleRelation> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserRoleRelation GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserRoleRelation> GetAll()
		{
			return this.Values;
		}

		public IList<UserRoleRelation> GetAll(ISpecification<UserRoleRelation> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserRoleRelation> GetPaged(ISpecification<UserRoleRelation> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserRoleRelation>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserRoleRelation> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserRoleRelation entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserRoleRelation entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserRoleRelation entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}