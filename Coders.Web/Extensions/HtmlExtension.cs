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
using System.Web.Mvc;
using Coders.Collections;
using Coders.Web.ActionResults.Enums;
using Coders.Web.Helpers;
using Coders.Web.Models;
#endregion

namespace Coders.Web.Extensions
{
	public static class HtmlExtension
	{
		/// <summary>
		/// Renders the pager for the specified collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="helper">The helper.</param>
		/// <param name="collection">The collection.</param>
		/// <param name="route">The route.</param>
		/// <returns></returns>
		public static void Pager<T>(this HtmlHelper helper, IPagedCollection<T> collection, string route)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			var pager = new PagerHelper<T>(helper.ViewContext, collection, route);

			pager.Render();
		}

		/// <summary>
		/// Renders the message.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="value">The value.</param>
		public static void Message(this HtmlHelper helper, IValue value)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (value.Message == null)
			{
				return;
			}

			var message = new MessageHelper(helper.ViewContext, value.Message.Message, value.Message.Scope);

			message.Render();
		}

		/// <summary>
		/// Renders the validation result when errors have occurred.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static void ValidationResult(this HtmlHelper helper)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			if (helper.ViewData.ModelState.IsValid)
			{
				return;
			}

			var messages = helper.ViewData.ModelState.Values.SelectMany(state => state.Errors);
			var message = new MessageHelper(helper.ViewContext, MessageScope.Error, messages.Select(x => x.ErrorMessage).ToList());

			message.Render();
		}
	}
}