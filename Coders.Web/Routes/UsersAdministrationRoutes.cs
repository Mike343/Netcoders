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
	public static class UsersAdministrationRoutes
	{
		// constants
		public const string HomeIndex = "administration.user.home.index";
		public const string HomeHistory = "administration.user.home.history";
		public const string HomeCreate = "administration.user.home.create";
		public const string HomeUpdate = "administration.user.home.update";
		public const string HomeDelete = "administration.user.home.delete";
		public const string HomeReset = "administration.user.home.reset";
		public const string RoleIndex = "administration.user.role.index";
		public const string RoleCreate = "administration.user.role.create";
		public const string RoleUpdate = "administration.user.role.update";
		public const string RoleDelete = "administration.user.role.delete";
		public const string RolePrivilege = "administration.user.role.privilege";
		public const string BanIndex = "administration.user.ban.index";
		public const string BanCreate = "administration.user.ban.create";
		public const string BanUpdate = "administration.user.ban.update";
		public const string BanDelete = "administration.user.ban.delete";
		public const string SearchIndex = "administration.user.search.index";
		public const string SearchCreate = "administration.user.search.create";
		public const string SearchDelete = "administration.user.search.delete";
		public const string HostSearchIndex = "administration.user.host.search.index";
		public const string HostSearchCreate = "administration.user.host.search.create";
		public const string HostSearchDelete = "administration.user.host.search.delete";

		public static void RegisterRoutes(RouteCollection routes)
		{
			#region Host Search
			routes.CreateArea("administration/users", "Coders.Web.Controllers.Users.Administration",
				routes.MapRoute(HostSearchIndex, "administration/users/hosts/search/results/{id}/{page}", new { controller = "hostsearch", action = "index", id = "", page = "1" }),
				routes.MapRoute(HostSearchDelete, "administration/users/hosts/search/delete/{id}", new { controller = "hostsearch", action = "delete", id = "" }),
				routes.MapRoute(HostSearchCreate, "administration/users/hosts/search/create", new { controller = "hostsearch", action = "create" })
			);
			#endregion

			#region Search
			routes.CreateArea("administration/users", "Coders.Web.Controllers.Users.Administration",
				routes.MapRoute(SearchIndex, "administration/users/search/results/{id}/{page}", new { controller = "search", action = "index", id = "", page = "1" }),
				routes.MapRoute(SearchDelete, "administration/users/search/delete/{id}", new { controller = "search", action = "delete", id = "" }),
				routes.MapRoute(SearchCreate, "administration/users/search/create", new { controller = "search", action = "create" })
			);
			#endregion

			#region Ban
			routes.CreateArea("administration/users", "Coders.Web.Controllers.Users.Administration",
				routes.MapRoute(BanIndex, "administration/users/bans/all/{page}", new { controller = "ban", action = "index", page = "1" }),
				routes.MapRoute(BanUpdate, "administration/users/bans/update/{id}", new { controller = "ban", action = "update", id = "" }),
				routes.MapRoute(BanDelete, "administration/users/bans/delete/{id}", new { controller = "ban", action = "delete", id = "" }),
				routes.MapRoute(BanCreate, "administration/users/bans/create", new { controller = "ban", action = "create" })
			);
			#endregion

			#region Role
			routes.CreateArea("administration/users", "Coders.Web.Controllers.Users.Administration",
				routes.MapRoute(RoleIndex, "administration/users/roles/all", new { controller = "role", action = "index" }),
				routes.MapRoute(RoleUpdate, "administration/users/roles/update/{id}", new { controller = "role", action = "update", id = "" }),
				routes.MapRoute(RoleDelete, "administration/users/roles/delete/{id}", new { controller = "role", action = "delete", id = "" }),
				routes.MapRoute(RoleCreate, "administration/users/roles/create", new { controller = "role", action = "create" }),
				routes.MapRoute(RolePrivilege, "administration/users/roles/privilege/{id}", new { controller = "role", action = "privilege", id = "" })
			);
			#endregion

			#region User
			routes.CreateArea("administration/users", "Coders.Web.Controllers.Users.Administration",
				routes.MapRoute(HomeIndex, "administration/users/all/{sort}/{order}/{page}", new { controller = "home", action = "index", sort = "name", order = "descending", page = "1" }),
				routes.MapRoute(HomeHistory, "administration/users/history/{sort}/{order}/{page}/{id}", new { controller = "home", action = "history", sort = "created", order = "descending", page = "1", id = "" }),
				routes.MapRoute(HomeUpdate, "administration/users/update/{id}", new { controller = "home", action = "update", id = "" }),
				routes.MapRoute(HomeDelete, "administration/users/delete/{id}", new { controller = "home", action = "delete", id = "" }),
				routes.MapRoute(HomeReset, "administration/users/reset/{id}", new { controller = "home", action = "reset", id = "" }),
				routes.MapRoute(HomeCreate, "administration/users/create", new { controller = "home", action = "create" })
			);
			#endregion
		}
	}
}