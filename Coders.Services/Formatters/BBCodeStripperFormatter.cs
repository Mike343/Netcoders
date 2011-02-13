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
using System.Collections.Generic;
using System.Linq;
using Coders.Extensions;
#endregion

namespace Coders.Services.Formatters
{
	public class BBCodeStripperFormatter : IFormatter
	{
		// fields
		private readonly List<IFormatter> _formatters;

		// constants
		private const string FormatList = "$1";

		/// <summary>
		/// Initializes a new instance of the <see cref="BBCodeFormatter"/> class.
		/// </summary>
		public BBCodeStripperFormatter()
		{
			_formatters = new List<IFormatter>();

			// regex
			_formatters.Add(new RegexFormatter(@"\[b(?:\s*)\]((.|\n)*?)\[/b(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[bold(?:\s*)\]((.|\n)*?)\[/bold(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[i(?:\s*)\]((.|\n)*?)\[/i(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[italic(?:\s*)\]((.|\n)*?)\[/italic(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[u(?:\s*)\]((.|\n)*?)\[/u(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[underline(?:\s*)\]((.|\n)*?)\[/underline(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[s(?:\s*)\]((.|\s)*?)\[/s(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[strike(?:\s*)\]((.|\s)*?)\[/strike(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "$3"));
			_formatters.Add(new RegexFormatter(@"\[size=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/size(?:\s*)\]", "$3"));
			_formatters.Add(new RegexFormatter(@"\[left(?:\s*)\]((.|\n)*?)\[/left(?:\s*)]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[center(?:\s*)\]((.|\n)*?)\[/center(?:\s*)]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[right(?:\s*)\]((.|\n)*?)\[/right(?:\s*)]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[url(?:\s*)\]www\.(.*?)\[/url(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[url(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[url=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "$3"));
			_formatters.Add(new RegexFormatter(@"\[email(?:\s*)\]((.|\n)*?)\[/email(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[img(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[img align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "$3"));
			_formatters.Add(new RegexFormatter(@"\[img=((.|\n)*?),((.|\n)*?),((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "$7"));
			_formatters.Add(new RegexFormatter(@"\[img=((.|\n)*?),((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "$5"));
			_formatters.Add(new RegexFormatter(@"\[youtube(?:\s*)\]((.|\n)*?)\[/youtube(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[flash(?:\s*)\]((.|\n)*?)\[/flash(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[flash=((.|\n)*?),((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/flash(?:\s*)\]", "$5"));
			_formatters.Add(new RegexFormatter(@"\[\*(?:\s*)]\s*([^\[]*)", "$1"));
			_formatters.Add(new RegexFormatter(@"\[list(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", "$1"));
			_formatters.Add(new RegexFormatter(@"\[list=1(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("decimal"), false));
			_formatters.Add(new RegexFormatter(@"\[list=i(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("lower-roman"), false));
			_formatters.Add(new RegexFormatter(@"\[list=I(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("upper-roman"), false));
			_formatters.Add(new RegexFormatter(@"\[list=a(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("lower-alpha"), false));
			_formatters.Add(new RegexFormatter(@"\[list=A(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("upper-alpha"), false));
			_formatters.Add(new RegexFormatter(@"\[quote user=((.|\n)*?)(?:\s*)\]", ""));
			_formatters.Add(new RegexFormatter(@"\[quote(\s*)\]", ""));
			_formatters.Add(new RegexFormatter(@"\[/quote(\s*)\]", ""));

			// replace
			_formatters.Add(new SearchReplaceFormatter("\r", ""));
			_formatters.Add(new SearchReplaceFormatter("\n\n", "\n\n"));
			_formatters.Add(new SearchReplaceFormatter("\n", "\n"));
		}

		/// <summary>
		/// Parses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string Parse(string value)
		{
			return _formatters.Aggregate(value, (current, formatter) => formatter.Parse(current));
		}
	}
}