﻿#region License
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
using System.Threading;
using System.Web.Security;
using Coders.DependencyResolution;
using Coders.Models;
using Coders.Web.Authentication;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Web.Mvc;
#endregion

namespace Coders.Web
{
	public class ApplicationHttpModule : NinjectHttpApplication
	{
		/// <summary>
		/// Handles the EndRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected virtual void Application_EndRequest(object sender, EventArgs args)
		{
			ServiceLocator.Current.GetInstance<IUnitOfWork>().Dispose();
		}

		/// <summary>
		/// Handles the OnPostAuthenticateRequest event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected virtual void Application_OnPostAuthenticateRequest(object sender, EventArgs args)
		{
			if (Context == null)
			{
				return;
			}

			var principal = Context.User;

			if (principal != null && principal.Identity.IsAuthenticated)
			{
				var identity = principal.Identity as FormsIdentity;

				if (identity == null)
				{
					return;
				}

				CreatePrincipal(identity.Ticket, true);
			}
			else
			{
				CreatePrincipal(
					new FormsAuthenticationTicket(
						1,
						Guid.Empty.ToString(),
						DateTime.Now,
						DateTime.Now.AddMinutes(30),
						false,
						string.Empty
					),
					false
				);
			}
		}

		/// <summary>
		/// Creates the kernel.
		/// </summary>
		/// <returns></returns>
		protected override IKernel CreateKernel()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new[] { "*.DependencyResolution.dll", "*.Repositories.dll", "*.Web.dll" });

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));

			return kernel;
		}

		/// <summary>
		/// Called when [application started].
		/// </summary>
		protected override void OnApplicationStarted()
		{
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

			var application = new Application();

			application.Setup();
			application.Initialize();
		}

		/// <summary>
		/// Creates the principal.
		/// </summary>
		/// <param name="ticket">The ticket.</param>
		/// <param name="authenticated">if set to <c>true</c> [authenticated].</param>
		private void CreatePrincipal(FormsAuthenticationTicket ticket, bool authenticated)
		{
			var identity = new WebUserIdentity(ticket, authenticated);

			Context.User = new WebPrivilegePrincipal(identity);

			Thread.CurrentPrincipal = Context.User;
		}
	}
}