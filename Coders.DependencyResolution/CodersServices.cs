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
using Coders.Models.Attachments;
using Coders.Models.Common;
using Coders.Models.Countries;
using Coders.Models.Settings;
using Coders.Models.TimeZones;
using Coders.Models.Users;
using Coders.Services;
using Ninject.Modules;
#endregion

namespace Coders.DependencyResolution
{
	public class CodersServices : NinjectModule
	{
		/// <summary>
		/// Loads this instance.
		/// </summary>
		public override void Load()
		{
			Bind(typeof(IAuditService<,>)).To(typeof(AuditService<,>));
			Bind<IAttachmentRuleService>().To<AttachmentRuleService>();
			Bind<IAttachmentService>().To<AttachmentService>();
			Bind<ICountryService>().To<CountryService>();
			Bind<IEmailService>().To<EmailService>();
			Bind<IFileService>().To<FileService>();
			Bind<IAuthenticationService>().To<FormsAuthenticationService>();
			Bind<IImageService>().To<ImageService>();
			Bind<ISettingService>().To<SettingService>();
			Bind<ITextFormattingService>().To<TextFormattingService>();
			Bind<ITimeZoneService>().To<TimeZoneService>();
			Bind<IUserAvatarService>().To<UserAvatarService>();
			Bind<IUserBanService>().To<UserBanService>();
			Bind<IUserHostService>().To<UserHostService>();
			Bind<IUserService>().To<UserService>();
			Bind<IUserRoleService>().To<UserRoleService>();
			Bind<IUserSearchService>().To<UserSearchService>();
			Bind<IUserHostSearchService>().To<UserHostSearchService>();
		}
	}
}