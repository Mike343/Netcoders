#region License
//	Author: Mike Geise - http://www.netcoders.net
//	Copyright (c) 2010, Mike Geise
//
//	Licensed under the Apache License, Version 2.0 (the "License");
//	you may not use this file except in compliance with the License.
//	You may obtain a copy of the License at
//
//		http://www.apache.org/licenses/LICENSE-2.0
//
//	Unless required by applicable law or agreed to in writing, software
//	distributed under the License is distributed on an "AS IS" BASIS,
//	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//	See the License for the specific language governing permissions and
//	limitations under the License.
#endregion

#region Using Directives
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
#endregion\

namespace Coders.Services
{
	public class ImageService : IImageService
	{
		/// <summary>
		/// Determines whether the specified file is an image.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		/// 	<c>true</c> if the specified file is an image; otherwise, <c>false</c>.
		/// </returns>
		public bool IsImage(HttpPostedFileBase file)
		{
			if (file == null)
			{
				return false;
			}

			var regex = new Regex(@"image/\S+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			if (regex.IsMatch(file.ContentType))
			{
				var value = Path.GetExtension(file.FileName);

				if (string.IsNullOrEmpty(value))
				{
					return false;
				}

				var extension = value.Replace(".", string.Empty);
				var extensions = Setting.ImageExtension.Value.Split(',');

				return extensions.Any(x => x == extension);
			}

			return false;
		}

		/// <summary>
		/// Gets the extension by the specified format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		public string GetExtension(ImageFormat format)
		{
			if (format == ImageFormat.Bmp)
			{
				return ".bmp";
			}

			if (format == ImageFormat.Gif)
			{
				return ".gif";
			}

			return format == ImageFormat.Png ? ".png" : ".jpeg";
		}

		/// <summary>
		/// Gets the type of the content by the specified format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		public string GetContentType(ImageFormat format)
		{
			if (format == ImageFormat.Bmp)
			{
				return "image/bmp";
			}

			if (format == ImageFormat.Gif)
			{
				return "image/gif";
			}

			return format == ImageFormat.Png ? "image/png" : "image/pjpeg";
		}

		/// <summary>
		/// Gets the image dimensions for the specified uploaded file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public int[] GetImageDimensions(HttpPostedFileBase file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			if (!this.IsImage(file))
			{
				return new[] { 0, 0 };
			}

			var result = new int[2];

			using (var image = Image.FromStream(file.InputStream))
			{
				result[0] = image.Width;
				result[0] = image.Height;
			}

			return result;
		}

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public ImageResult Save(HttpPostedFileBase file, string path)
		{
			return Save(file, null, path, Guid.NewGuid().ToString());
		}

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public ImageResult Save(HttpPostedFileBase file, string path, string name)
		{
			return Save(file, null, path, name);
		}

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public ImageResult Save(HttpPostedFileBase file, ImageFormat format, string path)
		{
			return Save(file, format, path, Guid.NewGuid().ToString());
		}

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public ImageResult Save(HttpPostedFileBase file, ImageFormat format, string path, string name)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			// image
			var image = new WebImage(file.InputStream);

			// name, path and type
			var nameOnDisk = string.Concat(name, (format == null) ? Path.GetExtension(file.FileName) : GetExtension(format));
			var typeOnDisk = (format == null) ? file.ContentType : GetContentType(format);
			var extension = Path.GetExtension(file.FileName);

			if (string.IsNullOrEmpty(extension))
			{
				throw new InvalidOperationException("The extension is null");
			}

			// image result
			var result = new ImageResult
			{
				FileWidth = image.Width,
				FileHeight = image.Height,
				FileName = Path.GetFileName(file.FileName),
				FileDiskName = nameOnDisk,
				FileExtension = extension.Replace(".", string.Empty),
				FilePath = path,
				FileType = typeOnDisk
			};

			// save the uploaded image to disk
			if (format == null)
			{
				image.Save(Path.Combine(result.FilePath, result.FileDiskName).AsPath());
			}
			else
			{
				image.Save(Path.Combine(result.FilePath, result.FileDiskName).AsPath(), GetContentType(format));
			}

			return result;
		}

		/// <summary>
		/// Resizes the specified image
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">Maintain aspect.</param>
		public void Resize(string source, string path, int width, int height, bool aspect)
		{
			var image = new WebImage(source.AsPath());

			Resize(image, image.ImageFormat, path, width, height, aspect);
		}

		/// <summary>
		/// Resizes the uploaded image
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">Maintain aspect.</param>
		public void Resize(HttpPostedFileBase file, string path, int width, int height, bool aspect)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			Resize(new WebImage(file.InputStream), file.ContentType, path, width, height, aspect);
		}

		/// <summary>
		/// Resizes the uploaded image
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">Maintain aspect.</param>
		public void Resize(HttpPostedFileBase file, ImageFormat format, string path, int width, int height, bool aspect)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			Resize(new WebImage(file.InputStream), GetContentType(format), path, width, height, aspect);
		}

		/// <summary>
		/// Resizes the specified image.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">if set to <c>true</c> [aspect].</param>
		private static void Resize(WebImage image, string format, string path, int width, int height, bool aspect)
		{
			image.Clone().Resize(width, height, aspect).Save(path.AsPath(), format);
		}
	}
}