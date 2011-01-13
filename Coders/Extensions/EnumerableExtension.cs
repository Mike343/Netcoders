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
using System.Collections.Generic;
using Coders.Collections;
#endregion

namespace Coders.Extensions
{
	public static class EnumerableExtension
	{
		/// <summary>
		/// Loops through the collection with the specified action.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="values">The values.</param>
		/// <param name="action">The action.</param>
		public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
		{
			if (values == null || action == null)
			{
				return;
			}

			foreach (var value in values)
			{
				action(value);
			}
		}

		/// <summary>
		/// Returns the first result or the default.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static T FirstOrDefault<T>(this IList<T> values) where T : new()
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			return values.Count > 0 ? values[0] : new T();
		}

		/// <summary>
		/// Returns the first result or the default.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static T FirstOrDefault<T>(this IPagedCollection<T> values) where T : new()
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			return FirstOrDefault(values.Items);
		}
	}
}