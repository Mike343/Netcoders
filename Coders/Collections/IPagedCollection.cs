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
using System.Collections.Generic;
#endregion

namespace Coders.Collections
{
	public interface IPagedCollection<T> : IEnumerable<T>
	{
		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>
		IList<T> Items
		{
			get;
		}

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <value>The page.</value>
		int Page
		{
			get;
		}

		/// <summary>
		/// Gets the limit.
		/// </summary>
		/// <value>The limit.</value>
		int Limit
		{
			get;
		}

		/// <summary>
		/// Gets the total.
		/// </summary>
		/// <value>The total.</value>
		int Total
		{
			get;
		}

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>The pages.</value>
		int Pages
		{
			get;
		}

		/// <summary>
		/// Gets the first index.
		/// </summary>
		/// <value>The first index.</value>
		int FirstIndex
		{
			get;
		}

		/// <summary>
		/// Gets the last index.
		/// </summary>
		/// <value>The last index.</value>
		int LastIndex
		{
			get;
		}

		/// <summary>
		/// Gets the start index.
		/// </summary>
		/// <value>The start index.</value>
		int StartIndex
		{
			get;
		}

		/// <summary>
		/// Gets the end index.
		/// </summary>
		/// <value>The end index.</value>
		int EndIndex
		{
			get;
		}

		/// <summary>
		/// Gets the previous page.
		/// </summary>
		/// <value>The previous page.</value>
		int PreviousPage
		{
			get;
		}

		/// <summary>
		/// Gets the next page.
		/// </summary>
		/// <value>The next page.</value>
		int NextPage
		{
			get;
		}

		/// <summary>
		/// Gets a value indicating whether this instance has previous.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has previous; otherwise, <c>false</c>.
		/// </value>
		bool HasPrevious
		{
			get;
		}

		/// <summary>
		/// Gets a value indicating whether this instance has a next page.
		/// </summary>
		/// <value><c>true</c> if this instance has a next page; otherwise, <c>false</c>.</value>
		bool HasNext
		{
			get;
		}
	}
}