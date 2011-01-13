using Coders.Models.Attachments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Attachments
{
	[TestClass]
	public class AttachmentTest
	{
		[TestMethod]
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