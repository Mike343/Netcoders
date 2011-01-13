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
using Coders.Web.Extensions;
#endregion

namespace Coders.Web.Routes
{
	public static class UsersRoutes
	{
		// public constants
		public const string Index = "user.index";
		public const string Detail = "user.detail";
		public const string Create = "user.create";
		public const string Update = "user.update";
		public const string AuthLogOn = "user.auth.logon";
		public const string AuthLogOff = "user.auth.logoff";
		public const string AuthReset = "user.auth.reset";
		public const string AuthUpdate = "user.auth.update";
		public const string SearchIndex = "user.search.index";
		public const string SearchCreate = "user.search.create";
		public const string SearchName = "user.search.name";
		public const string PreferenceUpdate = "user.preference.update";
		public const string AvatarIndex = "user.avatar.index";
		public const string AvatarSelect = "user.avatar.select";
		public const string AvatarCreate = "user.avatar.create";
		public const string AvatarDelete = "user.avatar.delete";

		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public static void RegisterRoutes(RouteCollection routes)
		{
			#region Avatar
			routes.CreateArea("users", "Coders.Web.Controllers.Users",
				routes.MapRoute(AvatarIndex, "account/avatars/all/{page}", new { controller = "avatar", action = "index", page = "1" }),
				routes.MapRoute(AvatarSelect, "account/avatars/assign/{id}", new { controller = "avatar", action = "assign", id = "" }),
				routes.MapRoute(AvatarDelete, "account/avatars/delete/{id}", new { controller = "avatar", action = "delete", id = "" }),
				routes.MapRoute(AvatarCreate, "account/avatars/create", new { controller = "avatar", action = "create" })
			);
			#endregion

			#region Preference
			routes.CreateArea("users", "Coders.Web.Controllers.Users",
				routes.MapRoute(PreferenceUpdate, "account/preference/update", new { controller = "preference", action = "update" })
			);
			#endregion

			#region Search
			routes.CreateArea("users", "Coders.Web.Controllers.Users",
				routes.MapRoute(SearchIndex, "users/search/results/{id}/{page}", new { controller = "search", action = "index", id = "", page = "1" }),
				routes.MapRoute(SearchCreate, "users/search/create", new { controller = "search", action = "create" })
			);
			#endregion

			#region Auth
			routes.CreateArea("users", "Coders.Web.Controllers.Users",
				routes.MapRoute(AuthLogOn, "account/logon", new { controller = "auth", action = "logon" }),
				routes.MapRoute(AuthLogOff, "account/logoff", new { controller = "auth", action = "logoff" }),
				routes.MapRoute(AuthReset, "account/password/reset", new { controller = "auth", action = "reset" }),
				routes.MapRoute(AuthUpdate, "account/password/update", new { controller = "auth", action = "update" })
			);
			#endregion

			#region User
			routes.CreateArea("users", "Coders.Web.Controllers.Users",
				routes.MapRoute(Create, "account/create", new { controller = "home", action = "create" }),
				routes.MapRoute(Update, "account/update", new { controller = "home", action = "update" }),
				routes.MapRoute(Index, "users/all/{sort}/{order}/{page}", new { controller = "home", action = "index", sort = "name", order = "ascending", page = "1" }),
				routes.MapRoute(Detail, "users/{slug}/{id}", new { controller = "home", action = "detail", id = "", slug = "" })
			);
			#endregion
		}
	}
}