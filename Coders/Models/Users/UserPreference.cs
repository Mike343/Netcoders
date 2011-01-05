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
using Coders.Models.Countries;
using Coders.Models.TimeZones;
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Models.Users
{
	public class UserPreference : EntityBase
	{
		/// <summary>
		/// Gets or sets the DST.
		/// </summary>
		/// <value>The DST.</value>
		public virtual UserPreferenceDaylightSavingTime Dst
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the start of week.
		/// </summary>
		/// <value>The start of week.</value>
		public virtual UserPreferenceStartOfWeek StartOfWeek
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the time format.
		/// </summary>
		/// <value>The time format.</value>
		public virtual UserPreferenceTimeFormat TimeFormat
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>The country.</value>
		public virtual Country Country
		{
			get; 
			set; 
		}

		/// <summary>
		/// Gets or sets the time zone.
		/// </summary>
		/// <value>The time zone.</value>
		public virtual TimeZone TimeZone
		{
			get;
			set;
		}
	}
}