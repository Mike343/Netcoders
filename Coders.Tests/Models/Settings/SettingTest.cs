using System.Collections.Generic;
using Coders.Models.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Settings
{
	[TestClass]
	public class SettingTest
	{
		private IList<Setting> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			this.Values = new List<Setting>
			{
				new Setting {Key = "test.one", Value = "One"},
				new Setting {Key = "test.two", Value = "Two {0}"}
			};
		}

		[TestMethod]
		public void Test_Setting_Rebuild()
		{
			Setting.Rebuild(this.Values);

			Assert.AreEqual("One", Setting.GetByKey("test.one"));
		}
	}
}