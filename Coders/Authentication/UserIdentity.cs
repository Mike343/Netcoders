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
using System.Security.Principal;
#endregion

namespace Coders.Authentication
{
	public abstract class UserIdentity : IIdentity
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets the name of the current user.
		/// </summary>
		/// <value></value>
		/// <returns>The name of the user on whose behalf the code is running.</returns>
		public string Name
		{
			get; 
			protected set;
		}

		/// <summary>
		/// Gets the type of authentication used.
		/// </summary>
		/// <value></value>
		/// <returns>The type of authentication used to identify the user.</returns>
		public string AuthenticationType
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets a value that indicates whether the user has been authenticated.
		/// </summary>
		/// <value></value>
		/// <returns>true if the user was authenticated; otherwise, false.</returns>
		public bool IsAuthenticated
		{
			get;
			protected set;
		}

		/// <summary>
		/// Authenticates the specified token.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <returns></returns>
		public abstract bool Authenticate(IAuthenticationToken token);
	}
}