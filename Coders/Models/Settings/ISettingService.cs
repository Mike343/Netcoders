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
#endregion

namespace Coders.Models.Settings
{
	public interface ISettingService : IEntityService<Setting>
	{
		/// <summary>
		/// Gets the groups.
		/// </summary>
		/// <returns></returns>
		IList<string> GetGroups();

		/// <summary>
		/// Rebuilds the settings cache
		/// </summary>
		void Rebuild();

		/// <summary>
		/// Inserts or updates the specified setting.
		/// </summary>
		/// <param name="setting">The setting.</param>
		void InsertOrUpdate(Setting setting);

		/// <summary>
		/// Updates the setting specified key and value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		void Update(string key, object value);
	}
}