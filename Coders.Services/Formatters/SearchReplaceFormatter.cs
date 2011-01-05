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

namespace Coders.Services.Formatters
{
	public class SearchReplaceFormatter : IFormatter
	{
		// fields
		private readonly string _pattern;
		private readonly string _replace;

		/// <summary>
		/// Initializes a new instance of the <see cref="SearchReplaceFormatter"/> class.
		/// </summary>
		/// <param name="pattern">The pattern.</param>
		/// <param name="replace">The replace.</param>
		public SearchReplaceFormatter(string pattern, string replace)
		{
			_pattern = pattern;
			_replace = replace;
		}

		/// <summary>
		/// Parses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			return value.Replace(_pattern, _replace);
		}
	}
}