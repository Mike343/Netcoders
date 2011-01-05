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
#endregion

namespace Coders
{
	public interface IServiceLocator
	{
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T GetInstance<T>();

		/// <summary>
		/// Gets the instance by the specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		T GetInstance<T>(string key);

		/// <summary>
		/// Gets the instance by the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		object GetInstance(Type type);

		/// <summary>
		/// Gets the instance by the specified type and key.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		object GetInstance(Type type, string key);

		/// <summary>
		/// Gets the instances.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IEnumerable<T> GetInstances<T>();

		/// <summary>
		/// Gets the instances by the specified type and key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		IEnumerable<T> GetInstances<T>(string key);

		/// <summary>
		/// Releases the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		void Release(object instance);
	}
}