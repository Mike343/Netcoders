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
	}
}