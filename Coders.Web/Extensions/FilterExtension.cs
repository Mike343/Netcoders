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
using System.Web.Mvc;
using Coders.Web.Filters;
#endregion

namespace Coders.Web.Extensions
{
	public static class FilterExtension
	{
		/// <summary>
		/// Renders the filter for the settings.
		/// </summary>
		/// <param name="helper">The helper.</param>
		public static void SettingFilter(this HtmlHelper helper)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			var filter = new SettingFilter(helper.ViewContext);

			filter.Initialize();
			filter.Render();
		}
	}
}