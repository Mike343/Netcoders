using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeUserBanRepository : IRepository<UserBan>
	{
		public FakeUserBanRepository()
		{
			this.Values = new List<UserBan> { 
				new UserBan
				{
					Id = 1,
					Reason = "test",
					IsPermanent = true,
					Expire = DateTime.Now.AddDays(-1),
					Created = DateTime.Now,
					Admin = new User { Id = 2 },
					User = new User { Id = 1 }
				}
			};
		}

		private IList<UserBan> Values
		{
			get;
			set;
		}

		public UserBan GetBy(ISpecification<UserBan> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserBan GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserBan> GetAll()
		{
			return this.Values;
		}

		public IList<UserBan> GetAll(ISpecification<UserBan> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserBan> GetPaged(ISpecification<UserBan> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserBan>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserBan> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserBan entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserBan entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserBan entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}