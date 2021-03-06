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
using Coders.Web.ViewModels;
using FluentValidation.Results;
#endregion

namespace Coders.Web.Models
{
	public interface IValue
	{
		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>The message.</value>
		MessageViewModel Message { get; }

		/// <summary>
		/// Gets the errors.
		/// </summary>
		/// <value>The errors.</value>
		IList<ValidationFailure> Errors { get; }

		/// <summary>
		/// Validates this instance.
		/// </summary>
		void Validate();
	}

	public interface IValue<in T> : IValue
	{
		/// <summary>
		/// Converts this instance to the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void ValueToModel(T entity);
	}
}