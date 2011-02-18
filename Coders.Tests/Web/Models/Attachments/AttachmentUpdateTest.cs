using Coders.Models.Attachments;
using Coders.Web.Models.Attachments;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Attachments
{
	[TestFixture]
	public class AttachmentUpdateTest
	{
		[Test]
		public void Test_AttachmentUpdate()
		{
			var value = new AttachmentUpdate(
				new Attachment
				{
					Id = 1, 
					FileName = "test"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.FileName, "FileName");
			Assert.IsNotNull(value.Attachment, "Attachment");
		}

		[Test]
		public void Test_AttachmentUpdate_Initialize()
		{
			var value = new AttachmentUpdate();

			value.Initialize(new Attachment());

			Assert.IsNotNull(value.Attachment, "Attachment");
		}

		[Test]
		public void Test_AttachmentUpdate_ValueToModel()
		{
			var value = new AttachmentUpdate
			{
				FileName = "test"
			};

			var attachment = new Attachment();

			value.ValueToModel(attachment);

			Assert.AreEqual("test", value.FileName, "FileName");
		}

		[Test]
		public void Test_AttachmentUpdate_Validate()
		{
			var value = new AttachmentUpdate();

			value.Validate();

			Assert.AreEqual(1, value.Errors.Count, "Errors");
		}
	}
}