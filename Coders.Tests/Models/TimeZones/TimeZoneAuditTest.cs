using Coders.Models.TimeZones;
using NUnit.Framework;

namespace Coders.Tests.Models.TimeZones
{
	[TestFixture]
	public class TimeZoneAuditTest
	{
		[Test]
		public void Test_TimeZoneAudit_ValueToAudit()
		{
			var audit = new TimeZoneAudit();

			audit.ValueToAudit(new TimeZone
			{
			    Offset = 1.0, 
				Title = "test", 
				Display = "test2"
			});

			Assert.AreEqual(1.0, audit.Offset, "Offset");
			Assert.AreEqual("test", audit.Title, "Title");
			Assert.AreEqual("test2", audit.Display, "Display");
		}
	}
}