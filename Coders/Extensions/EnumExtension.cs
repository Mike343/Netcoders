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
using System.Linq;
#endregion

namespace Coders.Extensions
{
	public static class EnumExtension
	{
		/// <summary>
		/// Determines whether the specified enum has any of the specified values.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="values">The values.</param>
		/// <returns>
		/// 	<c>true</c> if the specified enum has any of the specified values; otherwise, <c>false</c>.
		/// </returns>
		public static bool Has<T>(this Enum source, params T[] values)
		{
			var value = Convert.ToInt32(source, CultureInfo.InvariantCulture);

			return values.Select(item => Convert.ToInt32(item, CultureInfo.InvariantCulture)).All(mask => (value & mask) != 0);
		}

		/// <summary>
		/// Determines whether the specified enum has any of the specified values.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="values">The values.</param>
		/// <returns>
		/// 	<c>true</c> if the specified enum has any of the specified values; otherwise, <c>false</c>.
		/// </returns>
		public static bool Has<T>(this Enum source, T values)
		{
			var value = Convert.ToInt32(source, CultureInfo.InvariantCulture);
			var mask = Convert.ToInt32(values, CultureInfo.InvariantCulture);

			return (value & mask) != 0;
		}

		/// <summary>
		/// Adds the specified value to the enum.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="original">The original.</param>
		/// <returns></returns>
		public static T Add<T>(this Enum source, T original)
		{
			var value = Convert.ToInt32(source, CultureInfo.InvariantCulture);
			var mask = Convert.ToInt32(original, CultureInfo.InvariantCulture);

			return Enum.ToObject(typeof(T), value | mask).AsEnum<T>();
		}

		/// <summary>
		/// Removes the specified value from the enum.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="original">The original.</param>
		/// <returns></returns>
		public static T Remove<T>(this Enum source, T original)
		{
			var value = Convert.ToInt32(source, CultureInfo.InvariantCulture);
			var mask = Convert.ToInt32(original, CultureInfo.InvariantCulture);

			return Enum.ToObject(typeof(T), value & ~mask).AsEnum<T>();
		}

		/// <summary>
		/// Gets the default name of the specified enum.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static string Name(this Enum source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			return Enum.GetName(source.GetType(), source);
		}

		/// <summary>
		/// Returns the string as the specified enum and value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T AsEnum<T>(this string value)
		{
			return Enum.Parse(typeof(T), value, true).AsEnum<T>();
		}

		/// <summary>
		/// Returns the object as the specified enum.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T AsEnum<T>(this object value)
		{
			return (value is T) ? (T)value : default(T);
		}
	}
}