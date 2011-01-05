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
using System.Messaging;
using System.Net.Mail;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
#endregion

namespace Coders.MailerProcessor
{
	public static class Program
	{
		// private constants
		private const string QueueName = @".\private$\{0}-mail";

		/// <summary>
		/// Executes the application.
		/// </summary>
		static void Main()
		{
			// configures the application
			Application.Configure();

			// initialize the application
			Application.Initialize();

			var name = QueueName.FormatInvariant(Setting.SiteTitle.Value.Slug());

			using (var queue = new MessageQueue(name))
			{
				queue.Formatter = new XmlMessageFormatter(new[] { typeof(EmailResult) });

				var messages = queue.GetAllMessages();

				foreach (var message in messages)
				{
					if (message == null)
					{
						return;
					}

					var result = message.Body as EmailResult;

					if (result == null)
					{
						return;
					}

					using (var mail = new MailMessage())
					{
						mail.To.Add(new MailAddress(result.Recipient));
						mail.From = new MailAddress(result.From);

						mail.Subject = result.Subject;
						mail.Body = result.Body;
						mail.IsBodyHtml = false;

						using (var client = new SmtpClient())
						{
							client.Send(mail);
						}
					}
				}
			}
		}
	}
}