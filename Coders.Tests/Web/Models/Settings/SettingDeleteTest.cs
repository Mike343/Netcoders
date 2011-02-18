using Coders.Models.Settings;
using Coders.Web.Models.Settings;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Settings
{
	[TestFixture]
	public class SettingDeleteTest
	{
		[Test]
		public void Test_SettingDelete()
		{
			var value = new SettingDelete(
				new Setting
				{
					Id = 1, 
					Title = "test"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
		}
	}
}