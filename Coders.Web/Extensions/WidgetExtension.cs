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
using System.Web.Routing;
using Coders.Web.Widgets;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace Coders.Web.Extensions
{
	public static class WidgetExtension
	{
		/// <summary>
		/// Renders the widget using the specified name.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="name">The name.</param>
		public static void Widget(this HtmlHelper helper, string name)
		{
			Widget(helper, name, null);
		}

		/// <summary>
		/// Renders the widget using the specified name.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="name">The name.</param>
		/// <param name="attributes">The attributes.</param>
		public static void Widget(this HtmlHelper helper, string name, object attributes)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			var widget = ServiceLocator.Current.GetInstance<IWidget>(name);

			if (widget == null)
			{
				return;
			}

			widget.Attributes = new RouteValueDictionary(attributes);
			widget.Render(helper.ViewContext);
		}
	}
}