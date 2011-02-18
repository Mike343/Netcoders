using System;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Services;
using Coders.Tests.Authentication;
using Coders.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class UserBanServiceTest
	{
		public IUserBanService UserBanService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			this.UserBanService = new UserBanService(
				authentication,
				MockRepository.GenerateMock<IUserService>(),
				new FakeUserBanRepository()
			);
		}

		[Test]
		public void Test_UserBanService_Create()
		{
			var ban = this.UserBanService.Create();

			Assert.IsNotNull(ban, "NotNull");
			Assert.AreNotEqual(DateTime.MinValue, ban.Created, "Created");
		}

		[Test]
		public void Test_UserBanService_Check()
		{
			var ban = this.UserBanService.Check();

			Assert.IsNotNull(ban, "NotNull");
			Assert.AreEqual("test", ban.Reason, "Reason");
			Assert.AreEqual(1, ban.User.Id, "User.Id");
		}

		[Test]
		public void Test_UserBanService_InsertOrUpdate()
		{
			var service = this.UserBanService;
			var ban = new UserBan { Reason = "test2" };

			service.InsertOrUpdate(ban);

			Assert.AreEqual(2, ban.Id, "Id");
			Assert.AreEqual("test2", ban.Reason, "Reason");
		}

		[Test]
		public void Test_UserBanService_InsertOrUpdate_Update()
		{
			var service = this.UserBanService;
			var ban = service.GetById(1);

			ban.Reason = "test3";

			service.InsertOrUpdate(ban);

			Assert.AreEqual(1, ban.Id, "Update Id");
			Assert.AreEqual("test3", ban.Reason, "Update Reason");
		}

		[Test]
		public void Test_UserBanService_DeleteExpired()
		{
			this.UserBanService.DeleteExpired();

			var count = this.UserBanService.Count();

			Assert.AreEqual(0, count, "Count");
		}
	}
}