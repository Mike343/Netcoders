using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentRuleGroupSpecificationTest
	{
		private IQueryable<AttachmentRule> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<AttachmentRule>
			{
				new AttachmentRule {Group = "Test"}, 
				new AttachmentRule {Group = "Test 2"}
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_AttachmentRuleGroupSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentRuleGroupSpecification("Test");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("Test", result.Group);
		}

		[Test]
		public void Test_AttachmentRuleGroupSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentRuleGroupSpecification("Test");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}