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
using Coders.Web.Helpers;
#endregion

namespace Coders.Web.Extensions
{
	public static class RouteExtension
	{
		/// <summary>
		/// Returns the route using the specified name.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static RouteHelper Route(this UrlHelper helper, string name)
		{
			return Route(helper, name, null);
		}

		/// <summary>
		/// Returns the route using the specified name.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="name">The name.</param>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static RouteHelper Route(this UrlHelper helper, string name, object values)
		{
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}

			return new RouteHelper(helper.RouteUrl(name, values));
		}

		/// <summary>
		/// Creates the area.
		/// </summary>
		/// <param name="routes">The routes.</param>
		/// <param name="area">The area.</param>
		/// <param name="controllersNamespace">The controllers namespace.</param>
		/// <param name="entries">The entries.</param>
		public static void CreateArea(this RouteCollection routes, string area, string controllersNamespace, params Route[] entries)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}

			if (controllersNamespace == null)
			{
				throw new ArgumentNullException("controllersNamespace");
			}

			if (entries == null)
			{
				throw new ArgumentNullException("entries");
			}

			foreach (var route in entries)
			{
				if (route.Constraints == null)
				{
					route.Constraints = new RouteValueDictionary();
				}

				if (route.Defaults == null)
				{
					route.Defaults = new RouteValueDictionary();
				}

				if (route.DataTokens == null)
				{
					route.DataTokens = new RouteValueDictionary();
				}

				route.DataTokens.Add("area", area);
				route.DataTokens.Add("namespaces", controllersNamespace.Split(','));

				if (!routes.Contains(route))
				{
					routes.Add(route);
				}
			}
		}
	}
}