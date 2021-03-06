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
using Coders.Web.Models;
#endregion

namespace Coders.Web.ViewModels
{
	public class AttachmentFilterViewModel : FilterViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentFilterViewModel"/> class.
		/// </summary>
		public AttachmentFilterViewModel()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentFilterViewModel"/> class.
		/// </summary>
		/// <param name="types">The types.</param>
		/// <param name="statuses">The statuses.</param>
		/// <param name="sorts">The sorts.</param>
		public AttachmentFilterViewModel(IEnumerable<Filter> types, IEnumerable<Filter> statuses, IEnumerable<Filter> sorts) 
			: base(sorts)
		{
			this.Types = types;
			this.Statuses = statuses;
		}

		/// <summary>
		/// Gets the groups.
		/// </summary>
		public IEnumerable<Filter> Types
		{
			get; 
			private set;
		}

		public IEnumerable<Filter> Statuses
		{
			get;
			private set;
		}
	}
}