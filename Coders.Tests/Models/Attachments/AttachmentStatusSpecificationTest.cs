using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using Coders.Models.Attachments.Enums;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentStatusSpecificationTest
	{
		private IQueryable<Attachment> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<Attachment>
			{
				new Attachment { Status = AttachmentStatus.Published }, 
				new Attachment { Status = AttachmentStatus.Deleted }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_AttachmentStatusSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentStatusSpecification(AttachmentStatus.Published);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(AttachmentStatus.Published, result.Status);
		}

		[Test]
		public void Test_AttachmentStatusSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentStatusSpecification(AttachmentStatus.Published);
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}