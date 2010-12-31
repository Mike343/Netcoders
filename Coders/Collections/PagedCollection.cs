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
using System.Collections;
using System.Collections.Generic;
using Coders.Strings;
#endregion

namespace Coders.Collections
{
	public class PagedCollection<T> : IPagedCollection<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PagedCollection&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="page">The page.</param>
		/// <param name="limit">The limit.</param>
		/// <param name="total">The total.</param>
		public PagedCollection(IList<T> items, int page, int limit, int total)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}

			if (page <= 0)
			{
				throw new ArgumentOutOfRangeException("page", Errors.PageGreaterThanZero);
			}

			if (limit <= 0)
			{
				throw new ArgumentOutOfRangeException("limit", Errors.LimitGreaterThanZero);
			}

			if (total < 0)
			{
				throw new ArgumentOutOfRangeException("total", Errors.TotalGreaterThanOrEqualZero);
			}

			this.Items = items;
			this.Page = page;
			this.Limit = limit;
			this.Total = total;
		}

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		public IList<T> Items
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the page.
		/// </summary>
		/// <value>The page.</value>
		public int Page
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the limit.
		/// </summary>
		/// <value>The limit.</value>
		public int Limit
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the total.
		/// </summary>
		/// <value>The total.</value>
		public int Total
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>The pages.</value>
		public int Pages
		{
			get
			{
				return (int)Math.Ceiling(((double)this.Total) / ((double)this.Limit));
			}
		}

		/// <summary>
		/// Gets the first index.
		/// </summary>
		/// <value>The first index.</value>
		public int FirstIndex
		{
			get
			{
				return this.Limit * (this.Page - 1);
			}
		}

		/// <summary>
		/// Gets the last index.
		/// </summary>
		/// <value>The last index.</value>
		public int LastIndex
		{
			get
			{
				return Math.Min(this.FirstIndex + (this.Limit - 1), this.Total - 1);
			}
		}

		/// <summary>
		/// Gets the start index.
		/// </summary>
		/// <value>The start index.</value>
		public int StartIndex
		{
			get
			{
				return this.FirstIndex + 1;
			}
		}

		/// <summary>
		/// Gets the end index.
		/// </summary>
		/// <value>The end index.</value>
		public int EndIndex
		{
			get
			{
				return this.LastIndex + 1;
			}
		}

		/// <summary>
		/// Gets the previous page.
		/// </summary>
		/// <value>The previous page.</value>
		public int PreviousPage
		{
			get
			{
				return this.Page - 1;
			}
		}

		/// <summary>
		/// Gets the next page.
		/// </summary>
		/// <value>The next page.</value>
		public int NextPage
		{
			get
			{
				return this.Page + 1;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has previous.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has previous; otherwise, <c>false</c>.
		/// </value>
		public bool HasPrevious
		{
			get
			{
				return (this.FirstIndex != 0);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has next.
		/// </summary>
		/// <value><c>true</c> if this instance has next; otherwise, <c>false</c>.</value>
		public bool HasNext
		{
			get
			{
				return (this.Total == -1 || (this.FirstIndex + this.Limit) < this.Total);
			}
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}
	}
}