using System.Collections.Generic;
using System.Linq;
using Coders.Models.Settings;
using NUnit.Framework;

namespace Coders.Tests.Models.Settings
{
	[TestFixture]
	public class SettingKeySpecificationTest
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
				new Setting { Key = "one" }, 
				new Setting { Key = "two" }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_SettingKeySpecification_SatisfyEntityFrom()
		{
			var specification = new SettingKeySpecification("one");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("one", result.Key);
		}

		[Test]
		public void Test_SettingKeySpecification_SatisfyEntitiesFrom()
		{
			var specification = new SettingKeySpecification("one");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}