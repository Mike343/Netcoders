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
using System.Web;
using System.Web.Mvc;
using Coders.Extensions;
using Coders.Models.Countries;
using Coders.Models.Settings;
#endregion

namespace Coders.Web.Extensions
{
	public static class CountryExtension
	{
		/// <summary>
		/// Gets the flag image for the specified country.
		/// </summary>
		/// <param name="country">The country.</param>
		/// <returns></returns>
		public static IHtmlString FlagImage(this Country country)
		{
			return FlagImage(country, new HttpContextWrapper(HttpContext.Current));
		}

		/// <summary>
		/// Gets the flag image for the specified country.
		/// </summary>
		/// <param name="country">The country.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public static IHtmlString FlagImage(this Country country, HttpContextBase context)
		{
			if (country == null)
			{
				throw new ArgumentNullException("country");
			}

			var builder = new TagBuilder("img");
			var source = string.Concat(Setting.CountryFlagPath.Value, "/", country.Code, ".gif");

			builder.MergeAttribute("src", source.AsRoot(context));
			builder.MergeAttribute("alt", country.Title);

			return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
		}
	}
}