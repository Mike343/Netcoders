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
using Coders.Web.ActionResults.Enums;
#endregion

namespace Coders.Web.ViewModels
{
	public class MessageViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageViewModel"/> class.
		/// </summary>
		public MessageViewModel()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageViewModel"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="scope">The scope.</param>
		public MessageViewModel(string message, MessageScope scope)
		{
			this.Message = message;
			this.Scope = scope;
		}

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		public MessageScope Scope
		{
			get;
			private set;
		}
	}
}