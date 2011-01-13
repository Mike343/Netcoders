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
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Coders.Extensions;
using Coders.Models.Users.Enums;
using Coders.Strings;
using Coders.Web.Authentication;
#endregion

namespace Coders.Web.Extensions
{
	public static class DateExtension
	{
		// private properties
		private static Dictionary<double, Func<TimeSpan, string>> _map;

		/// <summary>
		/// Personalizes the specified date.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="identity">The identity.</param>
		/// <returns></returns>
		public static string Personalize(this DateTime date, IIdentity identity)
		{
			var value = identity as WebUserIdentity;

			if (!value.IsAuthenticated())
			{
				return date.ToString("MMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
			}

			var adjusted = Adjust(date, identity);

			switch (value.Session.TimeFormat)
			{
				case UserPreferenceTimeFormat.Relative:
					{
						return adjusted.Ago(identity);
					}
				case UserPreferenceTimeFormat.Extended:
					{
						return adjusted.ToString("MMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
					}
				default:
					{
						return adjusted.ToString("M/d/yyyy h:mm tt", CultureInfo.InvariantCulture);
					}
			}
		}

		/// <summary>
		/// Adjusts the specified date.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="identity">The identity.</param>
		/// <returns></returns>
		public static DateTime Adjust(this DateTime date, IIdentity identity)
		{
			var value = identity as WebUserIdentity;

			if (!value.IsAuthenticated())
			{
				return date;
			}

			var zoneF = TimeZoneInfo.Utc;
			var zoneT = TimeZoneInfo.FindSystemTimeZoneById(value.Session.TimeZone);

			return TimeZoneInfo.ConvertTime(date.ToUniversalTime(), zoneF, zoneT);
		}

		/// <summary>
		/// Returns the specified date as an ago representation.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="identity">The identity.</param>
		/// <returns></returns>
		public static string Ago(this DateTime date, IIdentity identity)
		{
			return date > DateTime.Now ? Future(date, identity) : Past(date, identity);
		}

		/// <summary>
		/// Returns specified date as a past representation.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="identity">The identity.</param>
		/// <returns></returns>
		private static string Past(this DateTime date, IIdentity identity)
		{
			Initialize();

			var difference = DateTime.Now.Adjust(identity).Subtract(date.Adjust(identity));
			var minutes = difference.TotalMinutes;

			if (minutes < 0.0)
			{
				minutes = Math.Round(minutes);
			}

			return string.Concat(
				_map.First(n => minutes < n.Key).Value(difference),
				string.Empty.PadLeft(1),
				Titles.Ago.ToLowerInvariant()
			);
		}

		/// <summary>
		/// Returns specified date as a future representation.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="identity">The identity.</param>
		/// <returns></returns>
		private static string Future(this DateTime date, IIdentity identity)
		{
			Initialize();

			var difference = date.Adjust(identity).Subtract(DateTime.Now.Adjust(identity));
			var minutes = difference.TotalMinutes;

			if (minutes < 0.0)
			{
				minutes = Math.Round(minutes);
			}

			return _map.First(n => minutes < n.Key).Value(difference);
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		private static void Initialize()
		{
			if (_map != null && _map.Count > 0)
			{
				return;
			}

			_map = new Dictionary<double, Func<TimeSpan, string>>
			{
				{ 0.75, x => "less than a minute" },
				{ 1.5, x => "about a minute" },
				{ 45, x => "about {0} minutes".FormatInvariant(Math.Round(x.TotalMinutes)) },
				{ 90, x => "about an hour" },
				{ 60 * 24, x => "about {0} hours".FormatInvariant(Math.Round(Math.Abs(x.TotalHours))) },
				{ 60 * 48, x => "about a day" },
				{ 60 * 24 * 30, x => "about {0} days".FormatInvariant(Math.Floor(Math.Round(x.TotalDays))) },
				{ 60 * 24 * 60, x => "about a month" },
				{ 60 * 24 * 365, x => "about {0} months".FormatInvariant(Math.Floor(Math.Round(x.TotalDays / 30))) },
				{ 60 * 24 * 365 * 2, x => "about a year" },
				{ Double.MaxValue, x => "about {0} years".FormatInvariant(Math.Floor(Math.Round(x.TotalDays / 365))) }
			};
		}
	}
}