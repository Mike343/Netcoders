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
using System.Runtime.Serialization;
using Coders.Models.Common;
#endregion

namespace Coders.Models.Users
{
	[Serializable]
	public class UserRoleAudit : IAuditable<UserRole>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleAudit"/> class.
		/// </summary>
		public UserRoleAudit()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRoleAudit"/> class.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected UserRoleAudit(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			this.Title = info.GetString("Title");
			this.IsDefault = info.GetBoolean("IsDefault");
			this.IsAdministrator = info.GetBoolean("IsAdministrator");
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is default.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefault
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is administrator.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is administrator; otherwise, <c>false</c>.
		/// </value>
		public bool IsAdministrator
		{
			get;
			set;
		}

		/// <summary>
		/// Copies the specified value to the audit.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void ValueToAudit(UserRole entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			this.Title = entity.Title;
			this.IsDefault = entity.IsDefault;
			this.IsAdministrator = entity.IsAdministrator;
		}

		/// <summary>
		/// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			info.AddValue("Title", this.Title);
			info.AddValue("IsDefault", this.IsDefault);
			info.AddValue("IsAdministrator", this.IsAdministrator);
		}
	}
}