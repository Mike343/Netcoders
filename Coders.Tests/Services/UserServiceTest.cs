using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Services;
using Coders.Specifications;
using Coders.Tests.Authentication;
using Coders.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class UserServiceTest
	{
		public IUserService UserService
		{
			get;
			private set;
		}

		public FakeUserPreferenceRepository FakeUserPreferenceRepository
		{
			get; 
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());
			authentication.Stub(x => x.GeneratePassword(0)).Return("1234");

			var preference = new FakeUserPreferenceRepository();

			this.UserService = new UserService(
				MockRepository.GenerateMock<IAuditService<User, UserAudit>>(),
				authentication,
				MockRepository.GenerateMock<ITextFormattingService>(),
				MockRepository.GenerateMock<IUserHostService>(),
				MockRepository.GenerateMock<IUserRoleService>(),
				new FakeUserRepository(),
				preference
			);

			this.FakeUserPreferenceRepository = preference;
		}

		[Test]
		public void Test_UserService_GetPreferenceBy()
		{
			var preference = this.UserService.GetPreferenceBy(new Specification<UserPreference>());

			Assert.AreEqual(1, preference.Id, "Id");
		}

		[Test]
		public void Test_UserService_Create()
		{
			var user= this.UserService.Create();

			Assert.IsNotNull(user, "NotNull");
			Assert.AreEqual(UserStatus.Activated, user.Status, "Status");
			Assert.AreEqual(user.Updated, user.Created, "Updated");
			Assert.AreEqual(user.LastVisit, user.Created, "LastVisit");
			Assert.AreEqual(user.LastLogOn, user.Created, "LastLogOn");
		}

		[Test]
		public void Test_UserService_Insert()
		{
			var user = this.UserService.Create();
			var password = "test";

			user.Name = "Test 2";
			user.Password = password;

			this.UserService.Insert(user);

			Assert.AreEqual("test-2", user.Slug, "Slug");
			Assert.AreEqual(password.HashToSha1(user.Salt).Hex(), user.Password, "Password");
			Assert.IsNotNullOrEmpty(user.Salt, "Salt");
			Assert.AreEqual(UserStatus.Activated, user.Status, "Status");
		}

		[Test]
		public void Test_UserService_Insert_With_Preference()
		{
			var user = this.UserService.Create();
			var preference = new UserPreference();

			this.UserService.Insert(user, preference);

			Assert.IsNotNull(user.Preference, "Preference");
		}

		[Test]
		public void Test_UserService_InsertPreference()
		{
			var preference = new UserPreference();

			this.UserService.InsertPreference(preference);

			Assert.AreEqual(2, preference.Id);
		}

		[Test]
		public void Test_UserService_Update()
		{
			var user = this.UserService.GetById(1);

			user.Name = "Other";

			this.UserService.Update(user);

			Assert.AreEqual("Other", user.Name, "Name");
			Assert.AreEqual("other", user.Slug, "Slug");
		}

		[Test]
		public void Test_UserService_UpdatePreference()
		{
			var user = this.UserService.GetById(1);

			user.Preference.TimeFormat = UserPreferenceTimeFormat.Extended;

			this.UserService.UpdatePreference(user.Preference);

			var result = this.FakeUserPreferenceRepository.GetById(1);

			Assert.AreEqual(UserPreferenceTimeFormat.Extended, result.TimeFormat, "TimeFormat");
		}

		[Test]
		public void Test_UserService_Delete()
		{
			var user = this.UserService.GetById(1);

			this.UserService.Delete(user);

			var count = this.UserService.Count();

			Assert.AreEqual(0, count, "Count");
		}
	}
}