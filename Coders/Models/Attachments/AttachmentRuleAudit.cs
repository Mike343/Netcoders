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

namespace Coders.Models.Attachments
{
	[Serializable]
	public class AttachmentRuleAudit : IAuditable<AttachmentRule>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleAudit"/> class.
		/// </summary>
		public AttachmentRuleAudit()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentRuleAudit"/> class.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected AttachmentRuleAudit(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			this.FileSize = info.GetInt32("FileSize");
			this.FileWidth = info.GetInt32("FileWidth");
			this.FileHeight = info.GetInt32("FileHeight");
			this.Group = info.GetString("Group");
			this.FileType = info.GetString("FileType");
			this.FileExtension = info.GetString("FileExtension");
		}

		/// <summary>
		/// Gets or sets the size of the file.
		/// </summary>
		/// <value>The size of the file.</value>
		public int FileSize
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the width of the file.
		/// </summary>
		/// <value>The width of the file.</value>
		public int FileWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of the file.
		/// </summary>
		/// <value>The height of the file.</value>
		public int FileHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the group.
		/// </summary>
		/// <value>The group.</value>
		public string Group
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>The type of the file.</value>
		public string FileType
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the file extension.
		/// </summary>
		/// <value>The file extension.</value>
		public string FileExtension
		{
			get;
			set;
		}

		/// <summary>
		/// Copies the specified value to the audit.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void ValueToAudit(AttachmentRule entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			this.FileSize = entity.FileSize;
			this.FileWidth = entity.FileWidth;
			this.FileHeight = entity.FileHeight;
			this.Group = entity.Group;
			this.FileType = entity.FileType;
			this.FileExtension = entity.FileExtension;
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

			info.AddValue("FileSize", this.FileSize);
			info.AddValue("FileWidth", this.FileWidth);
			info.AddValue("FileHeight", this.FileHeight);
			info.AddValue("Group", this.Group);
			info.AddValue("FileType", this.FileType);
			info.AddValue("FileExtension", this.FileExtension);
		}
	}
}