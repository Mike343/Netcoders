using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Specifications;
using Coders.Users.Models;

namespace Coders.Users.Tests.Services.Fakes
{
	public class FakeUserMessageFolderRepository : IRepository<UserMessageFolder>
	{
		public FakeUserMessageFolderRepository()
		{
			this.Values = new List<UserMessageFolder> { 
				new UserMessageFolder
				{
					Id = 1,
					UserId = 1,
					Title = "test",
					Slug = "test2"
				}
			};
		}

		private IList<UserMessageFolder> Values
		{
			get;
			set;
		}

		public UserMessageFolder GetBy(ISpecification<UserMessageFolder> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserMessageFolder GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserMessageFolder> GetAll()
		{
			return this.Values;
		}

		public IList<UserMessageFolder> GetAll(ISpecification<UserMessageFolder> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserMessageFolder> GetPaged(ISpecification<UserMessageFolder> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserMessageFolder>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserMessageFolder> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserMessageFolder entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserMessageFolder entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserMessageFolder entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}