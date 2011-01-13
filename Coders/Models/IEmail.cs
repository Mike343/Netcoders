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

namespace Coders.Models
{
	public interface IEmail
	{
		/// <summary>
		/// Gets the template.
		/// </summary>
		/// <value>The template.</value>
		string Template { get; }

		/// <summary>
		/// Gets the recipient.
		/// </summary>
		/// <value>The recipient.</value>
		string Recipient { get; }

		/// <summary>
		/// Gets from.
		/// </summary>
		/// <value>From.</value>
		string From { get; }

		/// <summary>
		/// Gets the subject.
		/// </summary>
		/// <value>The subject.</value>
		string Subject { get; }
	}
}