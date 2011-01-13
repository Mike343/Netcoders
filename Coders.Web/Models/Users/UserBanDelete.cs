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

namespace Coders.Web.Models.Users
{
	public class UserBanDelete
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanDelete"/> class.
		/// </summary>
		public UserBanDelete()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanDelete"/> class.
		/// </summary>
		/// <param name="ban">The ban.</param>
		public UserBanDelete(UserBan ban)
		{
			if (ban == null)
			{
				throw new ArgumentNullException("ban");
			}

			this.Id = ban.Id;
			this.User = ban.User;
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		public User User
		{
			get;
			set;
		}
	}
}