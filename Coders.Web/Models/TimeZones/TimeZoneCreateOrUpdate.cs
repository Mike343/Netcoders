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
using Coders.Strings;
using FluentValidation;
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Web.Models.TimeZones
{
	public class TimeZoneCreateOrUpdate : Value<TimeZone>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneCreateOrUpdate"/> class.
		/// </summary>
		public TimeZoneCreateOrUpdate()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneCreateOrUpdate"/> class.
		/// </summary>
		/// <param name="timeZone">The time zone.</param>
		public TimeZoneCreateOrUpdate(TimeZone timeZone)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}

			this.Id = timeZone.Id;
			this.Title = timeZone.Title;
			this.Display = timeZone.Display;
			this.Offset = timeZone.Offset;
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
		/// Gets or sets the display.
		/// </summary>
		/// <value>The display.</value>
		public string Display
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the offset.
		/// </summary>
		/// <value>The offset.</value>
		public double Offset
		{
			get;
			set;
		}

		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void ValueToModel(TimeZone entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			entity.Title = this.Title;
			entity.Display = this.Display;
			entity.Offset = this.Offset;
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public override void Validate()
		{
			base.Result = new TimeZoneCreateOrUpdateValidatorCollection().Validate(this);
		}
	}

	public class TimeZoneCreateOrUpdateValidatorCollection : AbstractValidator<TimeZoneCreateOrUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneCreateOrUpdateValidatorCollection"/> class.
		/// </summary>
		public TimeZoneCreateOrUpdateValidatorCollection()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Title));

			RuleFor(x => x.Display)
				.NotEmpty()
				.WithMessage(Errors.Required.FormatInvariant(Titles.Display));
		}
	}
}