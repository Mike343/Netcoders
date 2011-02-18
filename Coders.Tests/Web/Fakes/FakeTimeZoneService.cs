using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.TimeZones;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeTimeZoneService : ITimeZoneService
	{
		public TimeZone GetBy(ISpecification<TimeZone> specification)
		{
			return new TimeZone();
		}

		public TimeZone GetById(int id)
		{
			return id == 1 ? new TimeZone() : null;
		}

		public IList<TimeZone> GetAll()
		{
			return new List<TimeZone> { new TimeZone { Id = 1 } };
		}

		public IList<TimeZone> GetAll(ISpecification<TimeZone> specification)
		{
			return new List<TimeZone> { new TimeZone { Id = 1 } };
		}

		public IPagedCollection<TimeZone> GetPaged(ISpecification<TimeZone> specification)
		{
			return new PagedCollection<TimeZone>(new List<TimeZone> { new TimeZone { Id = 1 } }, 1, 1, 0);
		}

		public TimeZone Create()
		{
			return new TimeZone();
		}

		public int Count()
		{
			return 1;
		}

		public int Count(ISpecification<TimeZone> specification)
		{
			return 1;
		}

		public void Insert(TimeZone entity)
		{

		}

		public void Update(TimeZone entity)
		{

		}

		public void Delete(TimeZone entity)
		{

		}

		public void InsertOrUpdate(TimeZone country)
		{

		}
	}
}