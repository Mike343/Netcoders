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
#endregion

namespace Coders.Models.Common
{
	[Serializable]
	public class EmailResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmailResult"/> class.
		/// </summary>
		public EmailResult()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailResult"/> class.
		/// </summary>
		/// <param name="recipient">The recipient.</param>
		/// <param name="from">From.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		public EmailResult(string recipient, string from, string subject, string body)
		{
			this.Recipient = recipient;
			this.From = from;
			this.Subject = subject;
			this.Body = body;
		}

		/// <summary>
		/// Gets or sets the recipient.
		/// </summary>
		/// <value>The recipient.</value>
		public string Recipient
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets from.
		/// </summary>
		/// <value>From.</value>
		public string From
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		/// <value>The subject.</value>
		public string Subject
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the body.
		/// </summary>
		/// <value>The body.</value>
		public string Body
		{
			get;
			set;
		}
	}
}