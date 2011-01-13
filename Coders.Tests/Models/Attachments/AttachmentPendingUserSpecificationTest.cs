using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Attachments
{
	[TestClass]
	public class AttachmentPendingUserSpecificationTest
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
				new AttachmentPending { UserId = 1 }, 
				new AttachmentPending { UserId = 2 }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_AttachmentPendingUserSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentPendingUserSpecification(1);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.UserId);
		}

		[TestMethod]
		public void Test_AttachmentPendingUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentPendingUserSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}