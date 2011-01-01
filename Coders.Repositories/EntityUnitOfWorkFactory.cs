using System;
using System.Data.Objects;
using Coders.Models;

namespace Coders.Repositories
{
	public class EntityUnitOfWorkFactory : IUnitOfWorkFactory
	{
		// private static fields / properties
		private static Func<ObjectContext> _contextDelegate;
		private static readonly Object Locker = new object();

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns></returns>
		public IUnitOfWork Create()
		{
			ObjectContext context;

			lock (Locker)
			{
				context = _contextDelegate();
			}

			return new EntityUnitOfWork(context);
		}

		/// <summary>
		/// Sets the object context.
		/// </summary>
		/// <param name="contextDelegate">The context delegate.</param>
		public static void SetObjectContext(Func<ObjectContext> contextDelegate)
		{
			_contextDelegate = contextDelegate;
		}
	}
}