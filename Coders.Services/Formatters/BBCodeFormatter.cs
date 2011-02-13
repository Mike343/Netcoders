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
using System.Text.RegularExpressions;
using Coders.Extensions;
#endregion

namespace Coders.Services.Formatters
{
	public class BBCodeFormatter : IFormatter
	{
		// fields
		private readonly List<IFormatter> _formatters;

		// constants
		private const string FormatList = "<ol class=\"list\" style=\"list-style:{0};\">$1</ol>";

		/// <summary>
		/// Initializes a new instance of the <see cref="BBCodeFormatter"/> class.
		/// </summary>
		public BBCodeFormatter()
		{
			_formatters = new List<IFormatter>();

			// regex
			_formatters.Add(new RegexFormatter(@"\[b(?:\s*)\]((.|\n)*?)\[/b(?:\s*)\]", "<strong>$1</strong>"));
			_formatters.Add(new RegexFormatter(@"\[bold(?:\s*)\]((.|\n)*?)\[/bold(?:\s*)\]", "<strong>$1</strong>"));
			_formatters.Add(new RegexFormatter(@"\[i(?:\s*)\]((.|\n)*?)\[/i(?:\s*)\]", "<span style=\"font-style: italic;\">$1</span>"));
			_formatters.Add(new RegexFormatter(@"\[italic(?:\s*)\]((.|\n)*?)\[/italic(?:\s*)\]", "<span style=\"font-style: italic;\">$1</span>"));
			_formatters.Add(new RegexFormatter(@"\[u(?:\s*)\]((.|\n)*?)\[/u(?:\s*)\]", "<span style=\"text-decoration: underline;\">$1</span>"));
			_formatters.Add(new RegexFormatter(@"\[underline(?:\s*)\]((.|\n)*?)\[/underline(?:\s*)\]", "<span style=\"text-decoration: underline;\">$1</span>"));
			_formatters.Add(new RegexFormatter(@"\[s(?:\s*)\]((.|\s)*?)\[/s(?:\s*)\]", "<span style=\"text-decoration: line-through;\">$1</span>"));
			_formatters.Add(new RegexFormatter(@"\[strike(?:\s*)\]((.|\s)*?)\[/strike(?:\s*)\]", "<span style=\"text-decoration: line-through;\">$1</span>"));
			_formatters.Add(new RegexFormatter(@"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "<span style=\"color:$1;\">$3</span>"));
			_formatters.Add(new RegexFormatter(@"\[size=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/size(?:\s*)\]", "<span style=\"font-size:$1;\">$3</span>"));
			_formatters.Add(new RegexFormatter(@"\[left(?:\s*)\]((.|\n)*?)\[/left(?:\s*)]", "<div style=\"text-align:left\">$1</div>"));
			_formatters.Add(new RegexFormatter(@"\[center(?:\s*)\]((.|\n)*?)\[/center(?:\s*)]", "<div style=\"text-align:center\">$1</div>"));
			_formatters.Add(new RegexFormatter(@"\[right(?:\s*)\]((.|\n)*?)\[/right(?:\s*)]", "<div style=\"text-align:right\">$1</div>"));
			_formatters.Add(new RegexFormatter(@"\[url(?:\s*)\]www\.(.*?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" title=\"$1\">$1</a>"));
			_formatters.Add(new RegexFormatter(@"\[url(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" title=\"$1\">$1</a>"));
			_formatters.Add(new RegexFormatter(@"\[url=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" title=\"$1\">$3</a>"));
			_formatters.Add(new RegexFormatter(@"\[email(?:\s*)\]((.|\n)*?)\[/email(?:\s*)\]", "<a href=\"mailto:$1\">$1</a>"));
			_formatters.Add(new RegexFormatter(@"\[img(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$1\" alt=\"image\" class=\"bbcode-image\" />"));
			_formatters.Add(new RegexFormatter(@"\[img align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$3\" align=\"$1\" alt=\"image\" class=\"bbcode-image\" />"));
			_formatters.Add(new RegexFormatter(@"\[img=((.|\n)*?),((.|\n)*?),((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img width=\"$1\" height=\"$3\" align=\"$5\" src=\"$7\" alt=\"image\" class=\"bbcode-image\" />"));
			_formatters.Add(new RegexFormatter(@"\[img=((.|\n)*?),((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img width=\"$1\" height=\"$3\" src=\"$5\" alt=\"image\" class=\"bbcode-image\" />"));
			_formatters.Add(new RegexFormatter(@"\[youtube(?:\s*)\]((.|\n)*?)\[/youtube(?:\s*)\]", "<object width=\"560\" height=\"340\"><param name=\"movie\" value=\"http://www.youtube.com/v/$1&hl=en&fs=1&\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed src=\"http://www.youtube.com/v/$1&hl=en&fs=1&\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" width=\"560\" height=\"340\"></embed></object>"));
			_formatters.Add(new RegexFormatter(@"\[flash(?:\s*)\]((.|\n)*?)\[/flash(?:\s*)\]", "<embed src=\"$1\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\"></embed>"));
			_formatters.Add(new RegexFormatter(@"\[flash=((.|\n)*?),((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/flash(?:\s*)\]", "<embed src=\"$5\" width=\"$1\" height=\"$3\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\"></embed>"));
			_formatters.Add(new RegexFormatter(@"\[\*(?:\s*)]\s*([^\[]*)", "<li>$1</li>"));
			_formatters.Add(new RegexFormatter(@"\[list(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", "<ul class=\"bbcode-list\">$1</ul>"));
			_formatters.Add(new RegexFormatter(@"\[list=1(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("decimal"), false));
			_formatters.Add(new RegexFormatter(@"\[list=i(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("lower-roman"), false));
			_formatters.Add(new RegexFormatter(@"\[list=I(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("upper-roman"), false));
			_formatters.Add(new RegexFormatter(@"\[list=a(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("lower-alpha"), false));
			_formatters.Add(new RegexFormatter(@"\[list=A(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", FormatList.FormatInvariant("upper-alpha"), false));
			_formatters.Add(new RegexFormatter(@"\[quote user=((.|\n)*?)(?:\s*)\]", "<blockquote><div><div class=\"user\">$1 said:</div>"));
			_formatters.Add(new RegexFormatter(@"\[quote(\s*)\]", "<blockquote><div>"));
			_formatters.Add(new RegexFormatter(@"\[/quote(\s*)\]", "</div></blockquote>"));

			// replace
			_formatters.Add(new SearchReplaceFormatter("\r", ""));
			_formatters.Add(new SearchReplaceFormatter("\n\n", "<br /><br />"));
			_formatters.Add(new SearchReplaceFormatter("\n", "<br />"));
		}

		/// <summary>
		/// Parses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string Parse(string value)
		{
			return this.Parse(value, true);
		}

		/// <summary>
		/// Parses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="html">if set to <c>true</c> [HTML].</param>
		/// <returns></returns>
		public string Parse(string value, bool html)
		{
			value = _formatters.Aggregate(value, (current, formatter) => formatter.Parse(current));

			if (!html)
			{
				value = Regex.Replace(value, @"</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style)(.|\n)*?>", string.Empty, RegexOptions.IgnoreCase);
			}

			return value;
		}
	}
}