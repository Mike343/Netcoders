using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models.Settings;
using Coders.Specifications;

namespace Coders.Tests.Services.Fakes
{
	public class FakeSettingRepository : ISettingRepository
	{
		public FakeSettingRepository()
		{
			this.Values = new List<Setting> { 
				new Setting
				{
					Id = 1,
					Group = "Test",
					Title = "Test2",
					Key = "Test3",
					Value = "Test4"
				}
			};
		}

		private IList<Setting> Values
		{
			get;
			set;
		}

		public Setting GetBy(ISpecification<Setting> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public Setting GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<string> GetGroups()
		{
			var entities = this.Values.AsQueryable()
				.GroupBy(x => x.Group)
				.Select(x => x.Key)
				.Distinct()
				.ToList();

			return entities;
		}

		public IList<Setting> GetAll()
		{
			return this.Values;
		}

		public IList<Setting> GetAll(ISpecification<Setting> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<Setting> GetPaged(ISpecification<Setting> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<Setting>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<Setting> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(Setting entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(Setting entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(Setting entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}