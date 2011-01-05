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
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
#endregion

namespace Coders.Repositories.Conventions
{
	public class EnumConvention : IPropertyConvention, IPropertyConventionAcceptance
	{
		/// <summary>
		/// Accepts the specified criteria.
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
		{
			if (criteria == null)
			{
				throw new ArgumentNullException("criteria");
			}

			criteria.Expect(x => x.Property.PropertyType.IsEnum);
		}

		/// <summary>
		/// Applies the specified instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		public void Apply(IPropertyInstance instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}

			instance.CustomType(instance.Property.PropertyType);
		}
	}
}