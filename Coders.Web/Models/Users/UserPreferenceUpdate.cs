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
using Coders.Models.Common.Enums;
using Coders.Models.Countries;
using Coders.Models.Countries.Enums;
using Coders.Models.TimeZones;
using Coders.Models.TimeZones.Enums;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserPreferenceUpdate : Value<UserPreference>
	{
		// private fields
		private readonly ICountryService _countryService;
		private readonly ITimeZoneService _timeZoneService;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserPreferenceUpdate"/> class.
		/// </summary>
		public UserPreferenceUpdate()
			: this(
			ServiceLocator.Current.GetInstance<ICountryService>(),
			ServiceLocator.Current.GetInstance<ITimeZoneService>())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserPreferenceUpdate"/> class.
		/// </summary>
		/// <param name="preference">The preference.</param>
		public UserPreferenceUpdate(UserPreference preference)
			: this(
			ServiceLocator.Current.GetInstance<ICountryService>(),
			ServiceLocator.Current.GetInstance<ITimeZoneService>())
		{
			if (preference == null)
			{
				throw new ArgumentNullException("preference");
			}

			this.CountryId = preference.Country.Id;
			this.TimeZoneId = preference.TimeZone.Id;
			this.Dst = preference.Dst;
			this.StartOfWeek = preference.StartOfWeek;
			this.TimeFormat = preference.TimeFormat;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserPreferenceUpdate"/> class.
		/// </summary>
		/// <param name="countryService">The country service.</param>
		/// <param name="timeZoneService">The time zone service.</param>
		public UserPreferenceUpdate(ICountryService countryService, ITimeZoneService timeZoneService)
		{
			if (countryService == null)
			{
				throw new ArgumentNullException("countryService");
			}

			if (timeZoneService == null)
			{
				throw new ArgumentNullException("timeZoneService");
			}

			_countryService = countryService;
			_timeZoneService = timeZoneService;
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
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			this.Countries = _countryService.GetAll(new CountrySpecification
			{
				Sort = SortCountry.Title,
				Order = SortOrder.Ascending
			});

			this.TimeZones = _timeZoneService.GetAll(new TimeZoneSpecification
			{
				Sort = SortTimeZone.Offset,
				Order = SortOrder.Ascending
			});

			this.Dsts = Enum.GetNames(typeof(UserPreferenceDaylightSavingTime)).ToList();
			this.StartOfWeeks = Enum.GetNames(typeof(UserPreferenceStartOfWeek)).ToList();
			this.TimeFormats = Enum.GetNames(typeof(UserPreferenceTimeFormat)).ToList();
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(UserPreference entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Dst = this.Dst;
			entity.StartOfWeek = this.StartOfWeek;
			entity.TimeFormat = this.TimeFormat;
			entity.Country = this.Countries.FirstOrDefault(x => x.Id == this.CountryId);
			entity.TimeZone = this.TimeZones.FirstOrDefault(x => x.Id == this.TimeZoneId);
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new UserPreferenceUpdateValidatorCollection().Validate(this);
		}
	}

	public class UserPreferenceUpdateValidatorCollection : AbstractValidator<UserPreferenceUpdate>
	{

	}
}