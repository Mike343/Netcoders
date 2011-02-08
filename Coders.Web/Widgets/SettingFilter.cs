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
using System.Web.Mvc.Html;
using Coders.Models.Settings;
using Coders.Models.Settings.Enums;
using Coders.Web.Models;
using Coders.Web.ViewModels;
#endregion

namespace Coders.Web.Widgets
{
	[Widget("setting.filter")]
	public class SettingFilter : WidgetBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingFilter"/> class.
		/// </summary>
		/// <param name="settingService">The setting service.</param>
		public SettingFilter(ISettingService settingService)
		{
			this.SettingService = settingService;
		}

		/// <summary>
		/// Gets the setting service.
		/// </summary>
		public ISettingService SettingService
		{
			get;
			private set;
		}

		/// <summary>
		/// Renders the widget.
		/// </summary>
		public override void Render()
		{
			var groups = this.SettingService.GetGroups().Select(
				value => new Filter(value, new { group = value })
			);

			var sorts = Enum.GetNames(typeof(SortSetting)).Select(
				value => new Filter(value, new { sort = value.ToLowerInvariant() })
			);

			Html.RenderPartial(Setting.TemplateWidgetSettingFilter.Value, new SettingFilterViewModel(groups.ToList(), sorts));
		}
	}
}