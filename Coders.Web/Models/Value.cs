﻿#region License
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
using System.Collections.Generic;
using Coders.Authentication;
using Coders.Web.ActionResults.Enums;
using Coders.Web.ViewModels;
using FluentValidation.Results;
#endregion

namespace Coders.Web.Models
{
	public abstract class Value : IValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Value&lt;T&gt;"/> class.
		/// </summary>
		protected Value()
		{
			var principal = PrivilegePrincipalPermission.Current;

			if (principal == null)
			{
				return;
			}

			this.Identity = principal.Identity as UserIdentity;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid
		{
			get
			{
				return this.Result.IsValid;
			}
		}

		/// <summary>
		/// Gets or sets the identity.
		/// </summary>
		/// <value>The identity.</value>
		public UserIdentity Identity
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public MessageViewModel Message
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the result.
		/// </summary>
		/// <value>The result.</value>
		public ValidationResult Result
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets the errors.
		/// </summary>
		/// <value>The errors.</value>
		public IList<ValidationFailure> Errors
		{
			get
			{
				return this.Result.Errors;
			}
		}

		/// <summary>
		/// Generates a success message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void SuccessMessage(string message)
		{
			this.Message = new MessageViewModel(message, MessageScope.Success);
		}

		/// <summary>
		/// Generates a warning message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void WarningMessage(string message)
		{
			this.Message = new MessageViewModel(message, MessageScope.Warning);
		}

		/// <summary>
		/// Generates a error message.
		/// </summary>
		/// <param name="message">The message.</param>
		public void ErrorMessage(string message)
		{
			this.Message = new MessageViewModel(message, MessageScope.Error);
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		public abstract void Validate();
	}

	public abstract class Value<T> : Value, IValue<T>
	{
		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public abstract void ValueToModel(T entity);
	}
}