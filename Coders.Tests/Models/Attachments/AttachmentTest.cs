using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentTest
	{
		[Test]
		public void Test_Attachment_FullPath()
		{
			var attachment = new Attachment
			{
				FilePath = "directory",
				FileDiskName = "test.zip"
			};

			Assert.AreEqual("directory/test.zip", attachment.FullPath);
		}
	}
}