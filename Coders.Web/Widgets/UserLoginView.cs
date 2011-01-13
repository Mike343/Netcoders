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
using System.Web.Mvc.Html;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Web.Authentication;
#endregion

namespace Coders.Web.Widgets
{
	[Widget("user.logon.view")]
	public class UserLogOnView : WidgetBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserLogOnView"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		public UserLogOnView(IAuthenticationService authenticationService)
		{
			this.AuthenticationService = authenticationService;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			private set;
		}

		/// <summary>
		/// Renders the widget.
		/// </summary>
		public override void Render()
		{
			var identity = this.AuthenticationService.Identity as WebUserIdentity;

			if (identity.IsAuthenticated())
			{
				Html.RenderPartial(Setting.TemplateWidgetUserLogOnView.Value, identity.Session);
			}
			else
			{
				Html.RenderPartial(Setting.TemplateWidgetUserGuestLogOnView.Value);
			}
		}
	}
}