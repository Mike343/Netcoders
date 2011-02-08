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
using Coders.Models.Common;
using Coders.Models.Common.Enums;
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
		/// <param name="auditService">The audit service.</param>
		/// <param name="timeZoneRepository">The time zone repository.</param>
		public TimeZoneService(
			IAuditService<TimeZone, TimeZoneAudit> auditService,
			IRepository<TimeZone> timeZoneRepository)
			: base(timeZoneRepository)
		{
			this.AuditService = auditService;
		}

		/// <summary>
		/// Gets the audit service.
		/// </summary>
		public IAuditService<TimeZone, TimeZoneAudit> AuditService
		{
			get;
			private set;
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
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(TimeZone entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Insert(entity);

			this.AuditService.Audit(entity, AuditAction.Create);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(TimeZone entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Update(entity);

			this.AuditService.Audit(entity, AuditAction.Update);
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

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(TimeZone entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Delete(entity);

			this.AuditService.Audit(entity, AuditAction.Delete);
		}
	}
}