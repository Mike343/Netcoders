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

namespace Coders.Models.Logs
{
	public class Log : EntityBase
	{
		// public constants
		public const string Settings = "Settings";

		/// <summary>
		/// Gets or sets the group id.
		/// </summary>
		/// <value>The group id.</value>
		public virtual int GroupId
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the group key.
		/// </summary>
		/// <value>The group key.</value>
		public virtual string GroupKey
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		public virtual string Action
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value>
		public virtual string Data
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
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		public virtual User User
		{
			get;
			set;
		}
	}
}