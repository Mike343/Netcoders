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
using System.Linq.Expressions;
using System.Web.Mvc;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Strings;
using FluentValidation.Validators;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace Coders.Web.Validators
{
	/// <summary>
	/// Validators that the user is authorized.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class UserAuthorizeValidator<T> : PropertyValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAuthorizeValidator&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="expression">The expression.</param>
		public UserAuthorizeValidator(Expression<Func<T, string>> expression)
			: base(Errors.UserAuthenticationFailed)
		{
			this.Bind = ExpressionHelper.GetExpressionText(expression);
			this.AuthenticationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();
			this.UserService = ServiceLocator.Current.GetInstance<IUserService>();
		}

		/// <summary>
		/// Gets or sets the bind.
		/// </summary>
		/// <value>The bind.</value>
		public string Bind
		{
			get; 
			private set;
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
		/// Gets or sets the user service.
		/// </summary>
		/// <value>The user service.</value>
		public IUserService UserService
		{
			get;
			private set;
		}

		/// <summary>
		/// Determines whether the specified context is valid.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>
		/// 	<c>true</c> if the specified context is valid; otherwise, <c>false</c>.
		/// </returns>
		protected override bool IsValid(PropertyValidatorContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			var address = this.Bind.GetProperty(context.Instance) as string;
			var password = context.PropertyValue as string;

			if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(password))
			{
				var user = this.UserService.GetBy(new UserEmailAddressSpecification(address));

				return user != null && this.AuthenticationService.Authenticate(user, password);
			}

			return false;
		}
	}
}