using Coders.Models.Settings;
using Coders.Web.Models.Settings;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Settings
{
	[TestFixture]
	public class SettingCreateOrUpdateTest
	{
		[Test]
		public void Test_SettingCreateOrUpdate()
		{
			var value = new SettingCreateOrUpdate(
				new Setting
				{
					Id = 1, 
					Group = "test", 
					Title = "test2", 
					Key = "test3", 
					Value = "test4"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Group, "Group");
			Assert.AreEqual("test2", value.Title, "Title");
			Assert.AreEqual("test3", value.ItemKey, "ItemKey");
			Assert.AreEqual("test4", value.ItemValue, "ItemValue");
			Assert.AreEqual("test3", value.CurrentKey, "CurrentKey");
		}

		[Test]
		public void Test_SettingCreateOrUpdate_ValueToModel()
		{
			var value = new SettingCreateOrUpdate
			{
				Group = "test",
				Title = "test2",
				ItemKey = "test3",
				ItemValue = "test4"
			};

			var setting = new Setting();

			value.ValueToModel(setting);

			Assert.AreEqual("test", setting.Group, "Group");
			Assert.AreEqual("test2", setting.Title, "Title");
			Assert.AreEqual("test3", setting.Key, "Key");
			Assert.AreEqual("test4", setting.Value, "Value");
		}

		[Test]
		public void Test_SettingCreateOrUpdate_Validate()
		{
			var value = new SettingCreateOrUpdate();

			value.Validate();

			Assert.AreEqual(4, value.Errors.Count, "Errors");
		}
	}
}