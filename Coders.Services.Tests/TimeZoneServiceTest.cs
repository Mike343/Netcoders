using Coders.Models.Common;
using Coders.Models.TimeZones;
using Coders.Services.Tests.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class TimeZoneServiceTest
	{
		public ITimeZoneService TimeZoneService
		{
			get
			{
				return new TimeZoneService(
					MockRepository.GenerateMock<IAuditService<TimeZone, TimeZoneAudit>>(),
					new FakeTimeZoneRepository()
				);
			}
		}

		[Test]
		public void Test_TimeZoneService_GetAll()
		{
			var results = this.TimeZoneService.GetAll();

			Assert.AreEqual(1, results.Count);
			Assert.AreEqual(1, TimeZone.TimeZones.Count);
		}

		[Test]
		public void Test_TimeZoneService_Insert()
		{
			var timeZone = new TimeZone();

			this.TimeZoneService.Insert(timeZone);

			Assert.AreEqual(2, timeZone.Id);
		}

		[Test]
		public void Test_TimeZoneService_Update()
		{
			var timeZone = this.TimeZoneService.GetById(1);

			timeZone.Title = "test3";

			this.TimeZoneService.Update(timeZone);

			Assert.AreEqual(1, timeZone.Id);
			Assert.AreEqual("test3", timeZone.Title, "Title");
		}

		[Test]
		public void Test_TimeZoneService_InsertOrUpdate()
		{
			var service = this.TimeZoneService;
			var timeZone = new TimeZone { Title = "test3" };

			service.InsertOrUpdate(timeZone);

			Assert.AreEqual(2, timeZone.Id);
			Assert.AreEqual("test3", timeZone.Title, "Title");

			var update = service.GetById(2);

			update.Title = "test4";

			service.InsertOrUpdate(update);

			Assert.AreEqual(2, update.Id);
			Assert.AreEqual("test4", update.Title, "Title");
		}

		[Test]
		public void Test_TimeZoneService_Delete()
		{
			var service = this.TimeZoneService;
			var timeZone = service.GetById(1);

			service.Delete(timeZone);

			Assert.AreEqual(0, service.Count());
		}
	}
}