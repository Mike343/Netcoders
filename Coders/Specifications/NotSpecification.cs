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

namespace Coders.Specifications
{
	public class NotSpecification<T> : CompositeSpecification<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NotSpecification&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public NotSpecification(ISpecification<T> left, ISpecification<T> right)
			: base(left, right)
		{

		}

		/// <summary>
		/// Determines whether [is satisfied by] [the specified entity].
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if [is satisfied by] [the specified entity]; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsSatisfiedBy(T entity)
		{
			return base.Left.IsSatisfiedBy(entity) && !base.Right.IsSatisfiedBy(entity);
		}
	}
}