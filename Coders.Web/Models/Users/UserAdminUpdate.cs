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
using System.Collections.Generic;
using System.Linq;
using Coders.Extensions;
using Coders.Models.Common.Enums;
using Coders.Models.Countries;
using Coders.Models.Countries.Enums;
using Coders.Models.TimeZones;
using Coders.Models.TimeZones.Enums;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserAdminUpdate : Value<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAdminUpdate"/> class.
		/// </summary>
		public UserAdminUpdate()
			: this(
			ServiceLocator.Current.GetInstance<ICountryService>(),
			ServiceLocator.Current.GetInstance<ITimeZoneService>())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserAdminUpdate"/> class.
		/// </summary>
		/// <param name="user">The user.</param>
		public UserAdminUpdate(User user)
			: this(
			ServiceLocator.Current.GetInstance<ICountryService>(),
			ServiceLocator.Current.GetInstance<ITimeZoneService>())
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			this.Id = user.Id;
			this.CountryId = user.Preference.Country.Id;
			this.TimeZoneId = user.Preference.TimeZone.Id;
			this.Name = user.Name;
			this.Title = user.Title;
			this.CurrentName = user.Name;
			this.EmailAddress = user.EmailAddress;
			this.CurrentEmailAddress = user.EmailAddress;
			this.VerifyEmailAddress = user.EmailAddress;
			this.Signature = user.Signature;
			this.IsProtected = user.IsProtected;
			this.Status = user.Status;
			this.Dst = user.Preference.Dst;
			this.StartOfWeek = user.Preference.StartOfWeek;
			this.TimeFormat = user.Preference.TimeFormat;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserAdminUpdate"/> class.
		/// </summary>
		/// <param name="countryService">The country service.</param>
		/// <param name="timeZoneService">The time zone service.</param>
		public UserAdminUpdate(ICountryService countryService, ITimeZoneService timeZoneService)
		{
			if (countryService == null)
			{
				throw new ArgumentNullException("countryService");
			}

			if (timeZoneService == null)
			{
				throw new ArgumentNullException("timeZoneService");
			}

			this.Countries = countryService.GetAll(new CountrySpecification
			{
				Sort = SortCountry.Title,
				Order = SortOrder.Ascending
			});

			this.TimeZones = timeZoneService.GetAll(new TimeZoneSpecification
			{
				Sort = SortTimeZone.Offset,
				Order = SortOrder.Ascending
			});

			this.Statuses = Enum.GetNames(typeof(UserStatus));
			this.Dsts = Enum.GetNames(typeof(UserPreferenceDaylightSavingTime));
			this.StartOfWeeks = Enum.GetNames(typeof(UserPreferenceStartOfWeek));
			this.TimeFormats = Enum.GetNames(typeof(UserPreferenceTimeFormat));
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the country id.
		/// </summary>
		/// <value>The country id.</value>
		public int CountryId
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the time zone id.
		/// </summary>
		/// <value>The time zone id.</value>
		public int TimeZoneId
		{
			get;
			set;
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
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the verify password.
		/// </summary>
		/// <value>The verify password.</value>
		public string VerifyPassword
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
		/// Gets or sets a value indicating whether this instance is protected.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is protected; otherwise, <c>false</c>.
		/// </value>
		public bool IsProtected
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
				return !this.Name.Equals(this.CurrentName);
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
				return !this.EmailAddress.Equals(this.CurrentEmailAddress);
			}
		}

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public UserStatus Status
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the DST.
		/// </summary>
		/// <value>The DST.</value>
		public UserPreferenceDaylightSavingTime Dst
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the start of week.
		/// </summary>
		/// <value>The start of week.</value>
		public UserPreferenceStartOfWeek StartOfWeek
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the time format.
		/// </summary>
		/// <value>The time format.</value>
		public UserPreferenceTimeFormat TimeFormat
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the countries.
		/// </summary>
		/// <value>The countries.</value>
		public IList<Country> Countries
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the time zones.
		/// </summary>
		/// <value>The time zones.</value>
		public IList<TimeZone> TimeZones
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the statuses.
		/// </summary>
		/// <value>The statuses.</value>
		public IList<string> Statuses
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the DSTS.
		/// </summary>
		/// <value>The DSTS.</value>
		public IList<string> Dsts
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the start of weeks.
		/// </summary>
		/// <value>The start of weeks.</value>
		public IList<string> StartOfWeeks
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the time formats.
		/// </summary>
		/// <value>The time formats.</value>
		public IList<string> TimeFormats
		{
			get;
			private set;
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
			entity.IsProtected = this.IsProtected;
			entity.Status = this.Status;
		}

		/// <summary>
		/// Converts this instance to the specified preference.
		/// </summary>
		/// <param name="preference">The preference.</param>
		public void ValueToPreference(UserPreference preference)
		{
			if (preference == null)
			{
				throw new ArgumentNullException("preference");
			}

			preference.Dst = this.Dst;
			preference.StartOfWeek = this.StartOfWeek;
			preference.TimeFormat = this.TimeFormat;
			preference.Country = this.Countries.FirstOrDefault(x => x.Id == this.CountryId);
			preference.TimeZone = this.TimeZones.FirstOrDefault(x => x.Id == this.TimeZoneId);
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new UserAdminUpdateValidatorCollection().Validate(this);
		}
	}

	public class UserAdminUpdateValidatorCollection : AbstractValidator<UserAdminUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAdminUpdateValidatorCollection"/> class.
		/// </summary>
		public UserAdminUpdateValidatorCollection()
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

			RuleFor(x => x.Password)
				.Length(6, int.MaxValue)
				.WithMessage(Errors.LengthMin.FormatInvariant(Titles.Password, 6))
				.When(x => !string.IsNullOrEmpty(x.Password));

			RuleFor(x => x.VerifyPassword)
				.Equal(x => x.Password)
				.WithMessage(Errors.Equal.FormatInvariant(Titles.Password, Titles.VerifyWith.FormatInvariant(Titles.Password)));

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