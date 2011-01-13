using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Attachments
{
	[TestClass]
	public class AttachmentRuleGroupSpecificationTest
	{
		private IQueryable<AttachmentRule> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<AttachmentRule>
			{
				new AttachmentRule {Group = "Test"}, 
				new AttachmentRule {Group = "Test 2"}
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_AttachmentRuleGroupSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentRuleGroupSpecification("Test");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("Test", result.Group);
		}

		[TestMethod]
		public void Test_AttachmentRuleGroupSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentRuleGroupSpecification("Test");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}