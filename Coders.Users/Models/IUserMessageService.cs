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
using System.Collections.Generic;
using Coders.Models;
using Coders.Specifications;
#endregion

namespace Coders.Users.Models
{
	public interface IUserMessageService : IEntityService<UserMessage>
	{
		/// <summary>
		/// Gets the folder by specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		UserMessageFolder GetFolderBy(ISpecification<UserMessageFolder> specification);

		/// <summary>
		/// Gets the folder by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		UserMessageFolder GetFolderById(int id);

		/// <summary>
		/// Gets the folders by specified specification..
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		IList<UserMessageFolder> GetFolders(ISpecification<UserMessageFolder> specification);

		/// <summary>
		/// Marks the specified message as read.
		/// </summary>
		/// <param name="message">The message.</param>
		void MarkRead(UserMessage message);

		/// <summary>
		/// Inserts the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="name">The name.</param>
		void Insert(UserMessage message, string name);

		/// <summary>
		/// Inserts the folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		void InsertFolder(UserMessageFolder folder);

		/// <summary>
		/// Updates the folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		void UpdateFolder(UserMessageFolder folder);

		/// <summary>
		/// Deletes the folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		void DeleteFolder(UserMessageFolder folder);
	}
}