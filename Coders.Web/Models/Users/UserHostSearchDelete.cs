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
using Coders.Models.Users;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserHostSearchDelete
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostSearchDelete"/> class.
		/// </summary>
		public UserHostSearchDelete()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostSearchDelete"/> class.
		/// </summary>
		/// <param name="search">The search.</param>
		public UserHostSearchDelete(UserHostSearch search)
		{
			if (search == null)
			{
				throw new ArgumentNullException("search");
			}

			this.Id = search.Id;
			this.Title = search.Title;
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get;
			set;
		}
	}
}