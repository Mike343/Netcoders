using System.Collections.Generic;
using System.Linq;
using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentFileTypeSpecificationTest
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
				new Attachment { FileType = "test"}, 
				new Attachment { FileType = "test2"}
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_AttachmentFileTypeSpecification_SatisfyEntityFrom()
		{
			var specification = new AttachmentFileTypeSpecification("test");
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("test", result.FileType);
		}

		[Test]
		public void Test_AttachmentFileTypeSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AttachmentFileTypeSpecification("test");
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}
