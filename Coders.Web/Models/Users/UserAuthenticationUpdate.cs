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

using Coders.Extensions;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserAuthenticationUpdateValidatorCollection))]
	public class UserAuthenticationUpdate
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

		/// <summary>
		/// Gets or sets the current password.
		/// </summary>
		/// <value>The current password.</value>
		public string CurrentPassword
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the new password.
		/// </summary>
		/// <value>The new password.</value>
		public string NewPassword
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the verify new password.
		/// </summary>
		/// <value>The verify new password.</value>
		public string VerifyNewPassword
		{
			get;
			set;
		}
	}

	public class UserAuthenticationUpdateValidatorCollection : AbstractValidator<UserAuthenticationUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAuthenticationUpdateValidatorCollection"/> class.
		/// </summary>
		public UserAuthenticationUpdateValidatorCollection()
		{
			RuleFor(x => x.EmailAddress)
				.UserEmailAddressMustExist();

			RuleFor(x => x.CurrentPassword)
				.UserAuthorize(x => x.EmailAddress);

			RuleFor(x => x.NewPassword)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.NewPassword));

			RuleFor(x => x.NewPassword)
				.Length(6, int.MaxValue)
				.WithMessage(Errors.LengthMin.FormatInvariant(Titles.NewPassword, 6));

			RuleFor(x => x.VerifyNewPassword)
				.Equal(x => x.NewPassword)
				.WithMessage(Errors.Equal.FormatInvariant(Titles.NewPassword, Titles.VerifyWith.FormatInvariant(Titles.NewPassword)));
		}
	}
}