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
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
#endregion

namespace Coders.Web
{
	public class ControllerFactory : DefaultControllerFactory
	{
		/// <summary>
		/// Creates the specified controller by using the specified request context.
		/// </summary>
		/// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
		/// <param name="controllerName">The name of the controller.</param>
		/// <returns>A reference to the controller.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext"/> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="controllerName"/> parameter is null or empty.</exception>
		public override IController CreateController(RequestContext requestContext, string controllerName)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}

			var type = GetControllerType(requestContext, controllerName);

			if (type == null)
			{
				throw new HttpException(404, string.Empty);
			}

			return ServiceLocator.Current.GetInstance(type) as IController;
		}

		/// <summary>
		/// Releases the specified controller.
		/// </summary>
		/// <param name="controller">The controller to release.</param>
		public override void ReleaseController(IController controller)
		{
			ServiceLocator.Current.Release(controller);
		}
	}
}