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
using System.Web;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
#endregion

namespace Coders.Repositories
{
	public class NHibernateSessionFactoryProvider
	{
		// private fields
		private ISessionFactory _sessionFactory;

		/// <summary>
		/// Gets the session factory.
		/// </summary>
		/// <value>The session factory.</value>
		public ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory != null)
				{
					return _sessionFactory;
				}

				_sessionFactory = CreateSessionFactory();

				return _sessionFactory;
			}
		}

		/// <summary>
		/// Creates the session factory.
		/// </summary>
		/// <returns></returns>
		private ISessionFactory CreateSessionFactory()
		{
			var path = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
				? AppDomain.CurrentDomain.BaseDirectory
				: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);

			var assemblies = Directory.GetFiles(path, "*.Repositories.dll")
				.Select(Path.GetFileNameWithoutExtension);

			var file = HttpContext.Current == null ? "app.config" : "web.config";
			var configuration = new Configuration();

			configuration.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file));

			var fluent = Fluently.Configure(configuration).Mappings(map =>
			{
				foreach (var assembly in assemblies.Select(Assembly.Load))
				{
					map.FluentMappings.AddFromAssembly(assembly).Conventions.AddAssembly(assembly);
				}
			});

			return fluent.BuildConfiguration().BuildSessionFactory();
		}
	}
}