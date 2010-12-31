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

namespace Coders
{
	public static class ServiceLocator
	{
		// private fields
		private static ServiceLocatorProvider _current;

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>The current.</value>
		public static IServiceLocator Current
		{
			get
			{
				return _current();
			}
		}

		/// <summary>
		/// Sets the locator provider.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public static void SetLocatorProvider(ServiceLocatorProvider provider)
		{
			_current = provider;
		}
	}
}