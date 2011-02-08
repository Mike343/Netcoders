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
using Coders.Extensions;
using Coders.Models.Common.Enums;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Web.Controllers.Users.Administration.Queries
{
	public class HomeQuery
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HomeQuery"/> class.
		/// </summary>
		/// <param name="status">The status.</param>
		/// <param name="sort">The sort.</param>
		/// <param name="order">The order.</param>
		/// <param name="page">The page.</param>
		public HomeQuery(string status, SortUser sort, SortOrder order, int? page)
		{
			IUserSpecification specification = null;

			if (!string.IsNullOrEmpty(status))
			{
				specification = new UserStatusSpecification(status.AsEnum<UserStatus>());
			}

			if (specification == null)
			{
				specification = new UserSpecification();
			}

			specification.Page = page;
			specification.Limit = Setting.UserPageLimit.Value;
			specification.Sort = sort;
			specification.Order = order;

			this.Specification = specification;
		}

		/// <summary>
		/// Gets or sets the specification.
		/// </summary>
		/// <value>The specification.</value>
		public IUserSpecification Specification
		{
			get;
			private set;
		}
	}
}