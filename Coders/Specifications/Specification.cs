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
    public abstract class Specification<T> : ISpecification<T>
    {
		/// <summary>
		/// Ands the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
        public AndSpecification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

		/// <summary>
		/// Ors the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
        public OrSpecification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

		/// <summary>
		/// Nots the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
        public NotSpecification<T> Not(Specification<T> specification)
        {
            return new NotSpecification<T>(this, specification);
        }

		/// <summary>
		/// Determines whether [is satisfied by] [the specified obj].
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if [is satisfied by] [the specified obj]; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool IsSatisfiedBy(T entity);
    }
}