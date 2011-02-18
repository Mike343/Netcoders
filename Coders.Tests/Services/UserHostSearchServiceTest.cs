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
	public class UserHostSearchServiceTest
	{
		public IUserHostSearchService UserHostSearchService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			this.UserHostSearchService = new UserHostSearchService(
				authentication,
				MockRepository.GenerateMock<IUserHostService>(),
				new FakeUserHostSearchRepository()
			);
		}

		[Test]
		public void Test_UserHostSearchService_Create()
		{
			var search = this.UserHostSearchService.Create();

			Assert.IsNotNull(search, "NotNull");
			Assert.AreEqual(search.Created, search.Updated, "Date");
		}

		[Test]
		public void Test_UserHostSearchService_Insert()
		{
			var search = new UserHostSearch
			{
				Title = "test2"
			};

			this.UserHostSearchService.Insert(search);

			Assert.AreEqual(2, search.Id, "Id");
			Assert.AreEqual(1, search.UserId, "UserId");
			Assert.AreEqual("test2", search.Title, "Title");
		}

		[Test]
		public void Test_UserHostSearchService_DeleteExpired()
		{
			this.UserHostSearchService.DeleteExpired();

			var count = this.UserHostSearchService.Count();

			Assert.AreEqual(0, count, "Count");
		}
	}
}