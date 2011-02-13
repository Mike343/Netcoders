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
using Coders.Extensions;
using Coders.Models.Countries;
using Coders.Strings;
using FluentValidation;
#endregion

namespace Coders.Web.Models.Countries
{
	public class CountryCreateOrUpdate : Value<Country>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryCreateOrUpdate"/> class.
		/// </summary>
		public CountryCreateOrUpdate()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CountryCreateOrUpdate"/> class.
		/// </summary>
		/// <param name="country">The country.</param>
		public CountryCreateOrUpdate(Country country)
		{
			if (country == null)
			{
				throw new ArgumentNullException("country");
			}

			this.Id = country.Id;
			this.Title = country.Title;
			this.Code = country.Code;
		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>The code.</value>
		public string Code
		{
			get;
			set;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(Country entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Title = this.Title;
			entity.Code = this.Code;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new CountryCreateOrUpdateValidatorCollection().Validate(this);
		}
	}

	public class CountryCreateOrUpdateValidatorCollection : AbstractValidator<CountryCreateOrUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryCreateOrUpdateValidatorCollection"/> class.
		/// </summary>
		public CountryCreateOrUpdateValidatorCollection()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Title));

			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Code));
		}
	}
}