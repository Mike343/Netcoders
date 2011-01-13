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

namespace Coders.Authentication
{
	public class EmailAddressPasswordToken : IAuthenticationToken
	{
		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public string EmailAddress
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the type of the authentication.
		/// </summary>
		/// <value>The type of the authentication.</value>
		public string AuthenticationType
		{
			get
			{
				return "Email Address Password Challenge";
			}
		}
	}
}