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
using System.Collections.Generic;
using Coders.Extensions;
using Coders.Models.Users;
using Coders.Strings;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserHostSearchCreateValidatorCollection))]
	public class UserHostSearchCreate : Value<UserHostSearch>
	{
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
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the host address.
		/// </summary>
		/// <value>The host address.</value>
		public string HostAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="UserHostSearchCreate"/> is save.
		/// </summary>
		/// <value><c>true</c> if save; otherwise, <c>false</c>.</value>
		public bool Save
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the searches.
		/// </summary>
		/// <value>The searches.</value>
		public IList<UserHostSearch> Searches
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes the specified searches.
		/// </summary>
		/// <param name="searches">The searches.</param>
		public void Initialize(IList<UserHostSearch> searches)
		{
			this.Searches = searches;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(UserHostSearch entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Name = this.Name;
			entity.HostAddress = this.HostAddress;

			if (!this.Save)
			{
				return;
			}

			entity.Title = this.Title;
		}
	}

	public class UserHostSearchCreateValidatorCollection : AbstractValidator<UserHostSearchCreate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostSearchCreateValidatorCollection"/> class.
		/// </summary>
		public UserHostSearchCreateValidatorCollection()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.When(x => x.Save)
				.WithMessage(Errors.Required.FormatInvariant(Titles.Title));
		}
	}
}
