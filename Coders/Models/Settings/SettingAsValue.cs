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

namespace Coders.Models.Settings
{
	public partial class Setting
	{
		/// <summary>
		/// Gets the site title.
		/// </summary>
		/// <value>The site title.</value>
		public static SettingValue SiteTitle
		{
			get
			{
				return new SettingValue("site.title");
			}
		}

		/// <summary>
		/// Gets the site URL.
		/// </summary>
		/// <value>The site URL.</value>
		public static SettingValue SiteUrl
		{
			get
			{
				return new SettingValue("site.url");
			}
		}

		/// <summary>
		/// Gets the site email.
		/// </summary>
		/// <value>The site email.</value>
		public static SettingValue SiteEmail
		{
			get
			{
				return new SettingValue("site.email");
			}
		}

		/// <summary>
		/// Gets the image extension.
		/// </summary>
		/// <value>The image extension.</value>
		public static SettingValue ImageExtension
		{
			get
			{
				return new SettingValue("image.extension");
			}
		}

		/// <summary>
		/// Gets the attachment path.
		/// </summary>
		/// <value>The attachment path.</value>
		public static SettingValue AttachmentPath
		{
			get
			{
				return new SettingValue("attachment.path");
			}
		}

		/// <summary>
		/// Gets the attachment rule page limit.
		/// </summary>
		/// <value>The attachment rule page limit.</value>
		public static SettingValueAsInt AttachmentRulePageLimit
		{
			get
			{
				return new SettingValueAsInt("attachment.rule.page.limit");
			}
		}

		/// <summary>
		/// Gets the attachment thumb aspect.
		/// </summary>
		/// <value>The attachment thumb aspect.</value>
		public static SettingValueAsBoolean AttachmentThumbAspect
		{
			get
			{
				return new SettingValueAsBoolean("attachment.thumb.aspect");
			}
		}

		/// <summary>
		/// Gets the country flag path.
		/// </summary>
		/// <value>The country flag path.</value>
		public static SettingValue CountryFlagPath
		{
			get
			{
				return new SettingValue("country.flag.path");
			}
		}

		/// <summary>
		/// Gets the country default.
		/// </summary>
		/// <value>The country default.</value>
		public static SettingValueAsInt CountryDefault
		{
			get
			{
				return new SettingValueAsInt("country.default");
			}
		}

		/// <summary>
		/// Gets the country page limit.
		/// </summary>
		/// <value>The country page limit.</value>
		public static SettingValueAsInt CountryPageLimit
		{
			get
			{
				return new SettingValueAsInt("country.page.limit");
			}
		}

		/// <summary>
		/// Gets the log page limit.
		/// </summary>
		/// <value>The log page limit.</value>
		public static SettingValueAsInt LogPageLimit
		{
			get
			{
				return new SettingValueAsInt("log.page.limit");
			}
		}

		/// <summary>
		/// Gets the setting page limit.
		/// </summary>
		/// <value>The setting page limit.</value>
		public static SettingValueAsInt SettingPageLimit
		{
			get
			{
				return new SettingValueAsInt("setting.page.limit");
			}
		}

		/// <summary>
		/// Gets the time zone default.
		/// </summary>
		/// <value>The time zone default.</value>
		public static SettingValueAsInt TimeZoneDefault
		{
			get
			{
				return new SettingValueAsInt("timezone.default");
			}
		}

		/// <summary>
		/// Gets the time zone page limit.
		/// </summary>
		/// <value>The time zone page limit.</value>
		public static SettingValueAsInt TimeZonePageLimit
		{
			get
			{
				return new SettingValueAsInt("timezone.page.limit");
			}
		}

		/// <summary>
		/// Gets the user page limit.
		/// </summary>
		/// <value>The user page limit.</value>
		public static SettingValueAsInt UserPageLimit
		{
			get
			{
				return new SettingValueAsInt("user.page.limit");
			}
		}

		/// <summary>
		/// Gets the length of the user password salt.
		/// </summary>
		/// <value>The length of the user password salt.</value>
		public static SettingValueAsInt UserPasswordSaltLength
		{
			get
			{
				return new SettingValueAsInt("user.password.salt.length");
			}
		}

		/// <summary>
		/// Gets the user avatar path.
		/// </summary>
		/// <value>The user avatar path.</value>
		public static SettingValue UserAvatarPath
		{
			get
			{
				return new SettingValue("user.avatar.path");
			}
		}

		/// <summary>
		/// Gets the user avatar default.
		/// </summary>
		/// <value>The user avatar default.</value>
		public static SettingValue UserAvatarDefault
		{
			get
			{
				return new SettingValue("user.avatar.default");
			}
		}

		/// <summary>
		/// Gets the user avatar page limit.
		/// </summary>
		/// <value>The user avatar page limit.</value>
		public static SettingValueAsInt UserAvatarPageLimit
		{
			get
			{
				return new SettingValueAsInt("user.avatar.page.limit");
			}
		}

		/// <summary>
		/// Gets the width of the user avatar max.
		/// </summary>
		/// <value>The width of the user avatar max.</value>
		public static SettingValueAsInt UserAvatarMaxWidth
		{
			get
			{
				return new SettingValueAsInt("user.avatar.max.width");
			}
		}

		/// <summary>
		/// Gets the height of the user avatar max.
		/// </summary>
		/// <value>The height of the user avatar max.</value>
		public static SettingValueAsInt UserAvatarMaxHeight
		{
			get
			{
				return new SettingValueAsInt("user.avatar.max.height");
			}
		}

		/// <summary>
		/// Gets the user ban page limit.
		/// </summary>
		/// <value>The user ban page limit.</value>
		public static SettingValueAsInt UserBanPageLimit
		{
			get
			{
				return new SettingValueAsInt("user.ban.page.limit");
			}
		}

		/// <summary>
		/// Gets the user search page limit.
		/// </summary>
		/// <value>The user search page limit.</value>
		public static SettingValueAsInt UserSearchPageLimit
		{
			get
			{
				return new SettingValueAsInt("user.search.page.limit");
			}
		}

		/// <summary>
		/// Gets the user host search page limit.
		/// </summary>
		/// <value>The user host search page limit.</value>
		public static SettingValueAsInt UserHostSearchPageLimit
		{
			get
			{
				return new SettingValueAsInt("user.host.search.page.limit");
			}
		}

		/// <summary>
		/// Gets the template email user reset password.
		/// </summary>
		/// <value>The template email user reset password.</value>
		public static SettingValue TemplateEmailUserResetPassword
		{
			get
			{
				return new SettingValue("template.email.user.reset.password");
			}
		}

		/// <summary>
		/// Gets the template widget user log on view.
		/// </summary>
		/// <value>The template widget user log on view.</value>
		public static SettingValue TemplateWidgetUserLogOnView
		{
			get
			{
				return new SettingValue("template.widgets.user.login.view");
			}
		}

		/// <summary>
		/// Gets the template widget user guest log on view.
		/// </summary>
		/// <value>The template widget user guest log on view.</value>
		public static SettingValue TemplateWidgetUserGuestLogOnView
		{
			get
			{
				return new SettingValue("template.widgets.user.guest.login.view");
			}
		}
	}
}