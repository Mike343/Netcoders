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
using System.Security;
using System.Threading;
using Coders.Extensions;
#endregion

namespace Coders.Authentication
{
	public class PrivilegePrincipalPermission : IPermission
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PrivilegePrincipalPermission"/> class.
		/// </summary>
		public PrivilegePrincipalPermission()
		{
			AuthorizeOnly = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PrivilegePrincipalPermission"/> class.
		/// </summary>
		/// <param name="role">The role.</param>
		/// <param name="action">The action.</param>
		public PrivilegePrincipalPermission(string role, Privileges action)
		{
			this.AuthorizeOnly = false;
			this.Role = role;
			this.Action = action;
		}

		/// <summary>
		/// Gets the current principal.
		/// </summary>
		/// <value>The current.</value>
		public static PrivilegePrincipal Current
		{
			get
			{
				return Thread.CurrentPrincipal as PrivilegePrincipal;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [authorize only].
		/// </summary>
		/// <value><c>true</c> if [authorize only]; otherwise, <c>false</c>.</value>
		public bool AuthorizeOnly
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		public Privileges Action
		{
			get;
			private set;
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
		/// Creates and returns an identical copy of the current permission.
		/// </summary>
		/// <returns>A copy of the current permission.</returns>
		public IPermission Copy()
		{
			return this.Clone() as IPermission;
		}

		/// <summary>
		/// Creates and returns a permission that is the intersection of the current permission and the specified permission.
		/// </summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>
		/// A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target"/> parameter is not null and is not an instance of the same class as the current permission. </exception>
		public IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}

			var permission = VerifyTypeMatch(target);

			return this.Clone(permission.AuthorizeOnly, this.Role, this.Action & permission.Action);
		}

		/// <summary>
		/// Creates a permission that is the union of the current permission and the specified permission.
		/// </summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>
		/// A new permission that represents the union of the current permission and the specified permission.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target"/> parameter is not null and is not of the same type as the current permission. </exception>
		public IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return Copy();
			}

			var permission = VerifyTypeMatch(target);

			return this.Clone((permission.AuthorizeOnly || this.AuthorizeOnly), this.Role, this.Action | permission.Action);
		}

		/// <summary>
		/// Determines whether the current permission is a subset of the specified permission.
		/// </summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		/// true if the current permission is a subset of the specified permission; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target"/> parameter is not null and is not of the same type as the current permission. </exception>
		public bool IsSubsetOf(IPermission target)
		{
			var permission = target as PrivilegePrincipalPermission;

			if (permission == null)
			{
				return false;
			}

			if (this.AuthorizeOnly)
			{
				return true;
			}

			if (permission.AuthorizeOnly)
			{
				return false;
			}

			if (permission.Role != this.Role)
			{
				return false;
			}

			return ((permission.Action | this.Action) == this.Action);

		}

		/// <summary>
		/// Throws a <see cref="T:System.Security.SecurityException"/> at run time if the security requirement is not met.
		/// </summary>
		public void Demand()
		{
			if (this.Authorized())
			{
				return;
			}

			throw new SecurityException("Not Authorized");
		}

		/// <summary>
		/// Authorizeds this instance.
		/// </summary>
		/// <returns></returns>
		public bool Authorized()
		{
			var principal = Current;

			if (principal == null)
			{
				return false;
			}

			principal.DetermineRolePrivileges();

			return !AuthorizeOnly && principal.AllowedTo(this.Role, this.Action);
		}

		/// <summary>
		/// Creates an XML encoding of the security object and its current state.
		/// </summary>
		/// <returns>
		/// An XML encoding of the security object, including any state information.
		/// </returns>
		public SecurityElement ToXml()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reconstructs a security object with a specified state from an XML encoding.
		/// </summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		public void FromXml(SecurityElement e)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Clones the current instance with the specified parameters.
		/// </summary>
		/// <param name="authorizeOnly">if set to <c>true</c> [authorize only].</param>
		/// <param name="role">The role.</param>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		private PrivilegePrincipalPermission Clone(bool authorizeOnly, string role, Privileges action)
		{
			var clone = (PrivilegePrincipalPermission)this.Clone();

			clone.AuthorizeOnly = authorizeOnly;
			clone.Role = role;
			clone.Action = action;

			return clone;
		}

		/// <summary>
		/// Verifies the type match.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		private PrivilegePrincipalPermission VerifyTypeMatch(IPermission target)
		{
			if (this.GetType() != target.GetType())
			{
				throw new ArgumentException("Target must be of the {0} type".FormatInvariant(GetType().FullName));
			}

			return target as PrivilegePrincipalPermission;
		}

		#region ICloneable Members
		public object Clone()
		{
			return this.MemberwiseClone();
		}
		#endregion
	}
}