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
using Coders.Models;
using Coders.Models.TimeZones;
using TimeZone = Coders.Models.TimeZones.TimeZone;
#endregion

namespace Coders.Services
{
	public class TimeZoneService : EntityService<TimeZone>, ITimeZoneService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeZoneService"/> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		public TimeZoneService(IRepository<TimeZone> repository)
			: base(repository)
		{

		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public override IList<TimeZone> GetAll()
		{
			if (TimeZone.TimeZones.Count > 0)
			{
				return TimeZone.TimeZones;
			}

			var timeZones = base.GetAll();

			TimeZone.Cache(timeZones);

			return timeZones;
		}

		/// <summary>
		/// Inserts or updates the specified time zone.
		/// </summary>
		/// <param name="timeZone">The time zone.</param>
		public void InsertOrUpdate(TimeZone timeZone)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}

			if (timeZone.Id > 0)
			{
				this.Update(timeZone);
			}
			else
			{
				this.Insert(timeZone);
			}
		}
	}
}