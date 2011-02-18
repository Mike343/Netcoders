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
using Coders.Extensions;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserUpdateValidatorCollection))]
	public class UserUpdate : Value<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserUpdate"/> class.
		/// </summary>
		public UserUpdate()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserUpdate"/> class.
		/// </summary>
		/// <param name="user">The user.</param>
		public UserUpdate(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			this.Name = user.Name;
			this.Title = user.Title;
			this.CurrentName = user.Name;
			this.EmailAddress = user.EmailAddress;
			this.CurrentEmailAddress = user.EmailAddress;
			this.VerifyEmailAddress = user.EmailAddress;
			this.Signature = user.Signature;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the current.
		/// </summary>
		/// <value>The name of the current.</value>
		public string CurrentName
		{
			get;
			set;
		}

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
		/// Gets or sets the current email address.
		/// </summary>
		/// <value>The current email address.</value>
		public string CurrentEmailAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the verify email address.
		/// </summary>
		/// <value>The verify email address.</value>
		public string VerifyEmailAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the signature.
		/// </summary>
		/// <value>The signature.</value>
		public string Signature
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a value indicating whether [name changed].
		/// </summary>
		/// <value><c>true</c> if [name changed]; otherwise, <c>false</c>.</value>
		public bool NameChanged
		{
			get
			{
				return this.Name != this.CurrentName;
			}
		}

		/// <summary>
		/// Gets a value indicating whether [email address changed].
		/// </summary>
		/// <value><c>true</c> if [email address changed]; otherwise, <c>false</c>.</value>
		public bool EmailAddressChanged
		{
			get
			{
				return this.EmailAddress != this.CurrentEmailAddress;
			}
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(User entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			// user
			entity.Name = this.Name;
			entity.Title = this.Title;
			entity.EmailAddress = this.EmailAddress;
			entity.Signature = this.Signature;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new UserUpdateValidatorCollection().Validate(this);
		}
	}

	public class UserUpdateValidatorCollection : AbstractValidator<UserUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserUpdateValidatorCollection"/> class.
		/// </summary>
		public UserUpdateValidatorCollection()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Name));

			RuleFor(x => x.Name)
				.Length(4, 30)
				.WithMessage(Errors.LengthMinOrMax.FormatInvariant(Titles.Name, 4, 30));

			RuleFor(x => x.Name)
				.Character()
				.WithMessage(Errors.CharacterNotValid.FormatInvariant(Titles.Name));

			RuleFor(x => x.EmailAddress)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.EmailAddress));

			RuleFor(x => x.EmailAddress)
				.Email()
				.WithMessage(Errors.EmailNotValid.FormatInvariant(Titles.EmailAddress));

			RuleFor(x => x.VerifyEmailAddress)
				.Equal(x => x.EmailAddress)
				.WithMessage(Errors.Equal.FormatInvariant(Titles.EmailAddress, Titles.VerifyWith.FormatInvariant(Titles.EmailAddress)));

			RuleFor(x => x.Name)
				.UserUniqueName()
				.When(x => x.NameChanged);

			RuleFor(x => x.EmailAddress)
				.UserUniqueEmailAddress()
				.When(x => x.EmailAddressChanged);
		}
	}
}