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
using System.Text.RegularExpressions;
using Coders.Strings;
using FluentValidation.Validators;
#endregion

namespace Coders.Web.Validators
{
	public class EmailValidator : PropertyValidator
	{
		// private constants
		private const string Pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailValidator"/> class.
		/// </summary>
		public EmailValidator()
			: base(Errors.EmailNotValid)
		{

		}

		/// <summary>
		/// Determines whether the specified context is valid.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>
		/// 	<c>true</c> if the specified context is valid; otherwise, <c>false</c>.
		/// </returns>
		protected override bool IsValid(PropertyValidatorContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			var value = context.PropertyValue as string;

			if (string.IsNullOrEmpty(value))
			{
				return false;
			}

			var regex = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

			return regex.IsMatch(value);
		}
	}
}