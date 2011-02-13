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
using System.Web;
#endregion

namespace Coders.Models.Users
{
	public interface IUserAvatarService : IEntityService<UserAvatar>
	{
		/// <summary>
		/// Assigns the specified avatar to the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="avatar">The avatar.</param>
		void AssignToUser(User user, UserAvatar avatar);

		/// <summary>
		/// Removes the specified avatar from the specified avatar if they match.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="avatar">The avatar.</param>
		bool RemoveFromUserOnMatch(User user, UserAvatar avatar);

		/// <summary>
		/// Inserts the specified avatar.
		/// </summary>
		/// <param name="avatar">The avatar.</param>
		/// <param name="file">The file.</param>
		void Insert(UserAvatar avatar, HttpPostedFileBase file);
	}
}