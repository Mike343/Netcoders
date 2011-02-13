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
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
#endregion

namespace Coders.Models.Common
{
	public class Audit : EntityBase
	{
		/// <summary>
		/// Gets or sets the parent id.
		/// </summary>
		/// <value>
		/// The parent id.
		/// </value>
		public virtual int ParentId
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public virtual string Type
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public virtual string Data
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		public virtual AuditAction Action
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>
		/// The created.
		/// </value>
		public virtual DateTime Created
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		public virtual IUser User
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public virtual T GetEntity<T>() where T : class
		{
			var serializer = new XmlSerializer(typeof(T));

			using (var reader = new StringReader(this.Data))
			{
				XmlReader xml = new XmlTextReader(reader);

				var result = serializer.Deserialize(xml) as T;

				reader.Close();

				return result;
			}
		}
	}
}