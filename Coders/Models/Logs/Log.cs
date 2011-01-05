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
		/// Gets or sets the reason.
		/// </summary>
		/// <value>The reason.</value>
		public virtual string Reason
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