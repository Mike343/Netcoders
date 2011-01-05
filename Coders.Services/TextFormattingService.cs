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
using Coders.Models.Common;
using Coders.Services.Formatters;
#endregion

namespace Coders.Services
{
	public class TextFormattingService : ITextFormattingService
	{
		/// <summary>
		/// Parses the formatting tags in the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string Parse(string value)
		{
			return this.Parse(value, false);
		}

		/// <summary>
		/// Parses the formatting tags in the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="html">if set to <c>true</c> html is allowed.</param>
		/// <returns></returns>
		public string Parse(string value, bool html)
		{
			return string.IsNullOrEmpty(value) ? null : new BBCodeFormatter().Parse(value, html);
		}

		/// <summary>
		/// Strips the specified value of the formatting tags.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string Strip(string value)
		{
			return BBCodeFormatter.Strip(value);
		}
	}
}