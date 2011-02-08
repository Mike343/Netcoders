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
#endregion

namespace Coders.Authentication
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Privilege<T> : IPrivilege<T>
	{
		// private fields
		private readonly PrivilegePrincipal _principal;

		/// <summary>
		/// Initializes a new instance of the <see cref="Privilege&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="role">The role.</param>
		public Privilege(string role)
		{
			_principal = PrivilegePrincipalPermission.Current;

			if (_principal == null)
			{
				return;
			}

			this.Role = role;
			this.Identity = _principal.Identity as UserIdentity;
		}

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>The role.</value>
		public string Role
		{
			get; 
			private set; 
		}

		/// <summary>
		/// Gets or sets the identity.
		/// </summary>
		/// <value>The identity.</value>
		public UserIdentity Identity
		{
			get;
			private set;
		}

		/// <summary>
		/// Determines whether this instance can view the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can view the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanView(T entity)
		{
			return this.CanView(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance can view the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can view the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanView(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.View);
		}

		/// <summary>
		/// Determines whether this instance [can view any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can view any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanViewAny(T entity)
		{
			return this.CanViewAny(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance [can view any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can view any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanViewAny(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.ViewAny);
		}

		/// <summary>
		/// Determines whether this instance can create the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can create the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanCreate(T entity)
		{
			return this.CanCreate(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance can create the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can create the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanCreate(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.Create);
		}

		/// <summary>
		/// Determines whether this instance can update the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can update the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanUpdate(T entity)
		{
			return this.CanUpdate(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance can update the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can update the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanUpdate(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.Update);
		}

		/// <summary>
		/// Determines whether this instance [can update any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can update any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanUpdateAny(T entity)
		{
			return this.CanUpdateAny(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance [can update any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can update any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanUpdateAny(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.UpdateAny);
		}

		/// <summary>
		/// Determines whether this instance can delete the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can delete the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanDelete(T entity)
		{
			return this.CanDelete(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance can delete the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can delete the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanDelete(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.Delete);
		}

		/// <summary>
		/// Determines whether this instance [can delete any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can delete any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanDeleteAny(T entity)
		{
			return this.CanDeleteAny(entity, this.Identity);
		}

		/// <summary>
		/// Determines whether this instance [can delete any] the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can delete any] the specified entity; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool CanDeleteAny(T entity, UserIdentity identity)
		{
			return _principal.ContainsRole(this.Role) && _principal.Privileges[this.Role].AllowedTo(Privileges.DeleteAny);
		}
	}
}