using System;
using System.Web;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Services.Tests.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class UserAvatarServiceTest
	{
		private string _path;

		public HttpPostedFileBase Source
		{
			get; 
			private set;
		}

		public IUserAvatarService UserAvatarService
		{
			get; 
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var source = MockRepository.GenerateMock<HttpPostedFileBase>();

			source.Stub(x => x.ContentLength).Return(12345678);

			this.Source = source;

			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			var date = DateTime.Now;

			_path = "{0}\\{1}\\{2}".FormatInvariant(date.Year, date.Month, date.Day);

			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.Save(this.Source, _path)).Return(new ImageResult
			{
				FileName = "test.jpg",
				FileDiskName = "test2.jpg",
				FileType = "test3",
				FileExtension = "jpg",
				FilePath = _path
			});

			this.UserAvatarService = new UserAvatarService(
				MockRepository.GenerateMock<IFileService>(), 
				image, 
				authentication,
				MockRepository.GenerateMock<IRepository<UserAvatar>>()
			);
		}

		[Test]
		public void Test_UserAvatarServiceTest_Assign()
		{
			var user = new User();

			this.UserAvatarService.AssignToUser(user, new UserAvatar { Id = 1, FileName = "test" });

			Assert.AreEqual(1, user.Avatar.Id, "Id");
			Assert.AreEqual("test", user.Avatar.FileName, "FileName");
		}

		[Test]
		public void Test_UserAvatarServiceTest_RemoveFromUserOnMatch()
		{
			var user = new User();
			var avatar = new UserAvatar { Id = 1, FileName = "test" };

			user.Avatar = avatar;

			var matched = this.UserAvatarService.RemoveFromUserOnMatch(user, avatar);

			Assert.IsTrue(matched, "Matched");
			Assert.IsNull(user.Avatar, "Avatar");
		}

		[Test]
		public void Test_UserAvatarServiceTest_Insert()
		{
			var avatar = new UserAvatar();

			this.UserAvatarService.Insert(avatar, this.Source);

			Assert.AreEqual(1, avatar.UserId, "UserId");
			Assert.AreEqual("test.jpg", avatar.FileName, "FileName");
			Assert.AreEqual("test2.jpg", avatar.FileDiskName, "FileDiskName");
			Assert.AreEqual("test3", avatar.FileType, "FileType");
			Assert.AreEqual(_path, avatar.FilePath, "FilePath");
		}
	}
}