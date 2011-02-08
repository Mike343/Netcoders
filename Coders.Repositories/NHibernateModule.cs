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
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
#endregion

namespace Coders.Repositories
{
	public class NHibernateModule : NinjectModule
    {
		// private constants
		public const string SessionKey = "Coders.Repositories.HibernateModule.Key";

		// private static fields
		private static ISession _session;

		/// <summary>
		/// Loads this instance.
		/// </summary>
        public override void Load()
        {
			var path = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
				? AppDomain.CurrentDomain.BaseDirectory
				: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);

			var assemblies = Directory.GetFiles(path, "*.Repositories.dll")
				.Select(Path.GetFileNameWithoutExtension);

			var file = HttpContext.Current == null ? "app.config" : "web.config";
			var configuration = new Configuration();

			configuration.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file));

			if (configuration == null)
			{
				throw new InvalidOperationException("The configuration is null.");
			}

			var fluent = Fluently.Configure(configuration).Mappings(map =>
			{
				foreach (var assembly in assemblies.Select(Assembly.Load))
				{
					map.FluentMappings.AddFromAssembly(assembly).Conventions.AddAssembly(assembly);
				}
			});

			Bind<ISessionFactory>().ToConstant(fluent.BuildConfiguration().BuildSessionFactory());
			Bind<ISession>().ToMethod(OnSessionStart).InRequestScope().OnDeactivation(OnSessionEnd);
        }

		/// <summary>
		/// Starts and adds the session the the cache.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		private static ISession OnSessionStart(IContext context)
		{
			if (HttpContext.Current == null)
			{
				return _session ?? (_session = context.Kernel.Get<ISessionFactory>().OpenSession());
			}

			ISession session;

			var cache = HttpContext.Current.Items;

			if (!cache.Contains(SessionKey))
			{
				session = context.Kernel.Get<ISessionFactory>().OpenSession();
				cache.Add(SessionKey, session);
            }
			else
			{
				session = cache[SessionKey] as ISession;
			}

			return session;
		}

		/// <summary>
		/// Ends the session
		/// </summary>
		private static void OnSessionEnd(ISession session)
		{
			if (session != null)
			{
				session.Dispose();
			}

			var context = HttpContext.Current;

			if (context != null)
			{
				HttpContext.Current.Items[SessionKey] = null;
			}
		}
    }
}