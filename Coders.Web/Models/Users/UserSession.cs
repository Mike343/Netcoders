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
using Coders.Models.Users;
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserSession
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserSession"/> class.
		/// </summary>
		public UserSession()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserSession"/> class.
		/// </summary>
		/// <param name="privileges">The privileges.</param>
		public UserSession(IList<UserRoleRelation> privileges)
		{
			this.Privileges = privileges;
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
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the slug.
		/// </summary>
		/// <value>The slug.</value>
		public string Slug
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the time zone.
		/// </summary>
		/// <value>The time zone.</value>
		public string TimeZone
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the DST.
		/// </summary>
		/// <value>The DST.</value>
		public UserPreferenceDaylightSavingTime Dst
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the time format.
		/// </summary>
		/// <value>The time format.</value>
		public UserPreferenceTimeFormat TimeFormat
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the privileges.
		/// </summary>
		/// <value>The privileges.</value>
		public IList<UserRoleRelation> Privileges
		{
			get; 
			private set;
		}
	}
}