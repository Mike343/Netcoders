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
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Models.Users
{
	public class User : EntityBase
	{
		// public constants
		public const string Guest = "Guest";

		/// <summary>
		/// Gets or sets the reputation.
		/// </summary>
		/// <value>The reputation.</value>
		public virtual int Reputation
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public virtual string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the slug.
		/// </summary>
		/// <value>The slug.</value>
		public virtual string Slug
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public virtual string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public virtual string EmailAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the host address.
		/// </summary>
		/// <value>The host address.</value>
		public virtual string HostAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public virtual string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the salt.
		/// </summary>
		/// <value>The salt.</value>
		public virtual string Salt
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the signature.
		/// </summary>
		/// <value>The signature.</value>
		public virtual string Signature
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the signature parsed.
		/// </summary>
		/// <value>The signature parsed.</value>
		public virtual string SignatureParsed
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is protected.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is protected; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsProtected
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public virtual UserStatus Status
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>The created.</value>
		public virtual DateTime Created
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the updated.
		/// </summary>
		/// <value>The updated.</value>
		public virtual DateTime Updated
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last visit.
		/// </summary>
		/// <value>The last visit.</value>
		public virtual DateTime LastVisit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last log on.
		/// </summary>
		/// <value>The last log on.</value>
		public virtual DateTime LastLogOn
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the avatar.
		/// </summary>
		/// <value>The avatar.</value>
		public virtual UserAvatar Avatar
		{
			get; 
			set; 
		}

		/// <summary>
		/// Gets or sets the preference.
		/// </summary>
		/// <value>The preference.</value>
		public virtual UserPreference Preference
		{
			get;
			set;
		}

		/// <summary>
		/// Updates the reputation.
		/// </summary>
		public virtual void UpdateReputation()
		{
			UpdateReputation(1, true);
		}

		/// <summary>
		/// Updates the reputation.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="addition">if set to <c>true</c> [addition].</param>
		public virtual void UpdateReputation(int value, bool addition)
		{
			if (addition)
			{
				this.Reputation += value;
			}
			else
			{
				this.Reputation -= value;
			}
		}
	}
}