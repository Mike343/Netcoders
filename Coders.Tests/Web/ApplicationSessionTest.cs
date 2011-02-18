using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Specifications;
using Coders.Tests.Authentication;
using Coders.Tests.Web.Fakes;
using Coders.Web;
using Coders.Web.Authentication;
using NUnit.Framework;
using Rhino.Mocks;
using TimeZone = Coders.Models.TimeZones.TimeZone;

namespace Coders.Tests.Web
{
	[TestFixture]
	public class ApplicationSessionTest
	{
		[Test]
		public void Test_ApplicationSession_Create()
		{
			var userHostService = MockRepository.GenerateMock<IUserHostService>();

			userHostService.Stub(x => x.GetAddress()).Return("127.0.0.1");

			var session = new ApplicationSession(
				new FakeUserRoleService(),
				userHostService,
				new FakeUserRepository()
			);

			var httpContext = MockRepository.GenerateMock<HttpContextBase>();
			var httpSession = MockRepository.GenerateMock<HttpSessionStateBase>();

			httpContext.Stub(x => x.Session).Return(httpSession);

			var principal = new FakePrivilegePrincipal();
			var identity = new WebUserIdentity(
				new FormsAuthenticationTicket(0, "test@mail.com", DateTime.Now, DateTime.Now.AddDays(1), true, string.Empty), 
				true);

			session.Create(httpContext, principal, identity);

			var result = identity.Session;

			Assert.IsNotNull(result, "UserSession");
			Assert.AreEqual(1, result.Id, "Id");
			Assert.AreEqual("test", result.Name, "Name");
			Assert.AreEqual("test2", result.Slug, "Slug");
			Assert.AreEqual("test3", result.TimeZone, "TimeZone");
			Assert.AreEqual(UserPreferenceDaylightSavingTime.On, result.Dst, "Dst");
			Assert.AreEqual(UserPreferenceTimeFormat.Basic, result.TimeFormat, "TimeFormat");
		}

		public class FakeUserRepository : IRepository<User>
		{
			private readonly User _user;

			public FakeUserRepository()
			{
				_user = new User
				{
					Id = 1,
					Name = "test",
					Slug = "test2",
					Preference = new UserPreference
					{
						Dst = UserPreferenceDaylightSavingTime.On,
						TimeFormat = UserPreferenceTimeFormat.Basic,
						TimeZone = new TimeZone { Title = "test3" }
					}
				};
			}

			public User GetBy(ISpecification<User> specification)
			{
				return _user;
			}

			public User GetById(int id)
			{
				return _user;
			}

			public IList<User> GetAll()
			{
				return new List<User> { _user };
			}

			public IList<User> GetAll(ISpecification<User> specification)
			{
				return new List<User> { _user };
			}

			public IPagedCollection<User> GetPaged(ISpecification<User> specification)
			{
				return new PagedCollection<User>(new List<User> { _user }, 1, 1, 1);
			}

			public int Count()
			{
				return 1;
			}

			public int Count(ISpecification<User> specification)
			{
				return 1;
			}

			public void Insert(User entity)
			{

			}

			public void Update(User entity)
			{

			}

			public void Delete(User entity)
			{

			}
		}
	}
}