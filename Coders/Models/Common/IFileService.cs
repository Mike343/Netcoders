﻿#region License
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
using System.Web;
#endregion

namespace Coders.Models.Common
{
	public interface IFileService
	{
		/// <summary>
		/// Saves the uploaded file to the file system
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		FileResult Save(HttpPostedFileBase file, string path);

		/// <summary>
		/// Saves the uploaded file to the file system
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <param name="name">The name.</param>
		FileResult Save(HttpPostedFileBase file, string path, string name);

		/// <summary>
		/// Deletes the file from the file system
		/// </summary>
		/// <param name="path">The path to the file.</param>
		void Delete(string path);
	}
}