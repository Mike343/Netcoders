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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#endregion

namespace Coders.Extensions
{
	public static class InstallerExtension
	{
		/// <summary>
		/// Gets the installer instances from the specified assemblies.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		/// <returns></returns>
		public static IEnumerable<IInstaller> GetInstances(this IEnumerable<string> assemblies)
		{
			return assemblies.Select(Assembly.Load).SelectMany(
					assembly => assembly
						.GetTypesThatImplement<IInstaller>()
						.Select(type => type.Activate<IInstaller>())
						.Where(instance => instance != null)
			);
		}
	}
}