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
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Settings;
#endregion

namespace Coders.Services
{
	public class SettingService : EntityService<Setting>, ISettingService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingService"/> class.
		/// </summary>
		/// <param name="auditService">The audit service.</param>
		/// <param name="settingRepository">The setting repository.</param>
		public SettingService(
			IAuditService<Setting, SettingAudit> auditService,
			ISettingRepository settingRepository)
			: base(settingRepository)
		{
			this.AuditService = auditService;
			this.SettingRepository = settingRepository;
		}

		/// <summary>
		/// Gets the audit service.
		/// </summary>
		public IAuditService<Setting, SettingAudit> AuditService
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets or sets the setting repository.
		/// </summary>
		/// <value>The setting repository.</value>
		public ISettingRepository SettingRepository
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets the groups.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetGroups()
		{
			return this.SettingRepository.GetGroups();
		}

		/// <summary>
		/// Rebuilds the settings cache
		/// </summary>
		public void Rebuild()
		{
			var settings = this.GetAll();

			Setting.Rebuild(settings);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(Setting entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Insert(entity);

			this.AuditService.Audit(entity, AuditAction.Create);
		}

		/// <summary>
		/// Inserts or updates the specified setting.
		/// </summary>
		/// <param name="setting">The setting.</param>
		public void InsertOrUpdate(Setting setting)
		{
			if (setting == null)
			{
				throw new ArgumentNullException("setting");
			}

			if (setting.Id > 0)
			{
				this.Update(setting);
			}
			else
			{
				this.Insert(setting);
			}
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(Setting entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Update(entity);

			this.AuditService.Audit(entity, AuditAction.Update);
		}

		/// <summary>
		/// Updates the setting specified key and value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void Update(string key, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			var setting = this.GetBy(new SettingKeySpecification(key));

			setting.Value = value.ToString();

			this.Update(setting);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(Setting entity)
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