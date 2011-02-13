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
using Coders.Extensions;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Users
{
	[Validator(typeof(UserBanCreateOrUpdateValidatorCollection))]
	public class UserBanCreateOrUpdate : Value<UserBan>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanCreateOrUpdate"/> class.
		/// </summary>
		public UserBanCreateOrUpdate()
		{
			if (string.IsNullOrEmpty(this.Expire))
			{
				this.Expire = DateTime.Now.AddDays(7).ToString("M/d/yyyy", CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanCreateOrUpdate"/> class.
		/// </summary>
		/// <param name="ban">The ban.</param>
		public UserBanCreateOrUpdate(UserBan ban)
		{
			if (ban == null)
			{
				throw new ArgumentNullException("ban");
			}

			this.Id = ban.Id;
			this.Reason = ban.Reason;
			this.IsPermanent = ban.IsPermanent;

			if (ban.Expire.HasValue)
			{
				this.Expire = ban.Expire.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
			}

			this.User = ban.User;
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
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the reason.
		/// </summary>
		/// <value>The reason.</value>
		public string Reason
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is permanent.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is permanent; otherwise, <c>false</c>.
		/// </value>
		public bool IsPermanent
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the expire.
		/// </summary>
		/// <value>The expire.</value>
		public string Expire
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		public User User
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		public void Initialize(User user)
		{
			this.User = user;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(UserBan entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Reason = this.Reason;
			entity.IsPermanent = this.IsPermanent;

			if (!entity.IsPermanent)
			{
				entity.Expire = DateTime.Parse(this.Expire, CultureInfo.InvariantCulture);
			}
		}
	}

	public class UserBanCreateOrUpdateValidatorCollection : AbstractValidator<UserBanCreateOrUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserBanCreateOrUpdateValidatorCollection"/> class.
		/// </summary>
		public UserBanCreateOrUpdateValidatorCollection()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.When(x => x.Id <= 0)
				.WithMessage(Errors.Required.FormatInvariant(Titles.Name));

			RuleFor(x => x.Name)
				.UserNameMustExist()
				.When(x => x.Id <= 0);

			RuleFor(x => x.Name)
				.UserNameNotProtected()
				.When(x => x.Id <= 0);

			RuleFor(x => x.Reason)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Reason));

			RuleFor(x => x.Expire)
				.NotEmpty()
				.When(x => !x.IsPermanent)
				.WithMessage(Errors.Required.FormatInvariant(Titles.Expires));

			RuleFor(x => x.Expire)
				.Date()
				.When(x => !x.IsPermanent);
		}
	}
}