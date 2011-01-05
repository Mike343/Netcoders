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

namespace Coders.Models.Common
{
	public class ImageResult
	{
		/// <summary>
		/// Gets or sets the width of the file.
		/// </summary>
		/// <value>The width of the file.</value>
		public int FileWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of the file.
		/// </summary>
		/// <value>The height of the file.</value>
		public int FileHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file  on disk.
		/// </summary>
		/// <value>The name of the file disk.</value>
		public string FileDiskName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the file extension.
		/// </summary>
		/// <value>The file extension.</value>
		public string FileExtension
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>The file path.</value>
		public string FilePath
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>The type of the file.</value>
		public string FileType
		{
			get;
			set;
		}
	}
}