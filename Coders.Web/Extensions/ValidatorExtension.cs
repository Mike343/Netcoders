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
using System.Linq.Expressions;
using System.Web;
using Coders.Web.Validators;
using FluentValidation;
#endregion

namespace Coders.Web.Extensions
{
	public static class ValidatorExtension
	{
		/// <summary>
		/// Validates that the characters in the string are valid.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> Character<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new CharacterValidator());
		}

		/// <summary>
		/// Validates that the date is valid in the format M/d/yyyy.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> Date<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new DateValidator());
		}

		/// <summary>
		/// Validates that the image dimensions are not greater than the specifed width and height.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, HttpPostedFileBase> ImageDimensionMax<T>(this IRuleBuilder<T, HttpPostedFileBase> builder, int width, int height)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new ImageDimensionMaxValidator(width, height));
		}

		/// <summary>
		/// Validates that the image dimensions are at least the specifed width and height.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, HttpPostedFileBase> ImageDimensionMin<T>(this IRuleBuilder<T, HttpPostedFileBase> builder, int width, int height)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new ImageDimensionMinValidator(width, height));
		}

		/// <summary>
		/// Validates that the file is an image.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, HttpPostedFileBase> Image<T>(this IRuleBuilder<T, HttpPostedFileBase> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new ImageValidator());
		}

		/// <summary>
		/// Validates that the email address is valid.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new EmailValidator());
		}

		/// <summary>
		/// Validates that the setting is unique.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> SettingUnique<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new SettingUniqueValidator());
		}

		/// <summary>
		/// Validators that the user is authorized.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> UserAuthorize<T>(this IRuleBuilder<T, string> builder, Expression<Func<T, string>> expression)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new UserAuthorizeValidator<T>(expression));
		}

		/// <summary>
		/// Validates that the user email address exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> UserEmailAddressMustExist<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new UserEmailAddressMustExistValidator());
		}

		/// <summary>
		/// Validates that the user name exists.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> UserNameMustExist<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new UserNameMustExistValidator());
		}

		/// <summary>
		/// Validates that the user name is not protected.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> UserNameNotProtected<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new UserNameNotProtectedValidator());
		}

		/// <summary>
		/// Validates that the user email address is unique.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> UserUniqueEmailAddress<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new UserUniqueEmailAddressValidator());
		}

		/// <summary>
		/// Validates that the user name is unique.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IRuleBuilderOptions<T, string> UserUniqueName<T>(this IRuleBuilder<T, string> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			return builder.SetValidator(new UserUniqueNameValidator());
		}
	}
}