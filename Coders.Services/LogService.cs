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
using System.IO;
using System.Xml.Serialization;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Logs;
using Coders.Models.Users;
#endregion

namespace Coders.Services
{
	public class LogService : EntityService<Log>, ILogService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="userService">The user service.</param>
		/// <param name="repository">The repository.</param>
		public LogService(
			IAuthenticationService authenticationService,
			IUserService userService,
			IRepository<Log> repository)
			: base(repository)
		{
			this.AuthenticationService = authenticationService;
			this.UserService = userService;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user service.
		/// </summary>
		/// <value>The user service.</value>
		public IUserService UserService
		{
			get;
			set;
		}

		/// <summary>
		/// Logs the specified action.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <param name="groupKey">The group key.</param>
		/// <param name="action">The action.</param>
		public void Log(int groupId, string groupKey, string action)
		{
			this.Log(groupId, groupKey, action, null);
		}

		/// <summary>
		/// Logs the specified action.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <param name="groupKey">The group key.</param>
		/// <param name="action">The action.</param>
		/// <param name="data">The data.</param>
		public void Log(int groupId, string groupKey, string action, object data)
		{
			var identity = this.AuthenticationService.Identity;

			if (!identity.IsAuthenticated())
			{
				return;
			}

			var log = new Log
			{
				GroupId = groupId,
				GroupKey = groupKey,
				Action = action,
				Created = DateTime.Now,
				User = this.UserService.GetById(identity.Id)
			};

			if (data != null)
			{
				using (var writer = new StringWriter())
				{
					var serializer = new XmlSerializer(data.GetType());

					serializer.Serialize(writer, data);

					log.Data = writer.ToString();
				}
			}

			base.Insert(log);
		}
	}
}