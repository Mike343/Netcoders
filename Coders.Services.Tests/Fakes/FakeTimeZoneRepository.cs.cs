using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models;
using Coders.Models.TimeZones;
using Coders.Specifications;

namespace Coders.Services.Tests.Fakes
{
	public class FakeTimeZoneRepository : IRepository<TimeZone>
	{
		public FakeTimeZoneRepository()
		{
			this.Values = new List<TimeZone> { 
				new TimeZone
				{
					Id = 1,
					Title = "Test",
					Display = "Test2",
					Slug = "Test3",
					Offset = 1
				}
			};
		}

		private IList<TimeZone> Values
		{
			get;
			set;
		}

		public TimeZone GetBy(ISpecification<TimeZone> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public TimeZone GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<TimeZone> GetAll()
		{
			return this.Values;
		}

		public IList<TimeZone> GetAll(ISpecification<TimeZone> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<TimeZone> GetPaged(ISpecification<TimeZone> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<TimeZone>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<TimeZone> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(TimeZone entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(TimeZone entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(TimeZone entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}