using System.Collections.Generic;
using System.Linq;
using Coders.Models.TimeZones;
using NUnit.Framework;

namespace Coders.Tests.Models.TimeZones
{
	[TestFixture]
	public class TimeZoneOffsetSpecificationTest
	{
		private IQueryable<TimeZone> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<TimeZone>
			{
				new TimeZone { Offset = 1.0 }, 
				new TimeZone { Offset = 2.0 }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_TimeZoneOffsetSpecification_SatisfyEntityFrom()
		{
			var specification = new TimeZoneOffsetSpecification(1.0);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1.0, result.Offset);
		}

		[Test]
		public void Test_TimeZoneOffsetSpecification_SatisfyEntitiesFrom()
		{
			var specification = new TimeZoneOffsetSpecification(1.0);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}