using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentAuditTest
	{
		[Test]
		public void Test_AttachmentAudit_ValueToAudit()
		{
			var audit = new AttachmentAudit();

			audit.ValueToAudit(new Attachment { FileName = "test" });

			Assert.AreEqual("test", audit.FileName);
		}
	}
}