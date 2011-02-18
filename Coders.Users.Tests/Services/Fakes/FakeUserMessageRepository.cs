using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;
using Coders.Users.Models;

namespace Coders.Users.Tests.Services.Fakes
{
	public class FakeUserMessageRepository : IRepository<UserMessage>
	{
		public FakeUserMessageRepository()
		{
			var date = DateTime.Now;

			this.Values = new List<UserMessage> { 
				new UserMessage
				{
					Id = 1,
					Title = "test",
					Slug = "test2",
					Body = "test3",
					BodyParsed = "test4",
					Created = date,
					Updated = date,
					Sender = new User { Id = 1 },
					Receiver = new User { Id = 2 }
				}
			};
		}

		private IList<UserMessage> Values
		{
			get;
			set;
		}

		public UserMessage GetBy(ISpecification<UserMessage> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserMessage GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserMessage> GetAll()
		{
			return this.Values;
		}

		public IList<UserMessage> GetAll(ISpecification<UserMessage> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserMessage> GetPaged(ISpecification<UserMessage> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserMessage>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserMessage> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserMessage entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserMessage entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserMessage entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}