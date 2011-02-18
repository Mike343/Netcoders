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
using System.Web.Routing;
using Coders.Extensions;
using Coders.Models.Settings;
using Coders.Models.Users;
#endregion

namespace Coders.Web.Extensions
{
	public static class UserExtension
	{
		/// <summary>
		/// Gets the avatar image for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public static IHtmlString AvatarImage(this User user)
		{
			return AvatarImage(user, null, new HttpContextWrapper(HttpContext.Current));
		}

		/// <summary>
		/// Gets the avatar image for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns></returns>
		public static IHtmlString AvatarImage(this User user, object attributes)
		{
			return AvatarImage(user, attributes, new HttpContextWrapper(HttpContext.Current));
		}

		/// <summary>
		/// Gets the avatar image for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="attributes">The attributes.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public static IHtmlString AvatarImage(this User user, object attributes, HttpContextBase context)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			var source = (user.Avatar != null) ?
				user.Avatar.FullPath :
				string.Concat(Setting.UserAvatarPath.Value, "/", Setting.UserAvatarDefault.Value);

			var builder = new TagBuilder("img");

			builder.MergeAttribute("src", source.AsRoot(context));
			builder.MergeAttribute("alt", user.Name);
			builder.MergeAttributes(new RouteValueDictionary(attributes));

			return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
		}
	}
}