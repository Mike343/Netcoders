using System.Collections.Generic;
using System.Linq;
using Coders.Models.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Settings
{
	[TestClass]
	public class SettingKeySpecificationTest
	{
		private IQueryable<Setting> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<Setting>
			{
				new Setting { Key = "one" }, 
				new Setting { Key = "two" }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_SettingKeySpecification_SatisfyEntityFrom()
		{
			var specification = new SettingKeySpecification("one");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("one", result.Key);
		}

		[TestMethod]
		public void Test_SettingKeySpecification_SatisfyEntitiesFrom()
		{
			var specification = new SettingKeySpecification("one");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}