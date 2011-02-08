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
using System.Linq;
using System.Reflection;
#endregion

namespace Coders.Extensions
{
	public static class TypeExtension
	{
		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static object GetProperty(this string name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}

			if (value != null)
			{
				var type = value.GetType();

				if (name.IndexOf('.') > -1)
				{
					var properties = name.Split(new char[] { '.' }, 2);
					var info = type.GetProperty(properties[0]);

					return properties[1].GetProperty(info.GetValue(value, null));
				}

				return type.GetProperty(name).GetValue(value, null);
			}

			return null;
		}

		/// <summary>
		/// Gets the types that implement the specified interface.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		public static IEnumerable<Type> GetTypesThatImplement<T>(this Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}

			return assembly.GetTypes().Where(type => (typeof(T).IsAssignableFrom(type) && !type.IsInterface) && !type.IsAbstract);
		}

		/// <summary>
		/// Activates the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static T Activate<T>(this Type type) where T : class
		{
			return Activator.CreateInstance(type) as T;
		}
	}
}