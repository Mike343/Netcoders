using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Parameters;

namespace Coders.Tests
{
	public class NinjectServiceLocator : ServiceLocatorImplBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NinjectServiceLocator"/> class.
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		public NinjectServiceLocator(IKernel kernel)
		{
			this.Kernel = kernel;
		}

		/// <summary>
		/// Gets or sets the kernel.
		/// </summary>
		/// <value>The kernel.</value>
		public IKernel Kernel
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		protected override object DoGetInstance(Type serviceType, string key)
		{
			return string.IsNullOrEmpty(key) ? this.Kernel.TryGet(serviceType) : this.Kernel.TryGet(serviceType, key, new IParameter[0]);
		}

		/// <summary>
		/// Gets all the instances.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			return this.Kernel.GetAll(serviceType);
		}
	}
}