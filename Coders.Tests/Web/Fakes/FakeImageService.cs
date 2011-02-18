using System;
using System.Drawing.Imaging;
using System.Web;
using Coders.Models.Common;

namespace Coders.Tests.Web.Fakes
{
	public class FakeImageService : IImageService
	{
		public bool IsImage(HttpPostedFileBase file)
		{
			return true;
		}

		public int[] GetImageDimensions(HttpPostedFileBase file)
		{
			return new [] { 0, 0 };
		}

		public ImageResult Save(HttpPostedFileBase file, string path)
		{
			throw new NotImplementedException();
		}

		public ImageResult Save(HttpPostedFileBase file, string path, string name)
		{
			throw new NotImplementedException();
		}

		public void Resize(string source, string path, int width, int height, bool aspect)
		{
			throw new NotImplementedException();
		}

		public void Resize(HttpPostedFileBase file, string path, int width, int height, bool aspect)
		{
			throw new NotImplementedException();
		}

		public void Resize(HttpPostedFileBase file, ImageFormat format, string path, int width, int height, bool aspect)
		{
			throw new NotImplementedException();
		}

		public ImageResult Save(HttpPostedFileBase file, ImageFormat format, string path, string name)
		{
			throw new NotImplementedException();
		}

		public ImageResult Save(HttpPostedFileBase file, ImageFormat format, string path)
		{
			throw new NotImplementedException();
		}

		public string GetContentType(ImageFormat format)
		{
			throw new NotImplementedException();
		}

		public string GetExtension(ImageFormat format)
		{
			throw new NotImplementedException();
		}
	}
}