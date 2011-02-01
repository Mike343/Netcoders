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
using System.Web.Mvc;
using Coders.Strings;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Filters
{
	public class CountryFilter : FilterBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryFilter"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CountryFilter(ViewContext context)
			: base(context)
		{

		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			base.Add(Titles.SortBy, Titles.Title, AdministrationRoutes.CountryIndex, new { sort = "title" });
			base.Add(Titles.SortBy, Titles.Code, AdministrationRoutes.CountryIndex, new { sort = "code" });
			base.Add(Titles.OrderBy, Titles.Ascending, AdministrationRoutes.CountryIndex, new { order = "ascending" });
			base.Add(Titles.OrderBy, Titles.Descending, AdministrationRoutes.CountryIndex, new { order = "descending" });
		}
	}
}