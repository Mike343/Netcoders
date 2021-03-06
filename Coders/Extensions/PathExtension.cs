﻿#region License
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
using System.IO;
using System.Web;
using Coders.Exceptions;
using Coders.Strings;
#endregion

namespace Coders.Extensions
{
	public static class PathExtension
	{
		/// <summary>
		/// Returns the absolute path on the file system for the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static string AsPath(this string path)
		{
			var current = HttpContext.Current;

			if (current == null)
			{
				return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
			}

			var context = new HttpContextWrapper(current);

			return GetPath(context, path);
		}

		/// <summary>
		/// Returns the absolute path on the file system for the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public static string AsPath(this string path, HttpContextBase context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			return GetPath(context, path);
		}

		/// <summary>
		/// Returns the website root path for the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static string AsRoot(this string path)
		{
			return AsRoot(path, new HttpContextWrapper(HttpContext.Current));
		}

		/// <summary>
		/// Returns the website root path for the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public static string AsRoot(this string path, HttpContextBase context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			if (context.Request.ApplicationPath == null)
			{
				throw new PathException(Errors.PathFailed.FormatInvariant(path));
			}

			return Path.Combine(context.Request.ApplicationPath, path).Replace("\\", "/");
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		private static string GetPath(HttpContextBase context, string path)
		{
			if (context.Request.PhysicalApplicationPath != null)
			{
				return Path.Combine(context.Request.PhysicalApplicationPath, path.Replace("/", "\\"));
			}

			if (context.Request.ApplicationPath != null)
			{
				return Path.Combine(context.Request.ApplicationPath, path.Replace("/", "\\"));
			}

			throw new PathException(Errors.PathFailed.FormatInvariant(path));
		}
	}
}