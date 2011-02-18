using Coders.Models;
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
	public class UserHostServiceTest
	{
		public IUserHostService UserHostService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			var user = MockRepository.GenerateMock<IRepository<User>>();

			user.Stub(x => x.GetById(1)).Return(new User { Id = 1 });

			this.UserHostService = new UserHostService(
				authentication,
				user,
				new FakeUserHostRepository()
			);
		}

		[Test]
		public void Test_UserHostService_GetAddress()
		{
			var address = this.UserHostService.GetAddress();

			Assert.AreEqual("127.0.0.1", address, "Address");
		}

		[Test]
		public void Test_UserHostService_Capture()
		{
			this.UserHostService.Capture();

			Assert.AreEqual(2, this.UserHostService.Count(), "Captured");

			this.UserHostService.Capture();

			Assert.AreEqual(2, this.UserHostService.Count(), "Not Captured");
		}
	}
}