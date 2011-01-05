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

namespace Coders.Models.Common
{
	public interface IEmailService
	{
		/// <summary>
		/// Sends the email using the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		void Send<T>(T value) where T : class, IEmail;

		/// <summary>
		/// Sends the specified email
		/// </summary>
		/// <param name="recipient">The recipient.</param>
		/// <param name="from">The sender.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		void Send(string recipient, string from, string subject, string body);
	}
}