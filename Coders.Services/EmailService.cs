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
using System.Messaging;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Settings;

#endregion

namespace Coders.Services
{
	public class EmailService : IEmailService
	{
		// private constants
		private const string QueueName = @".\private$\{0}-mail";

		/// <summary>
		/// Sends the email using the specified email.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="email">The email.</param>
		public void Send<T>(T email) where T : class, IEmail
		{
			if (email == null)
			{
				return;
			}

			Send(email.Template, email.Recipient, email.From, email.Subject, email.Build());
		}

		/// <summary>
		/// Send the specified email
		/// </summary>
		/// <param name="recipient">The recipient.</param>
		/// <param name="from">The sender.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		public void Send(string recipient, string from, string subject, string body)
		{
			Process(recipient, from, subject, body);
		}

		/// <summary>
		/// Send the specified email
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="recipient">The recipient.</param>
		/// <param name="from">The sender.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="model">The model.</param>
		public void Send(string template, string recipient, string from, string subject, object model)
		{
			Process(recipient, from, subject, null);
		}

		/// <summary>
		/// Processes the specified email.
		/// </summary>
		/// <param name="recipient">The recipient.</param>
		/// <param name="from">From.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		private static void Process(string recipient, string from, string subject, string body)
		{
			try
			{
				var name = QueueName.FormatInvariant(Setting.SiteTitle.Value.Slug());

				if (!MessageQueue.Exists(name))
				{
					using (var queue = MessageQueue.Create(name))
					{
						Send(queue, new EmailResult(recipient, from, subject, body));
					}
				}

				using (var queue = new MessageQueue(name))
				{
					Send(queue, new EmailResult(recipient, from, subject, body));
				}
			}
			catch (MessageQueueException exception)
			{
				throw new InvalidOperationException("The message queue has failed to send the message", exception);
			}
		}

		/// <summary>
		/// Sends the specified result to the specified queue.
		/// </summary>
		/// <param name="queue">The queue.</param>
		/// <param name="result">The result.</param>
		private static void Send(MessageQueue queue, EmailResult result)
		{
			queue.Send(result);
		}
	}
}