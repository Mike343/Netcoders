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
using System.Globalization;
using System.IO;
using System.Web;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Models.Users;
#endregion

namespace Coders.Services
{
	public class UserAvatarService : EntityService<UserAvatar>, IUserAvatarService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAvatarService"/> class.
		/// </summary>
		/// <param name="fileService">The file service.</param>
		/// <param name="imageService">The image service.</param>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="repository">The repository.</param>
		public UserAvatarService(
			IFileService fileService,
			IImageService imageService,
			IAuthenticationService authenticationService,
			IRepository<UserAvatar> repository)
			: base(repository)
		{
			this.FileService = fileService;
			this.ImageService = imageService;
			this.AuthenticationService = authenticationService;
		}

		/// <summary>
		/// Gets or sets the file service.
		/// </summary>
		/// <value>The file service.</value>
		public IFileService FileService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the image service.
		/// </summary>
		/// <value>The image service.</value>
		public IImageService ImageService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			private set;
		}

		/// <summary>
		/// Removes the specified avatar from the specified avatar if they match.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="avatar">The avatar.</param>
		public bool RemoveFromUserOnMatch(User user, UserAvatar avatar)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			if (avatar == null)
			{
				throw new ArgumentNullException("avatar");
			}

			if (user.Avatar == null)
			{
				return false;
			}

			if (user.Avatar.Id == avatar.Id)
			{
				user.Avatar = null;
			}

			return true;
		}

		/// <summary>
		/// Assigns the specified avatar to the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="avatar">The avatar.</param>
		public void AssignToUser(User user, UserAvatar avatar)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			if (avatar == null)
			{
				throw new ArgumentNullException("avatar");
			}

			user.Avatar = avatar;
		}

		/// <summary>
		/// Inserts the specified avatar.
		/// </summary>
		/// <param name="avatar">The avatar.</param>
		/// <param name="file">The file.</param>
		public void Insert(UserAvatar avatar, HttpPostedFileBase file)
		{
			if (avatar == null)
			{
				throw new ArgumentNullException("avatar");
			}

			if (file == null)
			{
				throw new ArgumentNullException("file");
			}

			var identity = this.AuthenticationService.Identity;

			if (!identity.IsAuthenticated())
			{
				return;
			}

			var path = GetPath();
			var result = this.ImageService.Save(file, path);

			avatar.UserId = identity.Id;
			avatar.FileName = result.FileName;
			avatar.FileDiskName = result.FileDiskName;
			avatar.FileType = result.FileType;
			avatar.FilePath = result.FilePath;

			this.Insert(avatar);
		}

		/// <summary>
		/// Deletes the specified avatar.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(UserAvatar entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			this.FileService.Delete(entity.FilePath.AsPath());

			base.Delete(entity);
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns></returns>
		private static string GetPath()
		{
			// current date
			var date = DateTime.Now;

			// month/day/year
			var year = date.Year.ToString(CultureInfo.InvariantCulture);
			var month = Path.Combine(year, date.Month.ToString(CultureInfo.InvariantCulture));
			var day = Path.Combine(month, date.Day.ToString(CultureInfo.InvariantCulture));

			// path_to_avatars/month/day/year
			var path = Path.Combine(Setting.UserAvatarPath.Value, day);

			// create directory when needed
			var directory = new DirectoryInfo(path.AsPath());

			if (!directory.Exists)
			{
				directory.Create();
			}

			return path;
		}
	}
}