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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Coders.Extensions;
#endregion

namespace Coders.Web.Extensions
{
	public static class SelectExtension
	{
		public static SelectList SelectList(this string[] items)
		{
			return SelectList(items, null);
		}

		public static SelectList SelectList(this string[] items, object value)
		{
			return new SelectList(items, value);
		}

		public static SelectList SelectList<T>(this IList<T> items)
		{
			return SelectList(items, null);
		}

		public static SelectList SelectList<T>(this IList<T> items, object value)
		{
			return new SelectList(items, value);
		}

		public static SelectList SelectList<T>(this IList<T> items, string key, string text)
		{
			return SelectList(items, key, text, null);
		}

		public static SelectList SelectList<T>(this IList<T> items, string key, string text, object value)
		{
			return new SelectList(items, key, text, value);
		}

		public static SelectList SelectList<T>(this IList<T> items, Expression<Func<T, int>> key, Expression<Func<T, string>> text)
		{
			return SelectList(items, key, text, null);
		}

		public static SelectList SelectList<T>(this IList<T> items, Expression<Func<T, int>> key, Expression<Func<T, string>> text, object value)
		{
			return new SelectList(items, ExpressionHelper.GetExpressionText(key), ExpressionHelper.GetExpressionText(text), value);
		}

		public static MvcHtmlString SelectListTree<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, SelectList list)
		{
			var name = ExpressionHelper.GetExpressionText(expression);
			var builder = new StringBuilder();

			using (var writer = new StringWriter(builder, CultureInfo.InvariantCulture))
			{
				var html = new XhtmlTextWriter(writer);

				html.AddAttribute(HtmlTextWriterAttribute.Id, name);
				html.AddAttribute(HtmlTextWriterAttribute.Name, name);
				html.RenderBeginTag(HtmlTextWriterTag.Select);

				if (list != null)
				{
					OptionsRecursive(html, list, list.Items);
				}

				html.RenderEndTag();
			}

			return new MvcHtmlString(builder.ToString());
		}

		public static MvcHtmlString SelectListLocalized(this HtmlHelper helper, string name, SelectList list)
		{
			var builder = new StringBuilder();

			using (var writer = new StringWriter(builder, CultureInfo.InvariantCulture))
			{
				var html = new XhtmlTextWriter(writer);

				html.AddAttribute(HtmlTextWriterAttribute.Id, name);
				html.AddAttribute(HtmlTextWriterAttribute.Name, name);
				html.RenderBeginTag(HtmlTextWriterTag.Select);

				if (list != null)
				{
					OptionsLocalized(html, list, list.Items);
				}

				html.RenderEndTag();
			}

			return new MvcHtmlString(builder.ToString());
		}

		public static MvcHtmlString SelectListLocalizedFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, SelectList list)
		{
			var name = ExpressionHelper.GetExpressionText(expression);
			var builder = new StringBuilder();

			using (var writer = new StringWriter(builder, CultureInfo.InvariantCulture))
			{
				var html = new XhtmlTextWriter(writer);

				html.AddAttribute(HtmlTextWriterAttribute.Id, name);
				html.AddAttribute(HtmlTextWriterAttribute.Name, name);
				html.RenderBeginTag(HtmlTextWriterTag.Select);

				if (list != null)
				{
					OptionsLocalized(html, list, list.Items);
				}

				html.RenderEndTag();
			}

			return new MvcHtmlString(builder.ToString());
		}

		private static void OptionsRecursive(HtmlTextWriter html, SelectList list, IEnumerable items)
		{
			foreach (var item in items)
			{
				var value = Eval(item, list.DataValueField);

				if (value.Equals(list.SelectedValue))
				{
					html.AddAttribute(HtmlTextWriterAttribute.Selected, "selected");
				}

				html.AddAttribute(HtmlTextWriterAttribute.Value, value.ToString());
				html.RenderBeginTag(HtmlTextWriterTag.Option);
				html.Write(Eval(item, list.DataTextField).ToString().Indent(Convert.ToInt32(Eval(item, "Depth"), CultureInfo.InvariantCulture)));
				html.RenderEndTag();

				var childern = Eval(item, "Childern") as IList;

				if (childern != null && childern.Count > 0)
				{
					OptionsRecursive(html, list, childern);
				}
			}
		}

		private static void OptionsLocalized(HtmlTextWriter html, SelectList list, IEnumerable items)
		{
			foreach (var item in items)
			{
				var value = Eval(item, list.DataValueField);

				if (value.Equals(list.SelectedValue))
				{
					html.AddAttribute(HtmlTextWriterAttribute.Selected, "selected");
				}

				var text = Eval(item, list.DataTextField).ToString();

				html.AddAttribute(HtmlTextWriterAttribute.Value, value.ToString());
				html.RenderBeginTag(HtmlTextWriterTag.Option);
				html.Write(text.Localize("Titles"));
				html.RenderEndTag();
			}
		}

		private static object Eval(object container, string expression)
		{
			var value = container;

			if (!string.IsNullOrEmpty(expression))
			{
				value = DataBinder.Eval(container, expression);
			}

			return value;
		}
	}
}