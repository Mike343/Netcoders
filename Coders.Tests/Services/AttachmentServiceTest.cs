using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Attachments;
using Coders.Models.Attachments.Enums;
using Coders.Models.Common;
using Coders.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class AttachmentServiceTest
	{
		private HttpPostedFileBase _source;

		public IAttachmentService AttachmentService
		{
			get
			{
				_source = MockRepository.GenerateMock<HttpPostedFileBase>();
				_source.Stub(x => x.ContentLength).Return(12345678);

				var file = MockRepository.GenerateMock<IFileService>();
				var date = DateTime.Now;
				var path = "{0}\\{1}\\{2}".FormatInvariant(date.Year, date.Month, date.Day);

				file.Stub(x => x.Save(_source, path)).Return(new FileResult
				{
					FileName = "test.jpg",
					FileDiskName = "test2.jpg",
					FileType = "test3",
					FileExtension = "jpg",
					FilePath = path,
					FileSize = 12345678
				});

				var image = MockRepository.GenerateMock<IImageService>();

				image.Stub(x => x.GetImageDimensions(_source)).Return(new[] { 200, 200 });

				return new AttachmentService(
					MockRepository.GenerateMock<IAuditService<Attachment, AttachmentAudit>>(),
					file,
					image,
					MockRepository.GenerateMock<IAttachmentRuleService>(),
					MockRepository.GenerateMock<IAttachmentRepository>(),
					MockRepository.GenerateMock<IRepository<AttachmentPending>>()
				);
			}
		}

		[Test]
		public void Test_AttachmentService_GetThumbnail()
		{
			var attachment = new Attachment
			{
				FileDiskName = "test.jpg",
				FilePath = "\\images",
				FileWidth = 200,
				FileHeight = 200
			};

			var result = this.AttachmentService.GetThumbnail(attachment, 100, 100);

			Assert.IsNotNull(result, "Result");
			Assert.AreEqual("C:\\images\\test_100_100.jpg", result.FullName, "FileName");
		}

		[Test]
		public void Test_AttachmentService_GetPath()
		{
			var date = DateTime.Now;
			var path = "{0}\\{1}\\{2}".FormatInvariant(date.Year, date.Month, date.Day);
			var result = this.AttachmentService.GetPath();

			Assert.AreEqual(path, result, "Result");
		}

		[Test]
		public void Test_AttachmentService_Process()
		{
			var pending = new AttachmentPending
			{
				Id = 1,
				UserId = 1,
				Group = "test"
			};

			var pendings = new List<AttachmentPending>();
			var result = this.AttachmentService.Process(pending, pendings, _source);
			var date = DateTime.Now;

			Assert.IsTrue(result, "Result");
			Assert.AreEqual(1, pendings.Count, "Count");
			Assert.AreEqual("test.jpg", pendings[0].Attachment.FileName, "FileName");
			Assert.AreEqual("test2.jpg", pendings[0].Attachment.FileDiskName, "FileDiskName");
			Assert.AreEqual("test3", pendings[0].Attachment.FileType, "FileType");
			Assert.AreEqual("jpg", pendings[0].Attachment.FileExtension, "FileExtension");
			Assert.AreEqual("{0}/{1}/{2}".FormatInvariant(date.Year, date.Month, date.Day), pendings[0].Attachment.FilePath, "FilePath");
			Assert.AreEqual(200, pendings[0].Attachment.FileWidth, "FileWidth");
			Assert.AreEqual(200, pendings[0].Attachment.FileHeight, "FileHeight");
		}

		[Test]
		public void Test_AttachmentService_Create()
		{
			var attachment = this.AttachmentService.Create();

			Assert.IsNotNull(attachment, "NotNull");
			Assert.AreEqual(attachment.Created, attachment.Updated, "Date");
		}

		[Test]
		public void Test_AttachmentService_Update()
		{
			var attachment = this.AttachmentService.Create();
			var date = attachment.Updated;

			Thread.Sleep(1000);

			this.AttachmentService.Update(attachment);

			Assert.AreNotEqual(date, attachment.Updated, "Updated");
		}

		[Test]
		public void Test_AttachmentService_Delete()
		{
			var attachment = this.AttachmentService.Create();

			attachment.Status = AttachmentStatus.Published;

			this.AttachmentService.Delete(attachment, true);

			Assert.AreEqual(AttachmentStatus.Deleted, attachment.Status, "Status");
		}
	}
}