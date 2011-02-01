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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Coders.Extensions;
using Coders.Strings;
using Filter = Coders.Web.Models.Filter;
#endregion

namespace Coders.Web.Filters
{
	public abstract class FilterBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FilterBase"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		protected FilterBase(ViewContext context)
		{
			this.Context = context;
			this.Filters = new Dictionary<string, IList<Filter>>();
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
		/// Gets or sets the filters.
		/// </summary>
		/// <value>The filters.</value>
		public IDictionary<string, IList<Filter>> Filters
		{
			get;
			private set;
		}

		public void Add(string group, string title, string routeKey, object parameters)
		{
			this.Add(group, title, routeKey, parameters, null);
		}

		/// <summary>
		/// Adds the filter.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <param name="title">The title.</param>
		/// <param name="routeKey">The route key.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="conditions">The conditions.</param>
		public void Add(string group, string title, string routeKey, object parameters, object conditions)
		{
			var key = group.Slug();

			if (!this.Filters.ContainsKey(key))
			{
				this.Filters[key] = new List<Filter>();
			}

			var filter = new Filter
			{
				Group = group,
				Title = title,
				RouteKey = routeKey,
				Parameters = parameters
			};

			if (conditions == null)
			{
				filter.Conditions = parameters;
			}

			var childern = this.Filters[key];

			childern.Add(filter);
		}

		/// <summary>
		/// Renders this instance.
		/// </summary>
		public void Render()
		{
			using (var html = new XhtmlTextWriter(this.Context.Writer))
			{
				html.AddAttribute(HtmlTextWriterAttribute.Class, "filter");
				html.RenderBeginTag(HtmlTextWriterTag.Div);

				html.RenderBeginTag(HtmlTextWriterTag.H5);
				html.Write(Titles.Filter);
				html.RenderEndTag();

				foreach (var filter in this.Filters)
				{
					var group = filter.Value.Select(x => x.Group).FirstOrDefault();

					html.RenderBeginTag(HtmlTextWriterTag.H6);
					html.Write(group);
					html.RenderEndTag();

					html.RenderBeginTag(HtmlTextWriterTag.Ul);

					foreach (var childern in filter.Value)
					{
						var route = RouteTable.Routes[childern.RouteKey];
						var conditions = new RouteValueDictionary(childern.Conditions);

						if (conditions.Count > 0)
						{
							var selected = false;

							foreach (var condition in conditions)
							{
								string value = null;

								if (Context.RouteData.Values.ContainsKey(condition.Key))
								{
									value = Context.RouteData.Values[condition.Key] as string;
								}
								else if (!string.IsNullOrEmpty(Context.HttpContext.Request[condition.Key]))
								{
									value = Context.HttpContext.Request[condition.Key];
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

							if (selected)
							{
								html.AddAttribute(HtmlTextWriterAttribute.Class, "selected");
							}
						}

						html.RenderBeginTag(HtmlTextWriterTag.Li);

						var path = route.GetVirtualPath(
							this.Context.RequestContext, 
							new RouteValueDictionary(childern.Parameters)
						);

						if (path != null)
						{
							html.AddAttribute(HtmlTextWriterAttribute.Href, path.VirtualPath.AsRoot());
							html.RenderBeginTag(HtmlTextWriterTag.A);
							html.Write(childern.Title);
							html.RenderEndTag();
						}

						html.RenderEndTag();
					}

					html.RenderEndTag();
				}

				html.RenderEndTag();
			}
		}
	}
}