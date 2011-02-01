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
using Ninject;
using Ninject.Parameters;
#endregion

namespace Coders.DependencyResolution
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
			return string.IsNullOrEmpty(key) ? this.Kernel.TryGet<T>() : this.Kernel.TryGet<T>(key, new IParameter[0]);
		}

		protected override IEnumerable<T> TryGetInstances<T>(string key)
		{
			return string.IsNullOrEmpty(key) ? this.Kernel.GetAll<T>() : this.Kernel.GetAll<T>(key, new IParameter[0]);
		}

		protected override object TryGetInstance(Type type, string key)
		{
			return string.IsNullOrEmpty(key) ? this.Kernel.TryGet(type) : this.Kernel.TryGet(type, key, new IParameter[0]);
		}

		protected override void TryRelease(object instance)
		{
			this.Kernel.Release(instance);
		}
	}
}