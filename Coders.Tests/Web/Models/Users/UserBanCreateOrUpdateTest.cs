using System;
using System.Globalization;
using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Tests.Web.Fakes;
using Coders.Web.Models.Users;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserBanCreateOrUpdateTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[Test]
		public void Test_UserBanCreateOrUpdate()
		{
			var date = DateTime.Now;

			var value = new UserBanCreateOrUpdate(
				new UserBan
				{
					Id = 1,
					Reason = "test",
					IsPermanent = true,
					Expire = DateTime.Now,
					User = new User()
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Reason, "Reason");
			Assert.IsTrue(value.IsPermanent, "IsPermanent");
			Assert.AreEqual(date.ToString("M/d/yyyy", CultureInfo.InvariantCulture), value.Expire, "Expire");
			Assert.IsNotNull(value.User, "User");
		}

		[Test]
		public void Test_UserBanCreateOrUpdate_Initialize()
		{
			var value = new UserBanCreateOrUpdate();

			value.Initialize(new User());

			Assert.IsNotNull(value.User, "User");
		}

		[Test]
		public void Test_UserBanCreateOrUpdate_ValueToModel()
		{
			var date = DateTime.Now;

			var value = new UserBanCreateOrUpdate
			{
				Reason = "test",
				Expire = date.ToString("M/d/yyyy", CultureInfo.InvariantCulture),
			};

			var ban = new UserBan();

			value.ValueToModel(ban);

			Assert.AreEqual("test", ban.Reason, "Reason");
			Assert.IsNotNull(ban.Expire, "Expire NotNull");
			Assert.AreEqual(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0), ban.Expire.Value, "Expire");
		}

		[Test]
		public void Test_UserBanCreateOrUpdate_Validate()
		{
			var value = new UserBanCreateOrUpdate();

			value.Validate();

			Assert.AreEqual(3, value.Errors.Count, "Errors");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IUserService>().To<FakeUserService>();
			}
		}
	}
}