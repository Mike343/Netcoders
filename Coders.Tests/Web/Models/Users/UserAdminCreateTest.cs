using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Countries;
using Coders.Models.TimeZones;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Tests.Web.Fakes;
using Coders.Web.Models.Users;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserAdminCreateTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[Test]
		public void Test_UserAdminCreate_ValueToModel()
		{
			var value = new UserAdminCreate
			{
				CountryId = 1,
				TimeZoneId = 2,
				Name = "test",
				Title = "test2",
				EmailAddress = "test@mail.com",
				Password = "testing",
				Signature = "test5",
				IsProtected = true,
				Status = UserStatus.Activated,
				Dst = UserPreferenceDaylightSavingTime.On,
				StartOfWeek = UserPreferenceStartOfWeek.Monday,
				TimeFormat = UserPreferenceTimeFormat.Extended
			};

			var user = new User();

			value.ValueToModel(user);

			Assert.AreEqual("test", user.Name, "Name");
			Assert.AreEqual("test2", user.Title, "Title");
			Assert.AreEqual("test@mail.com", user.EmailAddress, "EmailAddress");
			Assert.AreEqual("testing", user.Password, "Password");
			Assert.AreEqual("test5", user.Signature, "Signature");
			Assert.IsTrue(user.IsProtected, "IsProtected");
			Assert.AreEqual(UserStatus.Activated, user.Status, "Status");
			Assert.AreEqual(UserPreferenceDaylightSavingTime.On, value.Preference.Dst, "Dst");
			Assert.AreEqual(UserPreferenceStartOfWeek.Monday, value.Preference.StartOfWeek, "StartOfWeek");
			Assert.AreEqual(UserPreferenceTimeFormat.Extended, value.Preference.TimeFormat, "TimeFormat");
		}

		[Test]
		public void Test_UserAdminCreate_Validate()
		{
			var value = new UserAdminCreate();

			value.Validate();

			Assert.AreEqual(6, value.Errors.Count, "Errors");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IUserService>().To<FakeUserService>();
				Bind<ICountryService>().To<FakeCountryService>();
				Bind<ITimeZoneService>().To<FakeTimeZoneService>();
			}
		}
	}
}