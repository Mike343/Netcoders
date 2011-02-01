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
using System.Linq;
using System.Linq.Expressions;
using Coders.Models.Settings.Enums;
using Coders.Specifications;
#endregion

namespace Coders.Models.Settings
{
	public class SettingSpecification : Specification<Setting>, ISettingSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingSpecification"/> class.
		/// </summary>
		public SettingSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public SettingSpecification(Expression<Func<Setting, bool>> predicate)
			: base(predicate)
		{

		}

		/// <summary>
		/// Gets or sets the sort.
		/// </summary>
		/// <value>The sort.</value>
		public SortSetting Sort
		{
			get;
			set;
		}

		/// <summary>
		/// Sorts the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public override IQueryable<Setting> OrderEntities(IQueryable<Setting> entities, ISpecification<Setting> specification)
		{
			var spec = specification as ISettingSpecification;

			if (spec == null)
			{
				return entities;
			}

			switch (spec.Sort)
			{
				case SortSetting.Title:
					return base.OrderBy(entities, x => x.Title, spec.Order);
				default:
					return base.OrderBy(entities, x => x.Group, spec.Order);
			}
		}
	}
}