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

namespace Coders.Web.Models
{
	public class Filter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Filter"/> class.
		/// </summary>
		public Filter()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Filter"/> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="parameters">The parameters.</param>
		public Filter(string title, object parameters) 
			: this(title, parameters, null)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Filter"/> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="parameters">The parameters.</param>
		/// <param name="conditions">The conditions.</param>
		public Filter(string title, object parameters, object conditions)
		{
			this.Title = title;
			this.Parameters = parameters;
			this.Conditions = conditions ?? this.Parameters;
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
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>The parameters.</value>
		public object Parameters
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the conditions.
		/// </summary>
		/// <value>The conditions.</value>
		public object Conditions
		{
			get;
			set;
		}
	}
}