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
using System.Web;
using Coders.Extensions;
using Coders.Models.Settings;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
#endregion

namespace Coders.Web.Models.Users
{
	public class UserAvatarCreate : Value
	{
		/// <summary>
		/// Gets or sets the file.
		/// </summary>
		/// <value>The file.</value>
		public HttpPostedFileBase File
		{
			get;
			set;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new UserAvatarCreateValidatorCollection().Validate(this);
		}
	}

	public class UserAvatarCreateValidatorCollection : AbstractValidator<UserAvatarCreate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAvatarCreateValidatorCollection"/> class.
		/// </summary>
		public UserAvatarCreateValidatorCollection()
		{
			RuleFor(x => x.File)
				.NotNull()
				.WithMessage(Errors.Required.FormatInvariant(Titles.File));

			RuleFor(x => x.File)
				.Image();

			RuleFor(x => x.File)
				.ImageDimensionMax(
					Setting.UserAvatarMaxWidth.Value, 
					Setting.UserAvatarMaxHeight.Value
				);
		}
	}
}