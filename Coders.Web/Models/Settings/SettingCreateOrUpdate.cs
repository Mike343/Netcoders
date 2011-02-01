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
using Coders.Models.Settings;
using Coders.Strings;
using Coders.Web.Extensions;
using FluentValidation;
using FluentValidation.Attributes;
#endregion

namespace Coders.Web.Models.Settings
{
	[Validator(typeof(SettingCreateOrUpdateValidatorCollection))]
	public class SettingCreateOrUpdate : Value<Setting>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingCreateOrUpdate"/> class.
		/// </summary>
		public SettingCreateOrUpdate()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingCreateOrUpdate"/> class.
		/// </summary>
		/// <param name="setting">The setting.</param>
		public SettingCreateOrUpdate(Setting setting)
		{
			if (setting == null)
			{
				throw new ArgumentNullException("setting");
			}

			this.Id = setting.Id;
			this.Title = setting.Title;
			this.Group = setting.Group;
			this.ItemKey = setting.Key;
			this.CurrentKey = setting.Key;
			this.ItemValue = setting.Value;
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
		/// Gets or sets the group.
		/// </summary>
		/// <value>The group.</value>
		public string Group
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the item key.
		/// </summary>
		/// <value>The item key.</value>
		public string ItemKey
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current key.
		/// </summary>
		/// <value>The current key.</value>
		public string CurrentKey
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the item value.
		/// </summary>
		/// <value>The item value.</value>
		public string ItemValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a value indicating whether [key changed].
		/// </summary>
		/// <value><c>true</c> if [key changed]; otherwise, <c>false</c>.</value>
		public bool KeyChanged
		{
			get
			{
				return this.ItemKey != this.CurrentKey;
			}
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(Setting entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Title = this.Title;
			entity.Group = this.Group;
			entity.Key = this.ItemKey;
			entity.Value = this.ItemValue;
		}
	}

	public class SettingCreateOrUpdateValidatorCollection : AbstractValidator<SettingCreateOrUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingCreateOrUpdateValidatorCollection"/> class.
		/// </summary>
		public SettingCreateOrUpdateValidatorCollection()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Title));

			RuleFor(x => x.Group)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Group));

			RuleFor(x => x.ItemKey)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Key));

			RuleFor(x => x.ItemValue)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Value));

			RuleFor(x => x.ItemKey)
				.SettingUnique()
				.When(x => x.Id == 0 || x.KeyChanged);
		}
	}
}