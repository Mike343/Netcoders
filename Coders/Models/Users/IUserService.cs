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
using Coders.Specifications;
#endregion

namespace Coders.Models.Users
{
	public interface IUserService : IEntityService<User>
	{
		/// <summary>
		/// Gets the user preference by specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		UserPreference GetPreferenceBy(ISpecification<UserPreference> specification);

		/// <summary>
		/// Inserts the specified user and the specified user preference.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="preference">The preference.</param>
		void Insert(User user, UserPreference preference);

		/// <summary>
		/// Inserts the preference.
		/// </summary>
		/// <param name="preference">The preference.</param>
		void InsertPreference(UserPreference preference);

		/// <summary>
		/// Updates the preference.
		/// </summary>
		/// <param name="preference">The preference.</param>
		void UpdatePreference(UserPreference preference);
	}
}