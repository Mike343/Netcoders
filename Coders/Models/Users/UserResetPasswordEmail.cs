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
using Coders.Extensions;
using Coders.Models.Settings;
using Coders.Strings;
#endregion

namespace Coders.Models.Users
{
	public class UserResetPasswordEmail : EmailBase
	{
		/// <summary>
		/// Gets or sets the template.
		/// </summary>
		/// <value>The template.</value>
		public override string Template
		{
			get
			{
				return Setting.TemplateEmailUserResetPassword.Value;
			}
		}

		/// <summary>
		/// Gets the subject.
		/// </summary>
		/// <value>The subject.</value>
		public override string Subject
		{
			get
			{
				return Emails.SubjectPasswordReset.FormatInvariant(Setting.SiteTitle.Value);
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public string EmailAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns></returns>
		public override object Build()
		{
			return new
			{
				name = this.Name,
				emailAddress = this.EmailAddress,
				password = this.Password,
				siteTitle = SiteTitle,
				siteUrl = SiteUrl,
			};
		}
	}
}