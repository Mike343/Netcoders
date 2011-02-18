using System;
using System.Collections.Generic;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Services;
using Coders.Tests.Authentication;
using Coders.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class UserSearchServiceTest
	{
		public IUserSearchService UserSearchService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var settings = new List<Setting>
			{
				new Setting {Key = "user.search.index.path", Value = "users".AsPath()},
				new Setting {Key = "user.search.index.created", Value = false.ToString()},
				new Setting {Key = "user.search.index.updated", Value = DateTime.MinValue.ToString()}
			};

			Setting.Rebuild(settings);

			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			this.UserSearchService = new UserSearchService(
				authentication,
				MockRepository.GenerateMock<IUserService>(),
				MockRepository.GenerateMock<ISettingService>(),
				new FakeUserSearchRepository()
			);
		}

		[Test]
		public void Test_UserSearchService_Create()
		{
			var search = this.UserSearchService.Create();

			Assert.IsNotNull(search, "NotNull");
			Assert.AreEqual(search.Created, search.Updated, "Date");
		}

		[Test]
		public void Test_UserSearchService_Insert()
		{
			var search = new UserSearch
			{
				Title = "test2"
			};

			this.UserSearchService.Insert(search);

			Assert.AreEqual(2, search.Id, "Id");
			Assert.AreEqual(1, search.UserId, "UserId");
			Assert.AreEqual("test2", search.Title, "Title");
		}

		[Test]
		public void Test_UserSearchService_DeleteExpired()
		{
			this.UserSearchService.DeleteExpired();

			var count = this.UserSearchService.Count();

			Assert.AreEqual(0, count, "Count");
		}
	}
}