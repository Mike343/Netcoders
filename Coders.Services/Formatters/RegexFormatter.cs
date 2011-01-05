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
using System.Text.RegularExpressions;
#endregion

namespace Coders.Services.Formatters
{
	public class RegexFormatter : IFormatter
	{
		// fields
		private readonly Regex _regex;
		private readonly string _replace;

		/// <summary>
		/// Initializes a new instance of the <see cref="RegexFormatter"/> class.
		/// </summary>
		/// <param name="pattern">The pattern.</param>
		/// <param name="replace">The replace.</param>
		public RegexFormatter(string pattern, string replace) : this(pattern, replace, true)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegexFormatter"/> class.
		/// </summary>
		/// <param name="pattern">The pattern.</param>
		/// <param name="replace">The replace.</param>
		/// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
		public RegexFormatter(string pattern, string replace, bool ignoreCase)
		{
			var options = RegexOptions.Compiled;

			if (ignoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			_replace = replace;
			_regex = new Regex(pattern, options);
		}

		/// <summary>
		/// Parses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string Parse(string value)
		{
			return _regex.Replace(value, _replace);
		}
	}
}