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
using System.Web.Mvc;
using Coders.Web.Controllers;
using Coders.Web.ViewModels;
#endregion

namespace Coders.Web.ActionResults
{
	public class ErrorResult : ViewResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorResult"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public ErrorResult(string message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get;
			set;
		}

		/// <summary>
		/// When called by the action invoker, renders the view to the response.
		/// </summary>
		/// <param name="context">The context that the result is executed in.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context"/> parameter is null.</exception>
		public override void ExecuteResult(ControllerContext context)
		{
			base.ViewData.Model = new ErrorViewModel
			{
				Message = this.Message
			};

			base.ViewName = Views.Error;

			base.ExecuteResult(context);
		}
	}
}