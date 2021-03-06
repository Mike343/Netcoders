﻿using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeUserPreferenceRepository : IRepository<UserPreference>
	{
		public FakeUserPreferenceRepository()
		{
			this.Values = new List<UserPreference>
			{
				new UserPreference { Id = 1 }
			};
		}

		private IList<UserPreference> Values
		{
			get;
			set;
		}

		public UserPreference GetBy(ISpecification<UserPreference> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public UserPreference GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<UserPreference> GetAll()
		{
			return this.Values;
		}

		public IList<UserPreference> GetAll(ISpecification<UserPreference> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<UserPreference> GetPaged(ISpecification<UserPreference> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<UserPreference>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<UserPreference> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(UserPreference entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(UserPreference entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(UserPreference entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}