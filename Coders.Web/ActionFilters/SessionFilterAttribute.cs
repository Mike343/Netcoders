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
using System.Web.Mvc;
using Coders.Authentication;
using Coders.Web.Authentication;
#endregion

namespace Coders.Web.ActionFilters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
	public sealed class SessionFilterAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Called by the MVC framework before the action method executes.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}

			var principal = PrivilegePrincipalPermission.Current;
	
			if (principal == null)
			{
				return;
			}

			var identity = principal.Identity as WebUserIdentity;

			if (identity == null)
			{
				return;
			}

			var session = new ApplicationSession();

			session.Create(filterContext.HttpContext, principal, identity);
		}
	}
}