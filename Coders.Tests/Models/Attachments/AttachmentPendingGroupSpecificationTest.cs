using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Attachments
{
	[TestClass]
	public class AttachmentPendingGroupSpecificationTest
	{
		private IQueryable<AttachmentPending> Values
		{
			get; 
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<AttachmentPending>
			{
				new AttachmentPending {Group = "Test"}, 
				new AttachmentPending {Group = "Test 2"}
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_AttachmentPendingGroupSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentPendingGroupSpecification("Test");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("Test", result.Group);
		}

		[TestMethod]
		public void Test_AttachmentPendingGroupSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentPendingGroupSpecification("Test");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}