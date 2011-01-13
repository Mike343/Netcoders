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
	public sealed class UserSearchMapping : ClassMap<UserSearch>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserSearchMapping"/> class.
		/// </summary>
		public UserSearchMapping()
		{
			base.Table("UsersSearches");

			base.Id(x => x.Id)
				.Column("Id")
				.GeneratedBy.Native();

			base.Map(x => x.UserId)
				.Column("UserId")
				.Nullable();

			base.Map(x => x.Reputation)
				.Nullable();

			base.Map(x => x.Title)
				.Column("Title")
				.Nullable();

			base.Map(x => x.Name)
				.Column("Name")
				.Nullable();

			base.Map(x => x.EmailAddress)
				.Column("EmailAddress")
				.Nullable();

			base.Map(x => x.NameExact)
				.Column("NameExact")
				.Not.Nullable();

			base.Map(x => x.ReputationAtLeast)
				.Column("ReputationAtLeast")
				.Not.Nullable();

			base.Map(x => x.CreatedBefore)
				.Column("CreatedBefore")
				.Nullable();

			base.Map(x => x.CreatedAfter)
				.Column("CreatedAfter")
				.Nullable();

			base.Map(x => x.LastVisitBefore)
				.Column("LastVisitBefore")
				.Nullable();

			base.Map(x => x.LastVisitAfter)
				.Column("LastVisitAfter")
				.Nullable();

			base.Map(x => x.LastLogOnBefore)
				.Column("LastLogOnBefore")
				.Nullable();

			base.Map(x => x.LastLogOnAfter)
				.Column("LastLogOnAfter")
				.Nullable();

			base.Map(x => x.Created)
				.Column("Created")
				.Not.Nullable();

			base.Map(x => x.Updated)
				.Column("Updated")
				.Not.Nullable();
		}
	}
}