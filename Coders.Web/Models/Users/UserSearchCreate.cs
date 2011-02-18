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
#endregion

namespace Coders.Web.Models.Users
{
	public class UserSearchCreate : Value<UserSearch>
	{
		/// <summary>
		/// Gets or sets the reputation.
		/// </summary>
		/// <value>The reputation.</value>
		public int Reputation
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
		/// Gets or sets a value indicating whether this <see cref="UserSearchCreate"/> is save.
		/// </summary>
		/// <value><c>true</c> if save; otherwise, <c>false</c>.</value>
		public bool Save
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [name exact].
		/// </summary>
		/// <value><c>true</c> if [name exact]; otherwise, <c>false</c>.</value>
		public bool NameExact
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [reputation at least].
		/// </summary>
		/// <value><c>true</c> if [reputation at least]; otherwise, <c>false</c>.</value>
		public bool ReputationAtLeast
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created before.
		/// </summary>
		/// <value>The created before.</value>
		public DateTime? CreatedBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created after.
		/// </summary>
		/// <value>The created after.</value>
		public DateTime? CreatedAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last visit before.
		/// </summary>
		/// <value>The last visit before.</value>
		public DateTime? LastVisitBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last visit after.
		/// </summary>
		/// <value>The last visit after.</value>
		public DateTime? LastVisitAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last log on before.
		/// </summary>
		/// <value>The last log on before.</value>
		public DateTime? LastLogOnBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last log on after.
		/// </summary>
		/// <value>The last log on after.</value>
		public DateTime? LastLogOnAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the searches.
		/// </summary>
		/// <value>The searches.</value>
		public IList<UserSearch> Searches
		{
			get; 
			private set;
		}

		/// <summary>
		/// Initializes the specified searches.
		/// </summary>
		/// <param name="searches">The searches.</param>
		public void Initialize(IList<UserSearch> searches)
		{
			if (searches == null)
			{
				this.Searches = new List<UserSearch>();
			}

			this.Searches = searches;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(UserSearch entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Reputation = this.Reputation > 0 ? this.Reputation : (int?)null;
			entity.Name = this.Name;
			entity.EmailAddress = this.EmailAddress;
			entity.NameExact = this.NameExact;
			entity.ReputationAtLeast = this.ReputationAtLeast;
			entity.CreatedBefore = this.CreatedBefore;
			entity.CreatedAfter = this.CreatedAfter;
			entity.LastVisitBefore = this.LastVisitBefore;
			entity.LastVisitAfter = this.LastVisitAfter;
			entity.LastLogOnBefore = this.LastLogOnBefore;
			entity.LastLogOnAfter = this.LastLogOnAfter;

			if (!this.Save)
			{
				return;
			}

			entity.Title = this.Title;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new UserSearchCreateValidatorCollection().Validate(this);
		}
	}

	public class UserSearchCreateValidatorCollection : AbstractValidator<UserSearchCreate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserSearchCreateValidatorCollection"/> class.
		/// </summary>
		public UserSearchCreateValidatorCollection()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.When(x => x.Save)
				.WithMessage(Errors.Required.FormatInvariant(Titles.Title));
		}
	}
}