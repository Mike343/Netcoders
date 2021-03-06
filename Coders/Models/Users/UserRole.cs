﻿#region License
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
using Coders.Authentication;
#endregion

namespace Coders.Models.Users
{
	public class UserRole : EntityBase
	{
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
		/// Gets or sets the slug.
		/// </summary>
		/// <value>The slug.</value>
		public virtual string Slug
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is default.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsDefault
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is administrator.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is administrator; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsAdministrator
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the privilege.
		/// </summary>
		/// <value>The privilege.</value>
		public virtual Privileges Privilege
		{
			get; 
			set;
		}
	}
}