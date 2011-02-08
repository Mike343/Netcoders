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
using Coders.Models.Users.Enums;
#endregion

namespace Coders.Models.Users
{
	[Serializable]
	public class UserAudit : IAuditable<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAudit"/> class.
		/// </summary>
		public UserAudit()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserAudit"/> class.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected UserAudit(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			this.Reputation = info.GetInt32("Reputation");
			this.Name = info.GetString("Name");
			this.Title = info.GetString("Title");
			this.EmailAddress = info.GetString("EmailAddress");
			this.Signature = info.GetString("Signature");
			this.Status = (UserStatus)info.GetInt32("Status");
		}

		/// <summary>
		/// Gets or sets the reputation.
		/// </summary>
		/// <value>The reputation.</value>
		public int Reputation
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
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
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public string EmailAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the signature.
		/// </summary>
		/// <value>The signature.</value>
		public string Signature
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public UserStatus Status
		{
			get;
			set;
		}

		/// <summary>
		/// Copies the specified value to the audit.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void ValueToAudit(User entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			this.Reputation = entity.Reputation;
			this.Name = entity.Name;
			this.Title = entity.Title;
			this.EmailAddress = entity.EmailAddress;
			this.Signature = entity.Signature;
			this.Status = entity.Status;
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

			info.AddValue("Reputation", this.Reputation);
			info.AddValue("Name", this.Name);
			info.AddValue("Title", this.Title);
			info.AddValue("EmailAddress", this.EmailAddress);
			info.AddValue("Signature", this.Signature);
			info.AddValue("Status", this.Status);
		}
	}
}