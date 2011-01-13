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
using System.Web.Routing;
using System.Web.UI;
using Coders.Collections;
using Coders.Extensions;
using Coders.Strings;
#endregion

namespace Coders.Web.Helpers
{
	public class PagerHelper<T>
	{
		// private constants
		private const int Adjacents = 1;

		/// <summary>
		/// Initializes a new instance of the <see cref="PagerHelper&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="items">The items.</param>
		/// <param name="route">The route.</param>
		public PagerHelper(ViewContext context, IPagedCollection<T> items, string route)
		{
			this.Context = context;
			this.Items = items;
			this.Route = route;
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		/// <value>The context.</value>
		public ViewContext Context
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		public IPagedCollection<T> Items
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the route.
		/// </summary>
		/// <value>The route.</value>
		public string Route
		{
			get;
			private set;
		}

		/// <summary>
		/// Renders this instance.
		/// </summary>
		/// <returns></returns>
		public void Render()
		{
			if (this.Items == null || this.Items.Total <= 0)
			{
				return;
			}

			using (var html = new XhtmlTextWriter(this.Context.Writer))
			{
				var route = RouteTable.Routes[this.Route];

				html.AddAttribute(HtmlTextWriterAttribute.Class, "pagination");
				html.RenderBeginTag(HtmlTextWriterTag.Div);

				html.AddAttribute(HtmlTextWriterAttribute.Class, "results");
				html.RenderBeginTag(HtmlTextWriterTag.Div);

				html.RenderBeginTag(HtmlTextWriterTag.Span);
				html.Write(Messages.ShowingResults.FormatInvariant(this.Items.StartIndex, this.Items.EndIndex, this.Items.Total));
				html.RenderEndTag();

				html.RenderEndTag();

				html.WriteLine();

				html.AddAttribute(HtmlTextWriterAttribute.Class, "pager");
				html.RenderBeginTag(HtmlTextWriterTag.Ul);

				if (this.Items.HasPrevious)
				{
					var path = route.GetVirtualPath(
						this.Context.RequestContext,
						new RouteValueDictionary(new { page = this.Items.PreviousPage })
					);

					if (path != null)
					{
						html.RenderBeginTag(HtmlTextWriterTag.Li);

						html.AddAttribute(HtmlTextWriterAttribute.Href, path.VirtualPath.AsRoot());
						html.RenderBeginTag(HtmlTextWriterTag.A);
						html.Write("&laquo; prev");
						html.RenderEndTag();

						html.RenderEndTag();
					}
				}
				else
				{
					html.AddAttribute(HtmlTextWriterAttribute.Class, "disabled");
					html.RenderBeginTag(HtmlTextWriterTag.Li);
					html.Write("&laquo; prev");
					html.RenderEndTag();
				}

				html.WriteLine();

				if (this.Items.Pages < (4 + (2 * 2)))
				{
					WriteNumberedLinks(html, route, 1, this.Items.Pages);
				}
				else
				{
					if ((this.Items.Pages - (Adjacents * 2) > this.Items.Page) && (this.Items.Page > (Adjacents * 2)))
					{
						WriteNumberedLinks(html, route, 1, 2);
						WriteElipsis(html);
						WriteNumberedLinks(html, route, this.Items.Page - Adjacents, this.Items.Page + Adjacents);
						WriteElipsis(html);
						WriteNumberedLinks(html, route, this.Items.Pages - 1, this.Items.Pages);
					}
					else if (this.Items.Page < (this.Items.Pages / 2))
					{
						WriteNumberedLinks(html, route, 1, 2 + (Adjacents * 2));
						WriteElipsis(html);
						WriteNumberedLinks(html, route, this.Items.Pages - 1, this.Items.Pages);
					}
					else
					{
						WriteNumberedLinks(html, route, 1, 2);
						WriteElipsis(html);
						WriteNumberedLinks(html, route, this.Items.Pages - (2 + (Adjacents * 2)), this.Items.Pages);
					}
				}

				if (this.Items.HasNext)
				{
					var path = route.GetVirtualPath(
						this.Context.RequestContext,
						new RouteValueDictionary(new { page = this.Items.NextPage })
					);

					if (path != null)
					{
						html.RenderBeginTag(HtmlTextWriterTag.Li);

						html.AddAttribute(HtmlTextWriterAttribute.Href, path.VirtualPath.AsRoot());
						html.RenderBeginTag(HtmlTextWriterTag.A);
						html.Write("next &laquo;");
						html.RenderEndTag();

						html.RenderEndTag();
					}
				}
				else
				{
					html.AddAttribute(HtmlTextWriterAttribute.Class, "disabled");
					html.RenderBeginTag(HtmlTextWriterTag.Li);
					html.Write("next &laquo;");
					html.RenderEndTag();
				}

				html.RenderEndTag();

				html.RenderEndTag();
			}
		}

		/// <summary>
		/// Writes the numbered links.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="route">The route.</param>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		private void WriteNumberedLinks(HtmlTextWriter html, RouteBase route, int start, int end)
		{
			for (var i = start; i <= end; i++)
			{
				WriteNumberedLink(html, route, i);
			}
		}

		/// <summary>
		/// Writes the numbered link.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="route">The route.</param>
		/// <param name="index">The index.</param>
		private void WriteNumberedLink(HtmlTextWriter html, RouteBase route, int index)
		{
			if (this.Items.Page == index)
			{
				html.AddAttribute(HtmlTextWriterAttribute.Class, "current");
				html.RenderBeginTag(HtmlTextWriterTag.Li);
				html.Write(index);
				html.RenderEndTag();
			}
			else
			{
				var path = route.GetVirtualPath(
					this.Context.RequestContext,
					new RouteValueDictionary(new { page = index })
				);

				if (path != null)
				{
					html.RenderBeginTag(HtmlTextWriterTag.Li);

					html.AddAttribute(HtmlTextWriterAttribute.Href, path.VirtualPath.AsRoot());
					html.RenderBeginTag(HtmlTextWriterTag.A);
					html.Write(index);
					html.RenderEndTag();

					html.RenderEndTag();
				}
			}

			html.WriteLine();
		}

		/// <summary>
		/// Writes the elipsis.
		/// </summary>
		/// <param name="html">The HTML.</param>
		private static void WriteElipsis(HtmlTextWriter html)
		{
			html.AddAttribute(HtmlTextWriterAttribute.Class, "separator");
			html.RenderBeginTag(HtmlTextWriterTag.Li);
			html.Write("&#8230;");
			html.RenderEndTag();
		}
	}
}