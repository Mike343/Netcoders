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
	public static class AdministrationRoutes
	{
		// constants
		public const string HomeIndex = "administration.home.index";
		public const string TimeZoneIndex = "administration.timezone.index";
		public const string TimeZoneUpdate = "administration.timezone.update";
		public const string TimeZoneDelete = "administration.timezone.delete";
		public const string TimeZoneCreate = "administration.timezone.create";
		public const string SettingIndex = "administration.setting.index";
		public const string SettingUpdate = "administration.setting.update";
		public const string SettingDelete = "administration.setting.delete";
		public const string SettingCreate = "administration.setting.create";
		public const string CountryIndex = "administration.country.index";
		public const string CountryUpdate = "administration.country.update";
		public const string CountryDelete = "administration.country.delete";
		public const string CountryCreate = "administration.country.create";
		public const string AttachmentRuleIndex = "administration.attachment.rule.index";
		public const string AttachmentRuleUpdate = "administration.attachment.rule.update";
		public const string AttachmentRuleDelete = "administration.attachment.rule.delete";
		public const string AttachmentRuleCreate = "administration.attachment.rule.create";

		public static void RegisterRoutes(RouteCollection routes)
		{
			#region AttachmentRule
			routes.CreateArea("administration", "Coders.Web.Controllers.Administration",
				routes.MapRoute(AttachmentRuleIndex, "administration/attachments/rules/all/{page}", new { controller = "attachmentrule", action = "index", page = "1" }),
				routes.MapRoute(AttachmentRuleUpdate, "administration/attachments/rules/update/{id}", new { controller = "attachmentrule", action = "update", id = "" }),
				routes.MapRoute(AttachmentRuleDelete, "administration/attachments/rules/delete/{id}", new { controller = "attachmentrule", action = "delete", id = "" }),
				routes.MapRoute(AttachmentRuleCreate, "administration/attachments/rules/create", new { controller = "attachmentrule", action = "create" })
			);
			#endregion

			#region Country
			routes.CreateArea("administration", "Coders.Web.Controllers.Administration",
				routes.MapRoute(CountryIndex, "administration/countries/all/{page}", new { controller = "country", action = "index", page = "1" }),
				routes.MapRoute(CountryUpdate, "administration/countries/update/{id}", new { controller = "country", action = "update", id = "" }),
				routes.MapRoute(CountryDelete, "administration/countries/delete/{id}", new { controller = "country", action = "delete", id = "" }),
				routes.MapRoute(CountryCreate, "administration/countries/create", new { controller = "country", action = "create" })
			);
			#endregion

			#region Setting
			routes.CreateArea("administration", "Coders.Web.Controllers.Administration",
				routes.MapRoute(SettingIndex, "administration/settings/all/{page}", new { controller = "setting", action = "index", page = "1" }),
				routes.MapRoute(SettingUpdate, "administration/settings/update/{id}", new { controller = "setting", action = "update", id = "" }),
				routes.MapRoute(SettingDelete, "administration/settings/delete/{id}", new { controller = "setting", action = "delete", id = "" }),
				routes.MapRoute(SettingCreate, "administration/settings/create", new { controller = "setting", action = "create" })
			);
			#endregion

			#region TimeZone
			routes.CreateArea("administration", "Coders.Web.Controllers.Administration",
				routes.MapRoute(TimeZoneIndex, "administration/timezones/all/{page}", new { controller = "timezone", action = "index", page = "1" }),
				routes.MapRoute(TimeZoneUpdate, "administration/timezones/update/{id}", new { controller = "timezone", action = "update", id = "" }),
				routes.MapRoute(TimeZoneDelete, "administration/timezones/delete/{id}", new { controller = "timezone", action = "delete", id = "" }),
				routes.MapRoute(TimeZoneCreate, "administration/timezones/create", new { controller = "timezone", action = "create" })
			);
			#endregion

			#region Home
			routes.CreateArea("administration", "Coders.Web.Controllers.Administration",
				routes.MapRoute(HomeIndex, "administration", new { controller = "home", action = "index" })
			);
			#endregion
		}
	}
}