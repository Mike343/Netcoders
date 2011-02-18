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
using Coders.Models;
using Coders.Models.Users;
#endregion

namespace Coders.Users.Models
{
	public class UserMessage : EntityBase
	{
		/// <summary>
		/// Gets or sets the parent id.
		/// </summary>
		/// <value>The parent id.</value>
		public int? ParentId
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
		/// Gets or sets the slug.
		/// </summary>
		/// <value>The slug.</value>
		public string Slug
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the body.
		/// </summary>
		/// <value>The body.</value>
		public string Body
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the body parsed.
		/// </summary>
		/// <value>The body parsed.</value>
		public string BodyParsed
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is sender active.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is sender active; otherwise, <c>false</c>.
		/// </value>
		public bool IsSenderActive
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is receiver active.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is receiver active; otherwise, <c>false</c>.
		/// </value>
		public bool IsReceiverActive
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has receiver read.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has receiver read; otherwise, <c>false</c>.
		/// </value>
		public bool HasReceiverRead
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has receiver replied.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has receiver replied; otherwise, <c>false</c>.
		/// </value>
		public bool HasReceiverReplied
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has receiver forwarded.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has receiver forwarded; otherwise, <c>false</c>.
		/// </value>
		public bool HasReceiverForwarded
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the read.
		/// </summary>
		/// <value>The read.</value>
		public DateTime? Read
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>The created.</value>
		public DateTime Created
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the updated.
		/// </summary>
		/// <value>The updated.</value>
		public DateTime Updated
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the folder.
		/// </summary>
		/// <value>The folder.</value>
		public UserMessageFolder Folder
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the sender.
		/// </summary>
		/// <value>The sender.</value>
		public IUser Sender
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the receiver.
		/// </summary>
		/// <value>The receiver.</value>
		public IUser Receiver
		{
			get;
			set;
		}
	}
}