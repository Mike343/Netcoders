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
using Coders.Models.Logs;
using Coders.Models.Settings;
using Coders.Strings;
#endregion

namespace Coders.Services
{
	public class SettingService : EntityService<Setting>, ISettingService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingService"/> class.
		/// </summary>
		/// <param name="logService">The log service.</param>
		/// <param name="repository">The repository.</param>
		public SettingService(
			ILogService logService, 
			ISettingRepository repository)
			: base(repository)
		{
			this.LogService = logService;
			this.SettingRepository = repository;
		}

		public ILogService LogService
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
				this.LogService.Log(setting.Id, Log.Settings, Logs.SettingUpdated, setting);
			}
			else
			{
				this.Insert(setting);
				this.LogService.Log(setting.Id, Log.Settings, Logs.SettingCreated, setting);
			}
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
			this.LogService.Log(setting.Id, Log.Settings, Logs.SettingUpdated, setting);
		}
	}
}