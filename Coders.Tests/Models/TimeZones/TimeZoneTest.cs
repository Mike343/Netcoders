using System.Collections.Generic;
using Coders.Models.TimeZones;
using NUnit.Framework;

namespace Coders.Tests.Models.TimeZones
{
	[TestFixture]
	public class TimeZoneTest
	{
		[Test]
		public void Test_TimeZone_Cache()
		{
			var timeZones = new List<TimeZone>
			{
				new TimeZone { Title = "Central Time" }
			};

			TimeZone.Cache(timeZones);

			Assert.AreEqual(1, TimeZone.TimeZones.Count);
		}
	}
}