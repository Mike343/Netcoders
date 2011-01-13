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
	public sealed class UserMapping : ClassMap<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserMapping"/> class.
		/// </summary>
		public UserMapping()
		{
			base.Table("Users");

			base.Id(x => x.Id)
				.Column("Id")
				.GeneratedBy.Native();

			base.Map(x => x.Reputation)
				.Column("Reputation")
				.Not.Nullable();

			base.Map(x => x.Name)
				.Column("Name")
				.Length(255)
				.Not.Nullable();

			base.Map(x => x.Slug)
				.Column("Slug")
				.Length(255)
				.Not.Nullable();

			base.Map(x => x.Title)
				.Column("Title")
				.Length(255)
				.Nullable();

			base.Map(x => x.EmailAddress)
				.Column("EmailAddress")
				.Length(255)
				.Not.Nullable();

			base.Map(x => x.HostAddress)
				.Column("HostAddress")
				.Length(255)
				.Not.Nullable();

			base.Map(x => x.Password)
				.Column("Password")
				.Length(255)
				.Not.Nullable();

			base.Map(x => x.Salt)
				.Column("Salt")
				.Length(255)
				.Not.Nullable();

			base.Map(x => x.Signature)
				.Column("Signature")
				.Nullable();

			base.Map(x => x.SignatureParsed)
				.Column("SignatureParsed")
				.Nullable();

			base.Map(x => x.IsProtected)
				.Column("Protected")
				.Not.Nullable();

			base.Map(x => x.Status)
				.Column("Status")
				.Not.Nullable();

			base.Map(x => x.Created)
				.Column("Created")
				.Not.Nullable();

			base.Map(x => x.Updated)
				.Column("Updated")
				.Not.Nullable();

			base.Map(x => x.LastVisit)
				.Column("LastVisit")
				.Not.Nullable();

			base.Map(x => x.LastLogOn)
				.Column("LastLogOn")
				.Not.Nullable();

			base.References(x => x.Avatar)
				.Column("AvatarId")
				.Nullable();

			base.References(x => x.Preference)
				.Column("PreferenceId")
				.Not.Nullable();
		}
	}
}