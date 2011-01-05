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
using System;
using Coders.Models.Settings;
#endregion

namespace Coders.Services
{
	public class SettingService : EntityService<Setting>, ISettingService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingService"/> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		public SettingService(ISettingRepository repository)
			: base(repository)
		{

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
	}
}