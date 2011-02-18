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
using System.Web;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Strings;
using FluentValidation.Resources;
using FluentValidation.Validators;
using Microsoft.Practices.ServiceLocation;
#endregion

namespace Coders.Web.Validators
{
	public class ImageValidator : PropertyValidator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageValidator"/> class.
		/// </summary>
		public ImageValidator()
			: this(ServiceLocator.Current.GetInstance<IImageService>())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageValidator"/> class.
		/// </summary>
		/// <param name="imageService">The image service.</param>
		public ImageValidator(IImageService imageService)
			: base(Errors.ImageContentTypeNotValid)
		{
			this.ImageService = imageService;
		}

		/// <summary>
		/// Gets or sets the image service.
		/// </summary>
		/// <value>The image service.</value>
		public IImageService ImageService
		{
			get; 
			set; 
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

			var file = context.PropertyValue as HttpPostedFileBase;

			if (file == null)
			{
				return true;
			}

			base.ErrorMessageSource = new StaticStringSource(
				Errors.ImageContentTypeNotValid.FormatInvariant(
					file.ContentType, 
					Setting.ImageExtension.Value
				)
			);

			return this.ImageService.IsImage(file);
		}
	}
}