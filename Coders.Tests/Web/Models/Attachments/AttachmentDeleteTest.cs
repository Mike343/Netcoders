using Coders.Models.Attachments;
using Coders.Web.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Attachments
{
	[TestFixture]
	public class AttachmentDeleteTest
	{
		[Test]
		public void Test_AttachmentDelete()
		{
			var value = new AttachmentDelete(
				new Attachment {Id = 1, FileName = "test"}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
			Assert.IsTrue(value.Soft, "Soft");
		}
	}
}