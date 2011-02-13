using Coders.Models.Settings;
using NUnit.Framework;

namespace Coders.Tests.Models.Settings
{
	[TestFixture]
	public class SettingAuditTest
	{
		[Test]
		public void Test_SettingAudit_ValueToAudit()
		{
			var audit = new SettingAudit();

			audit.ValueToAudit(new Setting
			{
			    Title = "test", 
				Group = "test2", 
				Key = "test3", 
				Value = "test4"
			});

			Assert.AreEqual("test", audit.Title, "Title");
			Assert.AreEqual("test2", audit.Group, "Group");
			Assert.AreEqual("test3", audit.Key, "Key");
			Assert.AreEqual("test4", audit.Value, "Value");
		}
	}
}