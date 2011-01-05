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
using System.Drawing.Imaging;
using System.Web;
#endregion

namespace Coders.Models.Common
{
	public interface IImageService
	{
		/// <summary>
		/// Determines whether the specified uploaded file is an image.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		/// 	<c>true</c> if the specified uploaded file is an image; otherwise, <c>false</c>.
		/// </returns>
		bool IsImage(HttpPostedFileBase file);

		/// <summary>
		/// Gets the extension by the specified format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		string GetExtension(ImageFormat format);

		/// <summary>
		/// Gets the type of the content by the specified format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		string GetContentType(ImageFormat format);

		/// <summary>
		/// Gets the image dimensions for the specified uploaded file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		int[] GetImageDimensions(HttpPostedFileBase file);

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		ImageResult Save(HttpPostedFileBase file, string path);

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <param name="name">The name.</param>
		ImageResult Save(HttpPostedFileBase file, string path, string name);

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		ImageResult Save(HttpPostedFileBase file, ImageFormat format, string path);

		/// <summary>
		/// Saves the specified uploaded image to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		/// <param name="name">The name.</param>
		ImageResult Save(HttpPostedFileBase file, ImageFormat format, string path, string name);

		/// <summary>
		/// Resizes the specified image
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">Maintain aspect.</param>
		void Resize(string source, string path, int width, int height, bool aspect);

		/// <summary>
		/// Resizes the specified uploaded image
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">Maintain aspect.</param>
		void Resize(HttpPostedFileBase file, string path, int width, int height, bool aspect);

		/// <summary>
		/// Resizes the specified uploaded image
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="format">The format.</param>
		/// <param name="path">The path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="aspect">Maintain aspect.</param>
		void Resize(HttpPostedFileBase file, ImageFormat format, string path, int width, int height, bool aspect);
	}
}