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
using System;
using System.Linq;
using System.Web.Mvc.Html;
using Coders.Models.Countries.Enums;
using Coders.Models.Settings;
using Coders.Web.Models;
using Coders.Web.ViewModels;
#endregion

namespace Coders.Web.Widgets
{
	[Widget("country.filter")]
	public class CountryFilter : WidgetBase
	{
		/// <summary>
		/// Renders the widget.
		/// </summary>
		public override void Render()
		{
			var sorts = Enum.GetNames(typeof(SortCountry)).Select(
				value => new Filter(value, new { sort = value.ToLowerInvariant() })
			);

			Html.RenderPartial(Setting.TemplateWidgetCountryFilter.Value, new FilterViewModel(sorts));
		}
	}
}