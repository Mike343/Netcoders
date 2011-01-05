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
using Coders.Models.Users;
#endregion

namespace Coders.Services
{
	public class UserAvatarService : EntityService<UserAvatar>, IUserAvatarService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAvatarService"/> class.
		/// </summary>
		/// <param name="userService">The user service.</param>
		/// <param name="repository">The repository.</param>
		public UserAvatarService(
			IUserService userService, 
			IUserAvatarRepository repository)
			: base(repository)
		{
			this.UserService = userService;
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
		/// Assigns the specified avatar.
		/// </summary>
		/// <param name="avatar">The avatar.</param>
		public void Assign(UserAvatar avatar)
		{
			if (avatar == null)
			{
				throw new ArgumentNullException("avatar");
			}

			// update user
			var user = this.UserService.GetById(avatar.UserId);

			user.Avatar = avatar;

			this.UserService.Update(user);
		}
	}
}