using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class ImageServiceTest
	{
		public IImageService ImageService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			Setting.Rebuild(new List<Setting>
			{
			    new Setting { Key = "image.extension", Value = "jpg,gif,png" }
			});

			this.ImageService = new ImageService();
		}

		[Test]
		public void Test_ImageService_IsImage()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();

			file.Stub(x => x.FileName).Return("test.jpg");
			file.Stub(x => x.ContentType).Return("image/jpeg");

			var result = this.ImageService.IsImage(file);

			Assert.IsTrue(result);
		}

		[Test]
		public void Test_ImageService_IsImage_False()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();

			file.Stub(x => x.FileName).Return("test.zip");
			file.Stub(x => x.ContentType).Return("application/zip");

			var result = this.ImageService.IsImage(file);

			Assert.IsFalse(result);
		}

		[Test]
		public void Test_ImageService_GetExtension()
		{
			var result = this.ImageService.GetExtension(ImageFormat.Jpeg);

			Assert.AreEqual(".jpg", result);
		}

		[Test]
		public void Test_ImageService_GetContentType()
		{
			var result = this.ImageService.GetContentType(ImageFormat.Png);

			Assert.AreEqual("image/png", result);
		}

		[Test]
		public void Test_ImageService_GetImageDimensions()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();

			using (var stream = File.OpenRead("network.jpg".AsPath()))
			{
				file.Stub(x => x.FileName).Return("network.jpg");
				file.Stub(x => x.ContentType).Return("image/jpeg");
				file.Stub(x => x.InputStream).Return(stream);

				var result = this.ImageService.GetImageDimensions(file);

				Assert.AreEqual(806, result[0], "Width");
				Assert.AreEqual(600, result[1], "Height");

				stream.Close();
			}
		}
	}
}