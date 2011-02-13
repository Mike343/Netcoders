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
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Parameters;
#endregion

namespace Coders.DependencyResolution
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