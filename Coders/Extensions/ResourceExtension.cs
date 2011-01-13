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
using System.Globalization;
using System.Web;
#endregion

namespace Coders.Extensions
{
	public static class ResourceExtension
	{
		/// <summary>
		/// Returns a localized string using the specified key and specified resource.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="resource">The resource.</param>
		/// <returns></returns>
		public static string Localize(this string key, string resource)
		{
			var value = HttpContext.GetGlobalResourceObject(resource, key, CultureInfo.CurrentCulture);

			return value == null ? "{0).{1}".FormatInvariant(resource, key) : value.ToString();
		}
	}
}