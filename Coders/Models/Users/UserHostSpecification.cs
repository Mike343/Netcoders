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
using System.Linq.Expressions;
using Coders.Specifications;
#endregion

namespace Coders.Models.Users
{
	public class UserHostSpecification : Specification<UserHost>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostSpecification"/> class.
		/// </summary>
		public UserHostSpecification()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostSpecification"/> class.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		public UserHostSpecification(Expression<Func<UserHost, bool>> predicate)
			: base(predicate)
		{

		}
	}
}