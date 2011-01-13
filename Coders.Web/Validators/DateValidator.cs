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
using System.Globalization;
using Coders.Strings;
using FluentValidation.Validators;
#endregion

namespace Coders.Web.Validators
{
	public class DateValidator : PropertyValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateValidator"/> class.
		/// </summary>
		public DateValidator()
			: base(Errors.DateNotValid)
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

			var date = context.PropertyValue as string;

			DateTime result;

			return DateTime.TryParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
		}
	}
}