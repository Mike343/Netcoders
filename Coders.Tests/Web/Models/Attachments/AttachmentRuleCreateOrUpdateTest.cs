using Coders.Models.Attachments;
using Coders.Web.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Attachments
{
	[TestFixture]
	public class AttachmentRuleCreateOrUpdateTest
	{
		[Test]
		public void Test_AttachmentRuleCreateOrUpdate()
		{
			var value = new AttachmentRuleCreateOrUpdate(
				new AttachmentRule
				{
					Id = 1,
					Group = "test",
					FileType = "test2",
					FileExtension = "test3",
					FileSize = 2,
					FileWidth = 3,
					FileHeight = 4
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Group, "Group");
			Assert.AreEqual("test2", value.FileType, "FileType");
			Assert.AreEqual("test3", value.FileExtension, "FileExtension");
			Assert.AreEqual(2, value.FileSize, "FileSize");
			Assert.AreEqual(3, value.FileWidth, "FileWidth");
			Assert.AreEqual(4, value.FileHeight, "FileHeight");
		}

		[Test]
		public void Test_AttachmentRuleCreateOrUpdate_ValueToModel()
		{
			var value = new AttachmentRuleCreateOrUpdate
			{
				Group = "test",
				FileType = "test2",
				FileExtension = "test3",
				FileSize = 2,
				FileWidth = 3,
				FileHeight = 4
			};

			var rule = new AttachmentRule();

			value.ValueToModel(rule);

			Assert.AreEqual("test", rule.Group, "Group");
			Assert.AreEqual("test2", rule.FileType, "FileType");
			Assert.AreEqual("test3", rule.FileExtension, "FileExtension");
			Assert.AreEqual(2, rule.FileSize, "FileSize");
			Assert.AreEqual(3, rule.FileWidth, "FileWidth");
			Assert.AreEqual(4, rule.FileHeight, "FileHeight");
		}

		[Test]
		public void Test_AttachmentRuleCreateOrUpdate_Validate()
		{
			var value = new AttachmentRuleCreateOrUpdate();

			value.Validate();

			Assert.AreEqual(3, value.Errors.Count, "Errors");
		}
	}
}