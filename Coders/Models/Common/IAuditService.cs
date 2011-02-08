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

using System.Runtime.Serialization;
using Coders.Collections;
using Coders.Models.Common.Enums;
using Coders.Specifications;
#endregion

namespace Coders.Models.Common
{
	public interface IAuditService<in T, in TK>
		where T : class, IEntity
		where TK : class, IAuditable<T>, new()
	{
		/// <summary>
		/// Gets the all the audits paged by the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		IPagedCollection<Audit> GetPaged(ISpecification<Audit> specification);

		/// <summary>
		/// Audits the specified value.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="action">The action.</param>
		void Audit(T entity, AuditAction action);
	}
}