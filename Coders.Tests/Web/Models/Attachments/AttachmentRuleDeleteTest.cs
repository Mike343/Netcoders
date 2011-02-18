using Coders.Models.Attachments;
using Coders.Web.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Attachments
{
	[TestFixture]
	public class AttachmentRuleDeleteTest
	{
		[Test]
		public void Test_AttachmentRuleDeleteTest()
		{
			var value = new AttachmentRuleDelete(
				new AttachmentRule
				{
					Id = 1, 
					Group = "test", 
					FileType = "test2"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Group, "Group");
			Assert.AreEqual("test2", value.FileType, "FileType");
		}
	}
}