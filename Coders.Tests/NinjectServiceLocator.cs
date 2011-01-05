using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Parameters;

namespace Coders.Tests
{
	public class NinjectServiceLocator : ServiceLocatorBase
	{
		public NinjectServiceLocator(IKernel kernel)
		{
			this.Kernel = kernel;
		}

		public IKernel Kernel
		{
			get; 
			private set;
		}

		protected override T TryGetInstance<T>(string key)
		{
			return string.IsNullOrEmpty(key) ? this.Kernel.Get<T>() : this.Kernel.Get<T>(key, new IParameter[0]);
		}

		protected override object TryGetInstance(Type type, string key)
		{
			return string.IsNullOrEmpty(key) ? this.Kernel.Get(type) : this.Kernel.Get(type, key, new IParameter[0]);
		}

		protected override IEnumerable<T> TryGetInstances<T>(string key)
		{
			return string.IsNullOrEmpty(key) ? this.Kernel.GetAll<T>() : this.Kernel.GetAll<T>(key, new IParameter[0]);
		}

		protected override void TryRelease(object instance)
		{
			this.Kernel.Release(instance);
		}
	}
}