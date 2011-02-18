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

using Coders.Repositories.Strategies.Joins;
using Coders.Users.Models;
using FluentNHibernate.Mapping;
#endregion

namespace Coders.Users.Repositories.Mappings
{
	public class UserMessageMapping : ClassMap<UserMessage>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserMessageMapping"/> class.
		/// </summary>
		public UserMessageMapping()
		{
			base.Table("UsersFolders");

			base.Id(x => x.Id)
				.Column("Id")
				.GeneratedBy.Native();

			base.Map(x => x.ParentId)
				.Column("ParentId")
				.Nullable();

			base.Map(x => x.Title)
				.Column("Title")
				.Not.Nullable();

			base.Map(x => x.Slug)
				.Column("Slug")
				.Not.Nullable();

			base.Map(x => x.Body)
				.Column("Body")
				.Not.Nullable();

			base.Map(x => x.BodyParsed)
				.Column("BodyParsed")
				.Not.Nullable();

			base.Map(x => x.IsSenderActive)
				.Column("SenderActive")
				.Not.Nullable();

			base.Map(x => x.IsReceiverActive)
				.Column("ReceiverActive")
				.Not.Nullable();

			base.Map(x => x.HasReceiverRead)
				.Column("ReceiverRead")
				.Not.Nullable();

			base.Map(x => x.HasReceiverReplied)
				.Column("ReceiverReplied")
				.Not.Nullable();

			base.Map(x => x.HasReceiverForwarded)
				.Column("ReceiverForwarded")
				.Not.Nullable();

			base.Map(x => x.Read)
				.Column("Read")
				.Nullable();

			base.Map(x => x.Created)
				.Column("Created")
				.Not.Nullable();

			base.Map(x => x.Updated)
				.Column("Updated")
				.Not.Nullable();

			base.References(x => x.Folder)
				.Column("FolderId")
				.Not.Nullable();

			base.References<UserMinimum>(x => x.Sender)
				.Column("SenderId")
				.Not.Nullable();

			base.References<UserMinimum>(x => x.Receiver)
				.Column("ReceiverId")
				.Not.Nullable();
		}
	}
}