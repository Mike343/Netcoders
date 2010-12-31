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
using System.Globalization;
using System.Text;
#endregion

namespace Coders.Extensions
{
	public static class StringExtension
	{
		// private cosntants
		public const int IndentMultiplier = 2;
		private const double Kb = 1024;
		private const double Mb = 1024 * Kb;
		private const double Gb = 1024 * Mb;
		private const double Tb = 1024 * Gb;

		/// <summary>
		/// Gets the value or null.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string GetValueOrNull(this string value)
		{
			return string.IsNullOrEmpty(value) ? null : value;
		}

		/// <summary>
		/// Slugs the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string Slug(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}

			var builder = new StringBuilder();
			var slug = value.Trim().ToLowerInvariant();

			foreach (var c in slug)
			{
				switch (c)
				{
					case ' ':
						builder.Append("-");
						break;
					case '&':
						builder.Append("and");
						break;
					default:

						if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') && c != '-')
						{
							builder.Append(c);
						}

						break;
				}
			}

			for (var i = 0; i < builder.Length; i++)
			{
				if (i != (builder.Length - 1))
				{
					continue;
				}

				if (builder[i] == '-')
				{
					builder.Remove(i, 1);
				}
			}

			builder.Replace("--", "-");

			return builder.ToString();
		}

		/// <summary>
		/// Indents the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		public static string Indent(this string value, int count)
		{
			return Indent(value, count, string.Empty);
		}

		/// <summary>
		/// Indents the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="count">The count.</param>
		/// <param name="indenter">The indenter.</param>
		/// <returns></returns>
		public static string Indent(this string value, int count, string indenter)
		{
			if (string.IsNullOrEmpty(indenter))
			{
				return string.Concat(string.Empty.PadLeft(count * IndentMultiplier), value);
			}

			var indent = string.Empty;

			for (var i = 1; i <= count; i++)
			{
				indent = string.Concat(indent, indenter);
			}

			return string.Concat(indent, string.Empty.PadLeft(1), value);
		}

		/// <summary>
		/// Formats the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		public static string FormatInvariant(this string value, params object[] parameters)
		{
			return string.Format(CultureInfo.InvariantCulture, value, parameters);
		}

		/// <summary>
		/// Truncates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="limit">The limit.</param>
		/// <returns></returns>
		public static string Truncate(this string value, int limit)
		{
			return Truncate(value, limit, "...");
		}

		/// <summary>
		/// Truncates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="limit">The limit.</param>
		/// <param name="ending">The ending.</param>
		/// <returns></returns>
		public static string Truncate(this string value, int limit, string ending)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}

			return (value.Length > limit) ? string.Concat(value.Substring(0, Math.Min(value.Length, limit)), ending) : value;
		}

		/// <summary>
		/// Uppercases the first letter in the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string UppercaseFirst(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}

			var character = value.ToCharArray();

			character[0] = char.ToUpper(character[0], CultureInfo.InvariantCulture);

			return new string(character);
		}

		/// <summary>
		/// Returns a readable file size from the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string AsReadableSize(this long value)
		{
			return AsReadableSize(Convert.ToInt32(value));
		}

		/// <summary>
		/// Returns a readable file size from the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string AsReadableSize(this int value)
		{
			if (value < Kb)
			{
				return "{0} Bytes".FormatInvariant(value);
			}

			if (value < Mb)
			{
				return "{0} KB".FormatInvariant(Math.Round(value / Kb, 2));
			}

			if (value < Gb)
			{
				return "{0} MB".FormatInvariant(Math.Round(value / Mb, 2));
			}

			return value < Tb ? "{0} GB".FormatInvariant(Math.Round(value / Gb, 2)) : "{0} TB".FormatInvariant(Math.Round(value / Tb, 2));
		}
	}
}