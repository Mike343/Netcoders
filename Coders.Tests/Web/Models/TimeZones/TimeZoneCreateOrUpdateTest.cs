using Coders.Models.TimeZones;
using Coders.Web.Models.TimeZones;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.TimeZones
{
	[TestFixture]
	public class TimeZoneCreateOrUpdateTest
	{
		[Test]
		public void Test_TimeZoneCreateOrUpdate()
		{
			var value = new TimeZoneCreateOrUpdate(
				new TimeZone
				{
					Id = 1, 
					Title = "test", 
					Display = "test2",
					Offset = 2
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
			Assert.AreEqual("test2", value.Display, "Display");
			Assert.AreEqual(2, value.Offset, "Offset");
		}

		[Test]
		public void Test_TimeZoneCreateOrUpdate_ValueToModel()
		{
			var value = new TimeZoneCreateOrUpdate
			{
				Title = "test",
				Display = "test2",
				Offset = 2
			};

			var country = new TimeZone();

			value.ValueToModel(country);

			Assert.AreEqual("test", value.Title, "Title");
			Assert.AreEqual("test2", value.Display, "Display");
			Assert.AreEqual(2, value.Offset, "Offset");
		}

		[Test]
		public void Test_TimeZoneCreateOrUpdate_Validate()
		{
			var value = new TimeZoneCreateOrUpdate();

			value.Validate();

			Assert.AreEqual(2, value.Errors.Count, "Errors");
		}
	}
}