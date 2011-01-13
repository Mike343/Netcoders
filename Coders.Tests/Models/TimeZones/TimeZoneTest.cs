using System.Collections.Generic;
using Coders.Models.TimeZones;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.TimeZones
{
	[TestClass]
	public class TimeZoneTest
	{
		[TestMethod]
		public void Test_TimeZone_Cache()
		{
			var timeZones = new List<TimeZone>
			{
				new TimeZone { Title = "Central Time" }, 
				new TimeZone { Title = "Eastern Time" }
			};

			TimeZone.Cache(timeZones);

			Assert.AreEqual(2, TimeZone.TimeZones.Count);
		}
	}
}