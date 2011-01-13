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
using Coders.Models.Settings;
using Coders.Models.TimeZones;
using Coders.Models.TimeZones.Enums;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
using FluentValidation.Attributes;
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserCreateValidatorCollection))]
	public class UserCreate : Value<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserCreate"/> class.
		/// </summary>
		public UserCreate()
			: this(
			ServiceLocator.Current.GetInstance<ICountryService>(),
			ServiceLocator.Current.GetInstance<ITimeZoneService>())
		{
			this.CountryId = Setting.CountryDefault.Value;
			this.TimeZoneId = Setting.TimeZoneDefault.Value;
			this.Dst = UserPreferenceDaylightSavingTime.Auto;
			this.StartOfWeek = UserPreferenceStartOfWeek.Monday;
			this.TimeFormat = UserPreferenceTimeFormat.Relative;
			this.Dsts = Enum.GetNames(typeof(UserPreferenceDaylightSavingTime));
			this.StartOfWeeks = Enum.GetNames(typeof(UserPreferenceStartOfWeek));
			this.TimeFormats = Enum.GetNames(typeof(UserPreferenceTimeFormat));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserCreate"/> class.
		/// </summary>
		/// <param name="countryService">The country service.</param>
		/// <param name="timeZoneService">The time zone service.</param>
		public UserCreate(ICountryService countryService, ITimeZoneService timeZoneService)
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
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public string EmailAddress
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
		/// Gets or sets the preference.
		/// </summary>
		/// <value>The preference.</value>
		public UserPreference Preference
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
			entity.EmailAddress = this.EmailAddress;
			entity.Password = this.Password;
			entity.Status = UserStatus.Activated;

			// preference
			this.Preference = new UserPreference
			{
				Dst = this.Dst,
				StartOfWeek = this.StartOfWeek,
				TimeFormat = this.TimeFormat,
				Country = this.Countries.FirstOrDefault(x => x.Id == this.CountryId),
				TimeZone = this.TimeZones.FirstOrDefault(x => x.Id == this.TimeZoneId)
			};
		}
	}

	public class UserCreateValidatorCollection : AbstractValidator<UserCreate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserCreateValidatorCollection"/> class.
		/// </summary>
		public UserCreateValidatorCollection()
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

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Password));

			RuleFor(x => x.Password)
				.Length(6, int.MaxValue)
				.WithMessage(Errors.LengthMin.FormatInvariant(Titles.Password, 6));

			RuleFor(x => x.VerifyPassword)
				.Equal(x => x.Password)
				.WithMessage(Errors.Equal.FormatInvariant(Titles.Password, Titles.VerifyWith.FormatInvariant(Titles.Password)));

			RuleFor(x => x.Name)
				.UserUniqueName();

			RuleFor(x => x.EmailAddress)
				.UserUniqueEmailAddress();
		}
	}
}