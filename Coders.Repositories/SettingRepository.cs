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
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Settings;
using NHibernate.Linq;
#endregion

namespace Coders.Repositories
{
	public class SettingRepository : NHibernateRepository<Setting>, ISettingRepository
	{
		/// <summary>
		/// Gets the groups.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetGroups()
		{
			var session = base.Session;

			using (var transaction = session.BeginTransaction())
			{
				var entities = session.Query<Setting>()
					.GroupBy(x => x.Group)
					.Select(x => x.Key)
					.Distinct()
					.ToList();

				transaction.Commit();

				return entities;
			}
		}
	}
}