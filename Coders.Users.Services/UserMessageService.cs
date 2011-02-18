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
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Services;
using Coders.Specifications;
using Coders.Users.Models;
#endregion

namespace Coders.Users.Services
{
	public class UserMessageService : EntityService<UserMessage>, IUserMessageService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserMessageService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="textFormattingService">The text formatting service.</param>
		/// <param name="userService">The user service.</param>
		/// <param name="userMessageRepository">The user message repository.</param>
		/// <param name="userMessageFolderRepository">The user message folder repository.</param>
		public UserMessageService(
			IAuthenticationService authenticationService,
			ITextFormattingService textFormattingService,
			IUserService userService,
			IRepository<UserMessage> userMessageRepository,
			IRepository<UserMessageFolder> userMessageFolderRepository)
			: base(userMessageRepository)
		{
			this.AuthenticationService = authenticationService;
			this.TextFormattingService = textFormattingService;
			this.UserService = userService;
			this.UserMessageFolderRepository = userMessageFolderRepository;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets or sets the text formatting service.
		/// </summary>
		/// <value>The text formatting service.</value>
		public ITextFormattingService TextFormattingService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user service.
		/// </summary>
		/// <value>The user service.</value>
		public IUserService UserService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user message folder repository.
		/// </summary>
		/// <value>The user message folder repository.</value>
		public IRepository<UserMessageFolder> UserMessageFolderRepository
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the folder by specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public UserMessageFolder GetFolderBy(ISpecification<UserMessageFolder> specification)
		{
			return this.UserMessageFolderRepository.GetBy(specification);
		}

		/// <summary>
		/// Gets the folder by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public UserMessageFolder GetFolderById(int id)
		{
			return this.UserMessageFolderRepository.GetById(id);
		}

		/// <summary>
		/// Gets the folders by specified specification..
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public IList<UserMessageFolder> GetFolders(ISpecification<UserMessageFolder> specification)
		{
			return this.UserMessageFolderRepository.GetAll(specification);
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns></returns>
		public override UserMessage Create()
		{
			var message = base.Create();

			message.Created = DateTime.Now;
			message.Updated = message.Created;

			return message;
		}

		/// <summary>
		/// Marks the specified message as read.
		/// </summary>
		/// <param name="message">The message.</param>
		public void MarkRead(UserMessage message)
		{
			if (message.HasReceiverRead)
			{
				return;
			}

			message.HasReceiverRead = true;
			message.Read = DateTime.Now;

			this.Update(message);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(UserMessage entity)
		{
			var identity = this.AuthenticationService.Identity;

			if (!identity.IsAuthenticated())
			{
				return;
			}

			entity.Slug = entity.Title.Slug();
			entity.BodyParsed = this.TextFormattingService.Parse(entity.Body, false);
			entity.Sender = this.UserService.GetById(identity.Id);

			base.Insert(entity);
		}

		/// <summary>
		/// Inserts the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="name">The name.</param>
		public void Insert(UserMessage message, string name)
		{
			var names = name.Split(',');

			foreach (var value in names)
			{
				message.Receiver = this.UserService.GetBy(new UserNameSpecification(value));

				this.Insert(message);
			}
		}

		/// <summary>
		/// Inserts the specified folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		public void InsertFolder(UserMessageFolder folder)
		{
			var identity = this.AuthenticationService.Identity;

			if (!identity.IsAuthenticated())
			{
				return;
			}

			folder.UserId = identity.Id;
			folder.Slug = folder.Title.Slug();

			this.UserMessageFolderRepository.Insert(folder);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(UserMessage entity)
		{
			entity.Updated = DateTime.Now;

			base.Update(entity);
		}

		/// <summary>
		/// Updates the specified folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		public void UpdateFolder(UserMessageFolder folder)
		{
			folder.Slug = folder.Title.Slug();

			this.UserMessageFolderRepository.Update(folder);
		}

		/// <summary>
		/// Deletes the specified folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		public void DeleteFolder(UserMessageFolder folder)
		{
			this.UserMessageFolderRepository.Delete(folder);
		}
	}
}