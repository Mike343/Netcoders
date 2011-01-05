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
using System.IO;
using System.Web;
using Coders.Extensions;
using Coders.Models.Common;
#endregion

namespace Coders.Services
{
	public class FileService : IFileService
	{
		/// <summary>
		/// Save the uploaded file to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public FileResult Save(HttpPostedFileBase file, string path)
		{
			return Save(file, path, Guid.NewGuid().ToString());
		}

		/// <summary>
		/// Saves the uploaded file to disk
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public FileResult Save(HttpPostedFileBase file, string path, string name)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			var fileName = Path.GetFileName(file.FileName);

			if (string.IsNullOrEmpty(fileName))
			{
				throw new InvalidOperationException("The file name is null");
			}

			var fileExtension = Path.GetExtension(file.FileName);

			if (string.IsNullOrEmpty(fileExtension))
			{
				throw new InvalidOperationException("The file extension is null");
			}

			var result = new FileResult
			{
				FileName = fileName.ToLowerInvariant(),
				FileDiskName = string.Concat(name, Path.GetExtension(file.FileName)).ToLowerInvariant(),
				FilePath = path,
				FileType = file.ContentType,
				FileExtension = fileExtension.Replace(".", string.Empty).ToLowerInvariant(),
				FileSize = file.ContentLength
			};

			file.SaveAs(Path.Combine(result.FilePath, result.FileDiskName).AsPath());

			return result;
		}

		/// <summary>
		/// Delete the specified file from disk
		/// </summary>
		/// <param name="path">The path to the file.</param>
		public void Delete(string path)
		{
			var file = new FileInfo(path.AsPath());

			if (file.Exists)
			{
				file.Delete();
			}
		}
	}
}