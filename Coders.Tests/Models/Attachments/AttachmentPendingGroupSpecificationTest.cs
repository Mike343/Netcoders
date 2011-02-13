using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentPendingGroupSpecificationTest
	{
		private IQueryable<AttachmentPending> Values
		{
			get; 
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<AttachmentPending>
			{
				new AttachmentPending { Group = "Test" }, 
				new AttachmentPending { Group = "Test2" }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_AttachmentPendingGroupSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentPendingGroupSpecification("Test");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("Test", result.Group);
		}

		[Test]
		public void Test_AttachmentPendingGroupSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentPendingGroupSpecification("Test");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}