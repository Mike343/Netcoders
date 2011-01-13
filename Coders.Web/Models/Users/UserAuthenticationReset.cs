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
using Coders.Web.Extensions;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserAuthenticationResetValidatorCollection))]
	public class UserAuthenticationReset
	{
		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public string EmailAddress
		{
			get;
			set;
		}
	}

	public class UserAuthenticationResetValidatorCollection : AbstractValidator<UserAuthenticationReset>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAuthenticationResetValidatorCollection"/> class.
		/// </summary>
		public UserAuthenticationResetValidatorCollection()
		{
			RuleFor(x => x.EmailAddress).UserEmailAddressMustExist();
		}
	}
}