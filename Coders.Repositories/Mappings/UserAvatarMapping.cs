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
	public sealed class UserAvatarMapping : ClassMap<UserAvatar>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAvatarMapping"/> class.
		/// </summary>
		public UserAvatarMapping()
		{
			base.Table("UsersAvatars");

			base.Id(x => x.Id)
				.Column("Id")
				.GeneratedBy.Native();

			base.Map(x => x.UserId)
				.Column("UserId")
				.Not.Nullable();

			base.Map(x => x.FileName)
				.Column("FileName")
				.Not.Nullable();

			base.Map(x => x.FileDiskName)
				.Column("FileDiskName")
				.Not.Nullable();

			base.Map(x => x.FilePath)
				.Column("FilePath")
				.Not.Nullable();

			base.Map(x => x.FileType)
				.Column("FileType")
				.Not.Nullable();

			base.Map(x => x.Created)
				.Column("Created")
				.Not.Nullable();
		}
	}
}