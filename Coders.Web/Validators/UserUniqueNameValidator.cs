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
using Coders.Models.Users;
using Coders.Strings;
using FluentValidation.Validators;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace Coders.Web.Validators
{
	/// <summary>
	/// Validates that the user email address is unique.
	/// </summary>
	public class UserUniqueNameValidator : PropertyValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserUniqueNameValidator"/> class.
		/// </summary>
		public UserUniqueNameValidator()
			: this(ServiceLocator.Current.GetInstance<IUserService>())
		{
			this.UserService = ServiceLocator.Current.GetInstance<IUserService>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserUniqueNameValidator"/> class.
		/// </summary>
		/// <param name="userService">The user service.</param>
		public UserUniqueNameValidator(IUserService userService)
			: base(Errors.UserNameTaken)
		{
			this.UserService = userService;
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

			var name = context.PropertyValue as string;

			if (string.IsNullOrEmpty(name))
			{
				return true;
			}

			var result = this.UserService.Count(new UserNameSpecification(name));

			return (result == 0);
		}
	}
}