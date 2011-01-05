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
#endregion

namespace Coders.Models.Users
{
	public class UserAvatar : EntityBase
	{
		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		/// <value>The user id.</value>
		public virtual int UserId
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public virtual string FileName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the file disk.
		/// </summary>
		/// <value>The name of the file disk.</value>
		public virtual string FileDiskName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>The file path.</value>
		public virtual string FilePath
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the full path.
		/// </summary>
		/// <value>The full path.</value>
		public virtual string FullPath
		{
			get
			{
				return string.Concat(this.FilePath, "/", this.FileDiskName);
			}
		}

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>The type of the file.</value>
		public virtual string FileType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>The created.</value>
		public virtual DateTime Created
		{
			get;
			set;
		}
	}
}