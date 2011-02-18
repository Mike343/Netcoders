using Coders.Models.Common;
using Coders.Models.TimeZones;
using Coders.Services;
using Coders.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
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

			Assert.AreEqual(1, results.Count, "Count");
			Assert.AreEqual(1, TimeZone.TimeZones.Count, "Cache");
		}

		[Test]
		public void Test_TimeZoneService_Insert()
		{
			var timeZone = new TimeZone
			{
				Title = "Test 123"
			};

			this.TimeZoneService.Insert(timeZone);

			Assert.AreEqual(2, timeZone.Id, "Id");
			Assert.AreEqual("test-123", timeZone.Slug, "Slug");
		}

		[Test]
		public void Test_TimeZoneService_Update()
		{
			var timeZone = this.TimeZoneService.GetById(1);

			timeZone.Title = "test3";

			this.TimeZoneService.Update(timeZone);

			Assert.AreEqual(1, timeZone.Id, "Id");
			Assert.AreEqual("test3", timeZone.Title, "Title");
			Assert.AreEqual("test3", timeZone.Slug, "Slug");
		}

		[Test]
		public void Test_TimeZoneService_InsertOrUpdate()
		{
			var service = this.TimeZoneService;
			var timeZone = new TimeZone { Title = "test3" };

			service.InsertOrUpdate(timeZone);

			Assert.AreEqual(2, timeZone.Id, "Id");
			Assert.AreEqual("test3", timeZone.Title, "Title");
		}

		[Test]
		public void Test_TimeZoneService_InsertOrUpdate_Update()
		{
			var service = this.TimeZoneService;
			var timeZone = service.GetById(1);

			timeZone.Title = "test4";

			service.InsertOrUpdate(timeZone);

			Assert.AreEqual(1, timeZone.Id, "Id");
			Assert.AreEqual("test4", timeZone.Title, "Title");
		}

		[Test]
		public void Test_TimeZoneService_Delete()
		{
			var service = this.TimeZoneService;
			var timeZone = service.GetById(1);

			service.Delete(timeZone);

			Assert.AreEqual(0, service.Count(), "Count");
		}
	}
}