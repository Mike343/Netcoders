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
using Ninject.Modules;
#endregion

namespace Coders.Web
{
	public class CodersControllers : NinjectModule
	{
		/// <summary>
		/// Loads this instance.
		/// </summary>
		public override void Load()
		{
			Bind<Controllers.Administration.HomeController>().ToSelf().InTransientScope();
			Bind<Controllers.Administration.SettingController>().ToSelf().InTransientScope();
			Bind<Controllers.Users.AuthController>().ToSelf().InTransientScope();
			Bind<Controllers.Users.HomeController>().ToSelf().InTransientScope();
			Bind<Controllers.AttachmentController>().ToSelf().InTransientScope();
			Bind<Controllers.HomeController>().ToSelf().InTransientScope();
		}
	}
}