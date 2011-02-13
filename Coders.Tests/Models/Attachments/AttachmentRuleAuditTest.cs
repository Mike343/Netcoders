using Coders.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Models.Attachments
{
	[TestFixture]
	public class AttachmentRuleAuditTest
	{
		[Test]
		public void Test_AttachmentRuleAudit_ValueToAudit()
		{
			var audit = new AttachmentRuleAudit();

			audit.ValueToAudit(new AttachmentRule
			{
				FileSize = 10,
				FileWidth = 12,
				FileHeight = 14,
				Group = "test",
				FileType = "test2",
				FileExtension = "test3"
			});

			Assert.AreEqual(10, audit.FileSize, "FileSize");
			Assert.AreEqual(12, audit.FileWidth, "FileWidth");
			Assert.AreEqual(14, audit.FileHeight, "FileHeight");
			Assert.AreEqual("test", audit.Group, "Group");
			Assert.AreEqual("test2", audit.FileType, "FileType");
			Assert.AreEqual("test3", audit.FileExtension, "FileExtension");
		}
	}
}