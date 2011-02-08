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
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Filter = Coders.Web.Models.Filter;
#endregion

namespace Coders.Web.Extensions
{
	public static class FilterExtension
	{
		/// <summary>
		/// Determines whether the specified filter is selected.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="helper">The helper.</param>
		/// <returns>
		///   <c>true</c> if the specified filter is selected; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSelected(this Filter filter, HtmlHelper helper)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}

			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			var conditions = new RouteValueDictionary(filter.Conditions);

			if (conditions.Count <= 0)
			{
				return false;
			}

			var selected = false;

			foreach (var condition in conditions)
			{
				string value = null;

				if (helper.ViewContext.RouteData.Values.ContainsKey(condition.Key))
				{
					value = helper.ViewContext.RouteData.Values[condition.Key] as string;
				}
				else if (!string.IsNullOrEmpty(helper.ViewContext.HttpContext.Request[condition.Key]))
				{
					value = helper.ViewContext.HttpContext.Request[condition.Key];
				}

				var compare = condition.Value as string;

				if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(compare))
				{
					continue;
				}

				if (compare.Contains(","))
				{
					selected = compare.Split(',').Any(item => item == value);
				}
				else
				{
					selected = (value == compare);
				}
			}

			return selected;
		}
	}
}