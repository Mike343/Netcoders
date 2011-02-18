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
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Coders.Extensions;
#endregion

namespace Coders.Web.Helpers
{
	public class RouteHelper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RouteHelper"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="context">The context.</param>
		public RouteHelper(string value, RequestContext context)
		{
			this.Value = value;
			this.Context = context;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value
		{
			get;
			private set;
		}

		public RequestContext Context
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the link using the specified text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public MvcHtmlString Link(string text)
		{
			return GetLink(text, new RouteValueDictionary());
		}

		/// <summary>
		/// Returns the link using the specified text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		public MvcHtmlString Link(string text, object attributes)
		{
			return GetLink(text, new RouteValueDictionary(attributes));
		}

		/// <summary>
		/// Returns the image using the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public MvcHtmlString Image(string source)
		{
			return GetImageLink(source, string.Empty, true, new RouteValueDictionary());
		}

		/// <summary>
		/// Returns the image using the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public MvcHtmlString Image(string source, string text)
		{
			return GetImageLink(source, text, true, new RouteValueDictionary());
		}

		/// <summary>
		/// Returns the image using the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		public MvcHtmlString Image(string source, object attributes)
		{
			return GetImageLink(source, string.Empty, true, new RouteValueDictionary(attributes));
		}

		/// <summary>
		/// Returns the image using the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="root">if set to <c>true</c> [root].</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		public MvcHtmlString Image(string source, bool root, object attributes)
		{
			return GetImageLink(source, string.Empty, root, new RouteValueDictionary(attributes));
		}

		/// <summary>
		/// Returns the image using the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		public MvcHtmlString Image(string source, string text, object attributes)
		{
			return GetImageLink(source, text, true, new RouteValueDictionary(attributes));
		}

		/// <summary>
		/// Returns the image using the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
		/// <param name="root">if set to <c>true</c> [root].</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		public MvcHtmlString Image(string source, string text, bool root, object attributes)
		{
			return GetImageLink(source, text, root, new RouteValueDictionary(attributes));
		}

		/// <summary>
		/// Gets the link.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		private MvcHtmlString GetLink(string text, IDictionary<string, object> attributes)
		{
			var clickable = true;

			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}

			if (attributes.ContainsKey("clickable"))
			{
				clickable = attributes["clickable"].AsBoolean();

				attributes.Remove("clickable");
			}

			if (!clickable)
			{
				return new MvcHtmlString(text);
			}

			var link = new TagBuilder("a");

			link.MergeAttribute("href", this.Value);
			link.MergeAttribute("title", text);

			if (attributes.Count > 0)
			{
				link.MergeAttributes(attributes);
			}

			link.SetInnerText(text);

			return new MvcHtmlString(link.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Gets the image link.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="text">The text.</param>
		/// <param name="root">if set to <c>true</c> [root].</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		private MvcHtmlString GetImageLink(string source, string text, bool root, IDictionary<string, object> attributes)
		{
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}

			var link = new TagBuilder("a");

			link.MergeAttribute("href", this.Value);

			var image = new TagBuilder("img");

			image.MergeAttribute("src", (root) ? source.AsRoot(this.Context.HttpContext) : source);

			if (!string.IsNullOrEmpty(text))
			{
				image.MergeAttribute("alt", text);
			}

			if (attributes.Count > 0)
			{
				image.MergeAttributes(attributes);
			}

			link.InnerHtml = image.ToString(TagRenderMode.SelfClosing);

			return new MvcHtmlString(link.ToString(TagRenderMode.Normal));
		}
	}
}