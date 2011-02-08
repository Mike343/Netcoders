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
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Common.Enums;
using Coders.Web.Models;
#endregion

namespace Coders.Web.ViewModels
{
	public class FilterViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FilterViewModel"/> class.
		/// </summary>
		public FilterViewModel()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FilterViewModel"/> class.
		/// </summary>
		/// <param name="sorts">The sorts.</param>
		public FilterViewModel(IEnumerable<Filter> sorts)
		{
			this.Sorts = sorts;

			this.Orders = Enum.GetNames(typeof(SortOrder)).Select(
				value => new Filter(value, new { order = value.ToLowerInvariant() })
			);
		}

		/// <summary>
		/// Gets the sorts.
		/// </summary>
		public IEnumerable<Filter> Sorts
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the orders.
		/// </summary>
		public IEnumerable<Filter> Orders
		{
			get;
			private set;
		}
	}
}