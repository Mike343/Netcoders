using System.IO;
using System.Web;
using Coders.Extensions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class FileServiceTest
	{
		[SetUp]
		public void Initialize()
		{
			using (var file = File.Create("test.jpg".AsPath()))
			{
				file.Close();
			}
		}

		[Test]
		public void Test_FileService_Save()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();

			file.Stub(x => x.FileName).Return("test.jpg");
			file.Stub(x => x.ContentType).Return("image/jpeg");
			file.Stub(x => x.ContentLength).Return(12345678);

			var service = new FileService();
			var result = service.Save(file, "test", "other");

			Assert.AreEqual("test.jpg", result.FileName, "FileName");
			Assert.AreEqual("other.jpg", result.FileDiskName, "FileDiskName");
			Assert.AreEqual("test", result.FilePath, "FilePath");
			Assert.AreEqual("image/jpeg", result.FileType, "FileType");
			Assert.AreEqual("jpg", result.FileExtension, "FileExtension");
			Assert.AreEqual(12345678, result.FileSize, "FileSize");
		}

		[Test]
		public void Test_FileService_Delete()
		{
			var service = new FileService();

			service.Delete("test.jpg");

			var file = new FileInfo("test.jpg".AsPath());

			Assert.IsFalse(file.Exists);
		}
	}
}