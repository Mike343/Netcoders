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
using Coders.Authentication;
using Coders.Models.Users;
#endregion

namespace Coders.Models.Common
{
	public interface IAuthenticationService
	{
		/// <summary>
		/// Gets the return URL.
		/// </summary>
		/// <value>The return URL.</value>
		Uri ReturnUrl { get; }

		/// <summary>
		/// Gets the log on URL.
		/// </summary>
		/// <value>The log on URL.</value>
		Uri LogOnUrl { get; }

		/// <summary>
		/// Generates the password.
		/// </summary>
		/// <returns></returns>
		string GeneratePassword();

		/// <summary>
		/// Generates the password using the specified length.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		string GeneratePassword(int length);

		/// <summary>
		/// Authenticates the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		bool Authenticate(User user, string password);

		/// <summary>
		/// Gets the identity.
		/// </summary>
		/// <value>The identity.</value>
		UserIdentity Identity { get; }

		/// <summary>
		/// Logs the specified user on.
		/// </summary>
		/// <param name="user">The user.</param>
		void LogOn(User user);

		/// <summary>
		/// Logs the current user off.
		/// </summary>
		void LogOff();

		/// <summary>
		/// Resets the password for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		void Reset(User user);

		/// <summary>
		/// Updates the password for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		void Update(User user, string password);
	}
}