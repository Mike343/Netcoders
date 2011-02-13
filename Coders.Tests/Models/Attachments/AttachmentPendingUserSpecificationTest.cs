using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentPendingUserSpecificationTest
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
				new AttachmentPending { UserId = 1 }, 
				new AttachmentPending { UserId = 2 }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_AttachmentPendingUserSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentPendingUserSpecification(1);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.UserId);
		}

		[Test]
		public void Test_AttachmentPendingUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentPendingUserSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}