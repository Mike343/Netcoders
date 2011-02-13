using System.Collections.Generic;
using System.Linq;
using Coders.Models.Settings;
using NUnit.Framework;

namespace Coders.Tests.Models.Settings
{
	[TestFixture]
	public class SettingGroupSpecificationTest
	{
		private IQueryable<Setting> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<Setting>
			{
				new Setting { Group = "one" }, 
				new Setting { Group = "two" }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_SettingGroupSpecification_SatisfyEntityFrom()
		{
			var specification = new SettingGroupSpecification("one");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("one", result.Group);
		}

		[Test]
		public void Test_SettingGroupSpecification_SatisfyEntitiesFrom()
		{
			var specification = new SettingGroupSpecification("one");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}