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
#endregion

namespace Coders.Web.Widgets
{
	public abstract class WidgetBase : IWidget, IViewDataContainer
	{
		/// <summary>
		/// Gets or sets the view context.
		/// </summary>
		/// <value>The view context.</value>
		public ViewContext ViewContext
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the view data.
		/// </summary>
		/// <value>The view data.</value>
		public ViewDataDictionary ViewData
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the HTML.
		/// </summary>
		/// <value>The HTML.</value>
		public HtmlHelper Html
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the ajax.
		/// </summary>
		/// <value>The ajax.</value>
		public AjaxHelper Ajax
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public UrlHelper Url
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the attributes.
		/// </summary>
		/// <value>The attributes.</value>
		public RouteValueDictionary Attributes
		{
			get; 
			set;
		}

		/// <summary>
		/// Renders the widget using the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		public void Render(ViewContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			this.ViewContext = context;
			this.ViewData = context.ViewData;
			this.Html = new HtmlHelper(context, this);
			this.Ajax = new AjaxHelper(context, this);
			this.Url = new UrlHelper(context.RequestContext);

			Render();
		}

		/// <summary>
		/// Renders the widget.
		/// </summary>
		public abstract void Render();
	}
}