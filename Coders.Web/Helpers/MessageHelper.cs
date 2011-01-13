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
using System.Web.Mvc;
using System.Web.UI;
using Coders.Extensions;
using Coders.Strings;
using Coders.Web.ActionResults.Enums;
#endregion

namespace Coders.Web.Helpers
{
	public class MessageHelper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageHelper"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="message">The message.</param>
		/// <param name="scope">The scope.</param>
		public MessageHelper(ViewContext context, string message, MessageScope scope)
		{
			this.Context = context;
			this.Message = message;
			this.Scope = scope;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageHelper"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="scope">The scope.</param>
		/// <param name="messages">The messages.</param>
		public MessageHelper(ViewContext context, MessageScope scope, IList<string> messages)
		{
			this.Context = context;
			this.Scope = scope;
			this.Messages = messages;
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		/// <value>The context.</value>
		public ViewContext Context
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the scope.
		/// </summary>
		/// <value>The scope.</value>
		public MessageScope Scope
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the messages.
		/// </summary>
		/// <value>The messages.</value>
		public IList<string> Messages
		{
			get;
			private set;
		}

		/// <summary>
		/// Renders this instance.
		/// </summary>
		public void Render()
		{
			using (var html = new XhtmlTextWriter(this.Context.Writer))
			{
				// messages
				html.AddAttribute(HtmlTextWriterAttribute.Class, "messages");
				html.AddAttribute(HtmlTextWriterAttribute.Id, "message");
				html.RenderBeginTag(HtmlTextWriterTag.Div);

				switch (this.Scope)
				{
					case MessageScope.Error:
						WriteMessage(html, "error", Errors.GenericMessage);
						break;
					case MessageScope.Warning:
						WriteMessage(html, "warning", Titles.Warning);
						break;
					default:
						WriteMessage(html, "success", Titles.Success);
						break;
				}

				// end messages
				html.RenderEndTag();
			}
		}

		/// <summary>
		/// Writes the message.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <param name="scope">The scope.</param>
		/// <param name="heading">The heading.</param>
		private void WriteMessage(HtmlTextWriter html, string scope, string heading)
		{
			html.AddAttribute(HtmlTextWriterAttribute.Class, "message message-{0}".FormatInvariant(scope));
			html.RenderBeginTag(HtmlTextWriterTag.Div);

			// image
			html.AddAttribute(HtmlTextWriterAttribute.Class, "image");
			html.RenderBeginTag(HtmlTextWriterTag.Div);

			html.AddAttribute(HtmlTextWriterAttribute.Src, "/content/images/icons/{0}.png".FormatInvariant(scope));
			html.AddAttribute(HtmlTextWriterAttribute.Alt, Titles.Error);
			html.AddAttribute(HtmlTextWriterAttribute.Height, "32");
			html.RenderBeginTag(HtmlTextWriterTag.Img);
			html.RenderEndTag();

			// end image
			html.RenderEndTag();

			// text
			html.AddAttribute(HtmlTextWriterAttribute.Class, "text");
			html.RenderBeginTag(HtmlTextWriterTag.Div);

			// h6
			html.RenderBeginTag(HtmlTextWriterTag.H6);
			html.Write(heading);
			html.RenderEndTag();

			// ol
			html.RenderBeginTag(HtmlTextWriterTag.Ol);

			if (this.Messages != null && this.Messages.Count > 0)
			{
				foreach (var message in this.Messages)
				{
					html.RenderBeginTag(HtmlTextWriterTag.Li);
					html.Write(message);
					html.RenderEndTag();
				}
			}
			else
			{
				html.RenderBeginTag(HtmlTextWriterTag.Li);
				html.Write(this.Message);
				html.RenderEndTag();
			}

			// end ol
			html.RenderEndTag();

			// end text
			html.RenderEndTag();

			// dismiss
			html.AddAttribute(HtmlTextWriterAttribute.Class, "dismiss");
			html.RenderBeginTag(HtmlTextWriterTag.Div);

			// dismiss link
			html.AddAttribute(HtmlTextWriterAttribute.Href, "#message");
			html.RenderBeginTag(HtmlTextWriterTag.A);
			html.RenderEndTag();

			// end dismiss
			html.RenderEndTag();

			// end message
			html.RenderEndTag();
		}
	}
}