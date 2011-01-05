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
using Ninject;
#endregion

namespace Coders.DependencyResolution
{
	public static class NinjectHelper
	{
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public static void Initialize()
		{
			Initialize(new[] { "*.DependencyResolution.dll", "*.Repositories.dll" });
		}

		/// <summary>
		/// Initializes the specified values.
		/// </summary>
		/// <param name="searchPatterns">The search patterns.</param>
		public static void Initialize(string[] searchPatterns)
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(searchPatterns);

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}
	}
}