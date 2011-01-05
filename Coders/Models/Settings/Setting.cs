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
using Coders.Extensions;
#endregion

namespace Coders.Models.Settings
{
	public partial class Setting : EntityBase
	{
		// settings cache dictionary
		private static readonly Dictionary<string, string> Values = new Dictionary<string, string>();

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public virtual string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the group.
		/// </summary>
		/// <value>The group.</value>
		public virtual string Group
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		public virtual string Key
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public virtual string Value
		{
			get;
			set;
		}

		/// <summary>
		/// Rebuilds the settings cache.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public static void Rebuild(IList<Setting> settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}

			lock (Values)
			{
				Values.Clear();

				foreach (var setting in settings)
				{
					Values.Add(setting.Key, setting.Value);
				}
			}
		}

		/// <summary>
		/// Gets the setting by key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static string GetByKey(string key)
		{
			return Values.ContainsKey(key) ? Values[key] : string.Empty;
		}

		/// <summary>
		/// Gets the setting by key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		public static string GetByKey(string key, params object[] parameters)
		{
			return GetByKey(key).FormatInvariant(parameters);
		}
	}
}