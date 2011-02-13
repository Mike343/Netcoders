using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Services.Tests.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class SettingServiceTest
	{
		public ISettingService SettingService
		{
			get
			{
				return new SettingService(
					MockRepository.GenerateMock<IAuditService<Setting, SettingAudit>>(),
					new FakeSettingRepository()
				);
			}
		}

		[Test]
		public void Test_SettingService_GetGroups()
		{
			var results = this.SettingService.GetGroups();

			Assert.AreEqual(1, results.Count, "Count");
			Assert.AreEqual("Test", results[0]);
		}

		[Test]
		public void Test_SettingService_Rebuild()
		{
			this.SettingService.Rebuild();

			Assert.AreEqual("Test4", Setting.GetByKey("Test3"));
		}

		[Test]
		public void Test_SettingService_Insert()
		{
			var setting = new Setting
			{
				Group = "Other"
			};

			this.SettingService.Insert(setting);

			Assert.AreEqual(2, setting.Id);
			Assert.AreEqual("Other", setting.Group, "Group");
		}

		[Test]
		public void Test_SettingService_Update()
		{
			var setting = this.SettingService.GetById(1);

			setting.Group = "Other";

			this.SettingService.Update(setting);

			Assert.AreEqual(1, setting.Id);
			Assert.AreEqual("Other", setting.Group, "Group");
		}

		[Test]
		public void Test_SettingService_Update_ByKey()
		{
			var service = this.SettingService;

			service.Update("Test3", "Different");

			var setting = service.GetById(1);

			Assert.AreEqual(1, setting.Id);
			Assert.AreEqual("Different", setting.Value, "Value");
		}

		[Test]
		public void Test_SettingService_InsertOrUpdate()
		{
			var service = this.SettingService;
			var setting = new Setting { Group = "Other" };

			service.InsertOrUpdate(setting);

			Assert.AreEqual(2, setting.Id);
			Assert.AreEqual("Other", setting.Group, "Group");

			var update = service.GetById(2);

			update.Group = "Other2";

			service.InsertOrUpdate(update);

			Assert.AreEqual(2, update.Id);
			Assert.AreEqual("Other2", update.Group, "Group");
		}

		[Test]
		public void Test_SettingService_Delete()
		{
			var service = this.SettingService;
			var setting = service.GetById(1);

			service.Delete(setting);

			Assert.AreEqual(0, service.Count());
		}
	}
}