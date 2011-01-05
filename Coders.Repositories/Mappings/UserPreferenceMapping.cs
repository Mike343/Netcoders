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
using Coders.Models.Users;
using FluentNHibernate.Mapping;
#endregion

namespace Coders.Repositories.Mappings
{
	public sealed class UserPreferenceMapping : ClassMap<UserPreference>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserPreferenceMapping"/> class.
		/// </summary>
		public UserPreferenceMapping()
		{
			base.Table("UsersPreferences");

			base.Id(x => x.Id)
				.Column("Id")
				.GeneratedBy.Native();

			base.Map(x => x.Dst)
				.Column("Dst")
				.Not.Nullable();

			base.Map(x => x.StartOfWeek)
				.Column("StartOfWeek")
				.Not.Nullable();

			base.Map(x => x.TimeFormat)
				.Column("TimeFormat")
				.Not.Nullable();

			base.References(x => x.Country)
				.Column("CountryId")
				.Not.Nullable();

			base.References(x => x.TimeZone)
				.Column("TimeZoneId")
				.Not.Nullable();
		}
	}
}