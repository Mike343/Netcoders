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
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject.Modules;
#endregion

namespace Coders.Web.Widgets
{
	public class WidgetModule : NinjectModule
	{
		/// <summary>
		/// Loads this instance.
		/// </summary>
		public override void Load()
		{
			var path = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
				? AppDomain.CurrentDomain.BaseDirectory
				: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);

			var assemblies = Directory.GetFiles(path, "*.Web.dll")
				.Select(Path.GetFileNameWithoutExtension);

			var types = assemblies.Select(Assembly.Load)
				.SelectMany(
					assembly => assembly.GetTypes().Where(
						type => (typeof(IWidget).IsAssignableFrom(type)) && !type.IsInterface && !type.IsAbstract
					)
					.Select(x => x)
				);

			foreach (var type in types)
			{
				var attributes = type.GetCustomAttributes(typeof(WidgetAttribute), false);

				if (attributes.Length <= 0)
				{
					continue;
				}

				var instance = attributes[0] as WidgetAttribute;

				if (instance == null)
				{
					continue;
				}

				Bind<IWidget>().To(type).Named(instance.Name);
			}
		}
	}
}