using Coders.Models.TimeZones;
using Coders.Web.Models.TimeZones;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.TimeZones
{
	[TestFixture]
	public class TimeZoneDeleteTest
	{
		[Test]
		public void Test_TimeZoneDelete()
		{
			var value = new TimeZoneDelete(
				new TimeZone
				{
					Id = 1, 
					Display = "test"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
		}
	}
}