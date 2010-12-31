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
#endregion

namespace Coders.Models
{
	public abstract class EntityBase : IEquatable<EntityBase>, IEntity
	{
		// private fields
		private int _transientHashCode;

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public virtual int Id
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is transient.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is transient; otherwise, <c>false</c>.
		/// </value>
		private bool IsTransient
		{
			get
			{
				return this.Id == 0;
			}
		}

		/// <summary>
		/// Equalses the specified other.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public virtual bool Equals(EntityBase other)
		{
			if (other == null)
			{
				return false;
			}

			if (this.IsTransient)
			{
				return ReferenceEquals(this, other);
			}

			return other.Id == this.Id && other.GetType() == this.GetType();
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EntityBase);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			if (this.IsTransient)
			{
				if (_transientHashCode == 0)
				{
					_transientHashCode = base.GetHashCode();
				}

				return _transientHashCode;
			}

			return this.Id;
		}
	}
}