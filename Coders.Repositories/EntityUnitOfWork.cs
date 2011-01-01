using System;
using System.Data.Objects;
using Coders.Models;

namespace Coders.Repositories
{
	public class EntityUnitOfWork : IUnitOfWork, IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityUnitOfWork"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public EntityUnitOfWork(ObjectContext context)
		{
			this.Context = context;
			this.Context.ContextOptions.LazyLoadingEnabled = true;
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		/// <value>The context.</value>
		public ObjectContext Context
		{
			get;
			private set;
		}

		/// <summary>
		/// Commits this instance.
		/// </summary>
		public void Commit()
		{
			Context.SaveChanges();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			if (this.Context != null)
			{
				this.Context.Dispose();
			}
		}
	}
}