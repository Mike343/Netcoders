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
using Coders.Exceptions;
using Coders.Extensions;
using Coders.Strings;
#endregion

namespace Coders
{
	public abstract class ServiceLocatorBase : IServiceLocator
	{
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetInstance<T>()
		{
			return GetInstance<T>(null);
		}

		/// <summary>
		/// Gets the instance by the specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public T GetInstance<T>(string key)
		{
			try
			{
				return TryGetInstance<T>(key);
			}
			catch (Exception exception)
			{
				throw new ActivationException(Errors.ActivationFailed.FormatInvariant(typeof(T)), exception);
			}
		}

		/// <summary>
		/// Gets the instance by the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object GetInstance(Type type)
		{
			return GetInstance(type, null);
		}

		/// <summary>
		/// Gets the instance by the specified type and key.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public object GetInstance(Type type, string key)
		{
			try
			{
				return TryGetInstance(type, key);
			}
			catch (Exception exception)
			{
				throw new ActivationException(Errors.ActivationFailed.FormatInvariant(type), exception);
			}
		}

		/// <summary>
		/// Releases the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		public void Release(object instance)
		{
			TryRelease(instance);
		}

		/// <summary>
		/// Tries to get instance by the specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		protected abstract T TryGetInstance<T>(string key);

		/// <summary>
		/// Tries to get instance by the specified type and  key.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		protected abstract object TryGetInstance(Type type, string key);

		/// <summary>
		/// Tries to release the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		protected abstract void TryRelease(object instance);
	}
}