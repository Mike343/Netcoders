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

namespace Coders.Models.Users
{
	public interface IUserBanService : IEntityService<UserBan>
	{
		/// <summary>
		/// Checks if the current user is banned.
		/// </summary>
		/// <returns></returns>
		UserBan Check();

		/// <summary>
		/// Inserts or updates the specified user ban.
		/// </summary>
		/// <param name="ban">The ban.</param>
		void InsertOrUpdate(UserBan ban);

		/// <summary>
		/// Inserts or updates the specified user ban.
		/// </summary>
		/// <param name="ban">The ban.</param>
		/// <param name="name">The name.</param>
		void InsertOrUpdate(UserBan ban, string name);

		/// <summary>
		/// Deletes the expired bans.
		/// </summary>
		void DeleteExpired();
	}
}