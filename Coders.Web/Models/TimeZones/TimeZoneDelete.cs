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
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Web.Models.TimeZones
{
	public class TimeZoneDelete
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneDelete"/> class.
		/// </summary>
		public TimeZoneDelete()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneDelete"/> class.
		/// </summary>
		/// <param name="timeZone">The time zone.</param>
		public TimeZoneDelete(TimeZone timeZone)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}

			this.Id = timeZone.Id;
			this.Title = timeZone.Display;
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
	}
}