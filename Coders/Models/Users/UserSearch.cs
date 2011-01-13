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

namespace Coders.Models.Users
{
	public class UserSearch : EntityBase
	{
		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		/// <value>The user id.</value>
		public virtual int? UserId
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the reputation.
		/// </summary>
		/// <value>The reputation.</value>
		public virtual int? Reputation
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public virtual string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public virtual string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		public virtual string EmailAddress
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [name exact].
		/// </summary>
		/// <value><c>true</c> if [name exact]; otherwise, <c>false</c>.</value>
		public virtual bool NameExact
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [reputation at least].
		/// </summary>
		/// <value><c>true</c> if [reputation at least]; otherwise, <c>false</c>.</value>
		public virtual bool ReputationAtLeast
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created before.
		/// </summary>
		/// <value>The created before.</value>
		public virtual DateTime? CreatedBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created after.
		/// </summary>
		/// <value>The created after.</value>
		public virtual DateTime? CreatedAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last visit before.
		/// </summary>
		/// <value>The last visit before.</value>
		public virtual DateTime? LastVisitBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last visit after.
		/// </summary>
		/// <value>The last visit after.</value>
		public virtual DateTime? LastVisitAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last log on before.
		/// </summary>
		/// <value>The last log on before.</value>
		public virtual DateTime? LastLogOnBefore
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last log on after.
		/// </summary>
		/// <value>The last log on after.</value>
		public virtual DateTime? LastLogOnAfter
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>The created.</value>
		public virtual DateTime Created
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the updated.
		/// </summary>
		/// <value>The updated.</value>
		public virtual DateTime Updated
		{
			get;
			set;
		}
	}
}