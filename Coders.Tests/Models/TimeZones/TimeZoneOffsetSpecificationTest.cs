using System.Collections.Generic;
using System.Linq;
using Coders.Models.TimeZones;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.TimeZones
{
	[TestClass]
	public class TimeZoneOffsetSpecificationTest
	{
		private IQueryable<TimeZone> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<TimeZone>
			{
				new TimeZone { Offset = 1.0 }, 
				new TimeZone { Offset = 2.0 }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_TimeZoneOffsetSpecification_SatisfyEntityFrom()
		{
			var specification = new TimeZoneOffsetSpecification(1.0);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1.0, result.Offset);
		}

		[TestMethod]
		public void Test_TimeZoneOffsetSpecification_SatisfyEntitiesFrom()
		{
			var specification = new TimeZoneOffsetSpecification(1.0);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}