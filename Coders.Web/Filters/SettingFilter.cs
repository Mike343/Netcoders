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
using Coders.Extensions;
using Coders.Models.Settings;
using Coders.Strings;
using Coders.Web.Routes;
#endregion

namespace Coders.Web.Filters
{
	public class SettingFilter : FilterBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingFilter"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public SettingFilter(ViewContext context) 
			: base(context)
		{
			this.SettingService = ServiceLocator.Current.GetInstance<ISettingService>();
		}

		/// <summary>
		/// Gets or sets the setting service.
		/// </summary>
		/// <value>The setting service.</value>
		public ISettingService SettingService
		{
			get; 
			private set;
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			var groups = this.SettingService.GetGroups();

			foreach (var item in groups)
			{
				base.Add(Titles.ByWith.FormatInvariant(Titles.Group), item, AdministrationRoutes.SettingIndex, new { group = item });
			}

			base.Add(Titles.SortBy, Titles.Title, AdministrationRoutes.SettingIndex, new { sort = "title" });
			base.Add(Titles.SortBy, Titles.Group, AdministrationRoutes.SettingIndex, new { sort = "group" });
			base.Add(Titles.OrderBy, Titles.Ascending, AdministrationRoutes.SettingIndex, new { order = "ascending" });
			base.Add(Titles.OrderBy, Titles.Descending, AdministrationRoutes.SettingIndex, new { order = "descending" });
		}
	}
}