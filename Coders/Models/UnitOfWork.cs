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
using System.Collections;
using System.Threading;
using System.Web;
#endregion

namespace Coders.Models
{
	public static class UnitOfWork
	{
		// private static fields / properties
		private static IUnitOfWorkFactory _factory;
		private static readonly Hashtable Threads = new Hashtable();

		// private constants
		private const string Key = "Coders.Models.UnitOfWork";

		/// <summary>
		/// Commits this instance.
		/// </summary>
		public static void Commit()
		{
			var work = GetUnitOfWork();

			if (work != null)
			{
				work.Commit();
			}
		}

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>The current.</value>
		public static IUnitOfWork Current
		{
			get
			{
				var work = GetUnitOfWork();

				if (work == null)
				{
					_factory = ServiceLocator.Current.GetInstance<IUnitOfWorkFactory>();

					work = _factory.Create();

					SaveUnitOfWork(work);
				}

				return work;
			}
		}

		/// <summary>
		/// Gets the unit of work.
		/// </summary>
		/// <returns></returns>
		private static IUnitOfWork GetUnitOfWork()
		{
			if (HttpContext.Current != null)
			{
				if (HttpContext.Current.Items.Contains(Key))
				{
					return HttpContext.Current.Items[Key] as IUnitOfWork;
				}

				return null;
			}
			else
			{
				var thread = Thread.CurrentThread;

				if (string.IsNullOrEmpty(thread.Name))
				{
					thread.Name = Guid.NewGuid().ToString();

					return null;
				}

				lock (Threads.SyncRoot)
				{
					return Threads[Thread.CurrentThread.Name] as IUnitOfWork;
				}
			}
		}

		/// <summary>
		/// Saves the unit of work.
		/// </summary>
		/// <param name="work">The work.</param>
		private static void SaveUnitOfWork(IUnitOfWork work)
		{
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Items[Key] = work;
			}
			else
			{
				lock (Threads.SyncRoot)
				{
					Threads[Thread.CurrentThread.Name] = work;
				}
			}
		}
	}
}