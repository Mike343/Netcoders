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

namespace Coders.Authentication
{
	public interface IPrivilege<in T> where T : class
	{
		/// <summary>
		/// Determines whether this instance can view the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can view the specified entity; otherwise, <c>false</c>.
		/// </returns>
		bool CanView(T entity, UserIdentity identity);

		/// <summary>
		/// Determines whether this instance [can view any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can view any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		bool CanViewAny(T entity, UserIdentity identity);

		/// <summary>
		/// Determines whether this instance can create the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can create the specified entity; otherwise, <c>false</c>.
		/// </returns>
		bool CanCreate(T entity, UserIdentity identity);

		/// <summary>
		/// Determines whether this instance can update the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can update the specified entity; otherwise, <c>false</c>.
		/// </returns>
		bool CanUpdate(T entity, UserIdentity identity);

		/// <summary>
		/// Determines whether this instance [can update any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can update any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		bool CanUpdateAny(T entity, UserIdentity identity);

		/// <summary>
		/// Determines whether this instance can delete the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can delete the specified entity; otherwise, <c>false</c>.
		/// </returns>
		bool CanDelete(T entity, UserIdentity identity);

		/// <summary>
		/// Determines whether this instance [can delete any] the specified enity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can delete any] the specified enity; otherwise, <c>false</c>.
		/// </returns>
		bool CanDeleteAny(T entity, UserIdentity identity);
	}
}