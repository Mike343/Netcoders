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
using Coders.Models;
using NHibernate;
#endregion

namespace Coders.Repositories
{
	public class NHibernateUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NHibernateUnitOfWork"/> class.
		/// </summary>
		/// <param name="session">The session.</param>
		public NHibernateUnitOfWork(ISession session)
		{
			this.Session = session;
		}

		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		/// <value>The session.</value>
		public ISession Session
		{
			get; 
			private set;
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		public void Dispose()
		{
			if (this.Session == null)
			{
				return;
			}

			this.Session.Dispose();
		}
	}
}