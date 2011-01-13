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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Coders.DependencyResolution;
using Coders.Models.Settings;
using Coders.Web.ActionFilters;
using Coders.Web.Routes;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
#endregion

namespace Coders.Web
{
	public class Application
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Application"/> class.
		/// </summary>
		public Application()
		{
			this.Modules = new List<IInstaller>();
		}

		/// <summary>
		/// Gets or sets the modules.
		/// </summary>
		/// <value>The modules.</value>
		public IList<IInstaller> Modules
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Application"/> is initialized.
		/// </summary>
		/// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
		public static bool Initialized
		{
			get; 
			private set;
		}

		/// <summary>
		/// Registers the modules.
		/// </summary>
		public void RegisterModules()
		{
			var path = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath) 
				? AppDomain.CurrentDomain.BaseDirectory 
				: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);

			var assemblies = Directory.GetFiles(path, "*.Web.dll")
				.Select(Path.GetFileNameWithoutExtension);

			var instances = assemblies.Select(Assembly.Load)
				.SelectMany(
					assembly => assembly.GetTypes().Where(
						type => (typeof(IInstaller).IsAssignableFrom(type) && !type.IsInterface) && !type.IsAbstract
					)
					.Select(type => Activator.CreateInstance(type) as IInstaller).Where(instance => instance != null)
				);

			foreach (var instance in instances.Where(instance => instance.IsActive))
			{
				this.Modules.Add(instance);
			}
		}

		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public void RegisterRoutes(RouteCollection routes)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			foreach (var module in this.Modules)
			{
				module.RegisterRoutes(routes);
			}

			UsersRoutes.RegisterRoutes(routes);
			AdministrationRoutes.RegisterRoutes(routes);
			CommonRoutes.RegisterRoutes(routes);

			routes.Add("default",
				new Route(
					"{controller}/{action}",
					new RouteValueDictionary(new { controller = "home", action = "index" }),
					new MvcRouteHandler()
				)
			);
		}

		/// <summary>
		/// Setups this instance.
		/// </summary>
		public void Setup()
		{
			Configure();

			this.RegisterModules();
			this.RegisterRoutes(RouteTable.Routes);

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterControllerFactory();

			ModelMetadataProviders.Current = new DataAnnotationsModelMetadataProvider();
			ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			ServiceLocator.Current.GetInstance<ISettingService>().Rebuild();

			foreach (var module in this.Modules)
			{
				module.Initialize();
			}

			Initialized = true;
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		public static void Configure()
		{
			NinjectHelper.Initialize(new[] { "*.DependencyResolution.dll", "*.Repositories.dll", "*.Web.dll" });
		}

		/// <summary>
		/// Registers the global filters.
		/// </summary>
		/// <param name="filters">The filters.</param>
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}

			filters.Add(new HandleErrorAttribute());
			filters.Add(new SessionFilterAttribute());
		}

		/// <summary>
		/// Registers the controller factory.
		/// </summary>
		public static void RegisterControllerFactory()
		{
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
		}
	}
}