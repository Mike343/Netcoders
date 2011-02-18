using System.IO;
using System.Web.Mvc;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Attachments;
using Coders.Web.Controllers;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Controllers
{
	[TestFixture]
	public class AttachmentControllerTest
	{
		public AttachmentController AttachmentController
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(new StandardKernel()));

			var attachment = new Attachment
			{
				FileName = "network.jpg",
				FileDiskName = "network.jpg",
				FilePath = "images",
				FileType = "image/jpeg"
			};

			var service = MockRepository.GenerateMock<IAttachmentService>();

			service.Stub(x => x.GetById(1)).Return(attachment);

			service.Stub(x => x.GetById(2)).Return(new Attachment
			{
				FileName = "fake.jpg",
				FileDiskName = "fake.jpg",
				FilePath = "fake",
				FileType = "fake"
			});

			service.Stub(x => x.GetThumbnail(attachment, 100, 100)).Return(new FileInfo(attachment.FullPath.AsPath()));

			var controller = new AttachmentController(service);

			this.AttachmentController = controller;
		}

		[Test]
		public void Test_AttachmentController_Detail()
		{
			var result = (FileStreamResult)this.AttachmentController.Detail(1);

			Assert.AreEqual("image/jpeg", result.ContentType, "ContentType");
			Assert.AreEqual("network.jpg", result.FileDownloadName, "FileDownloadName");
		}

		[Test]
		public void Test_AttachmentController_Detail_NotFound()
		{
			var result = this.AttachmentController.Detail(0);

			Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
		}

		[Test]
		public void Test_AttachmentController_Detail_File_NotFound()
		{
			var result = this.AttachmentController.Detail(2);

			Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
		}

		[Test]
		public void Test_AttachmentController_Image()
		{
			var result = (FileStreamResult)this.AttachmentController.Image(1, 100, 100);

			Assert.AreEqual("image/jpeg", result.ContentType, "ContentType");
			Assert.AreEqual("network.jpg", result.FileDownloadName, "FileDownloadName");
		}

		[Test]
		public void Test_AttachmentController_Image_NotFound()
		{
			var result = this.AttachmentController.Image(0, 0, 0);

			Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
		}
	}
}