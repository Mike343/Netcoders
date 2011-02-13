using System.Collections.Generic;
using Coders.Models.Settings;
using NUnit.Framework;

namespace Coders.Tests.Models.Settings
{
	[TestFixture]
	public class SettingTest
	{
		private IList<Setting> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			this.Values = new List<Setting>
			{
				new Setting {Key = "test.one", Value = "One"},
				new Setting {Key = "test.two", Value = "Two {0}"}
			};
		}

		[Test]
		public void Test_Setting_Rebuild()
		{
			Setting.Rebuild(this.Values);

			Assert.AreEqual("One", Setting.GetByKey("test.one"));
		}
	}
}