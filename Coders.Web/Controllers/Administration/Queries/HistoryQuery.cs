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
using Coders.Models.Common.Enums;
using Coders.Models.Logs;
using Coders.Models.Logs.Enums;
using Coders.Models.Settings;
#endregion

namespace Coders.Web.Controllers.Administration.Queries
{
	public class HistoryQuery
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HistoryQuery"/> class.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <param name="sort">The sort.</param>
		/// <param name="order">The order.</param>
		/// <param name="page">The page.</param>
		/// <param name="id">The id.</param>
		public HistoryQuery(string group, SortLog sort, SortOrder order, int? page, int? id)
		{
			var specification = id.HasValue
				? new LogGroupSpecification(id.Value, group)
				: new LogGroupSpecification(group);

			specification.Page = page;
			specification.Limit = Setting.LogPageLimit.Value;
			specification.Sort = sort;
			specification.Order = order;

			this.Specification = specification;
		}

		/// <summary>
		/// Gets or sets the specification.
		/// </summary>
		/// <value>The specification.</value>
		public ILogSpecification Specification
		{
			get; 
			private set;
		}
	}
}