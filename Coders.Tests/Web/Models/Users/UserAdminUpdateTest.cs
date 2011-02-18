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
	public class UserAdminUpdateTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[Test]
		public void Test_UserAdminUpdate()
		{
			var value = new UserAdminUpdate(
				new User
				{
					Id = 1,
					Name = "test",
					Title = "test2",
					EmailAddress = "test@mail.com",
					Signature = "test3",
					IsProtected = true,
					Status = UserStatus.Activated,
					Preference = new UserPreference
					{
						Dst = UserPreferenceDaylightSavingTime.Auto, 
						StartOfWeek = UserPreferenceStartOfWeek.Monday, 
						TimeFormat = UserPreferenceTimeFormat.Basic,
						Country = new Country { Id = 1 },
						TimeZone = new TimeZone { Id = 1 }
					}
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual(1, value.CountryId, "CountryId");
			Assert.AreEqual(1, value.TimeZoneId, "TimeZoneId");
			Assert.AreEqual("test", value.Name, "Name");
			Assert.AreEqual("test2", value.Title, "Title");
			Assert.AreEqual("test@mail.com", value.EmailAddress, "EmailAddress");
			Assert.AreEqual("test@mail.com", value.VerifyEmailAddress, "VerifyEmailAddress");
			Assert.AreEqual("test@mail.com", value.CurrentEmailAddress, "CurrentEmailAddress");
			Assert.AreEqual("test3", value.Signature, "Signature");
			Assert.IsTrue(value.IsProtected, "IsProtected");
			Assert.AreEqual(UserStatus.Activated, value.Status, "Status");
			Assert.AreEqual(UserPreferenceDaylightSavingTime.Auto, value.Dst, "Dst");
			Assert.AreEqual(UserPreferenceStartOfWeek.Monday, value.StartOfWeek, "StartOfWeek");
			Assert.AreEqual(UserPreferenceTimeFormat.Basic, value.TimeFormat, "TimeFormat");
		}

		[Test]
		public void Test_UserAdminUpdate_ValueToModel()
		{
			var value = new UserAdminUpdate
			{
				Name = "test",
				Title = "test2",
				EmailAddress = "test@mail.com",
				Signature = "test3",
				IsProtected = true,
				Status = UserStatus.Activated
			};

			var user = new User();

			value.ValueToModel(user);

			Assert.AreEqual("test", user.Name, "Name");
			Assert.AreEqual("test2", user.Title, "Title");
			Assert.AreEqual("test@mail.com", user.EmailAddress, "EmailAddress");
			Assert.AreEqual("test3", user.Signature, "Signature");
			Assert.IsTrue(user.IsProtected, "IsProtected");
			Assert.AreEqual(UserStatus.Activated, user.Status, "Status");
		}

		[Test]
		public void Test_UserAdminUpdate_ValueToPreference()
		{
			var value = new UserAdminUpdate
			{
				CountryId = 1,
				TimeZoneId = 1,
				Dst = UserPreferenceDaylightSavingTime.Auto,
				StartOfWeek = UserPreferenceStartOfWeek.Monday,
				TimeFormat = UserPreferenceTimeFormat.Basic
			};

			var preference = new UserPreference();

			value.ValueToPreference(preference);

			Assert.AreEqual(UserPreferenceDaylightSavingTime.Auto, preference.Dst, "Dst");
			Assert.AreEqual(UserPreferenceStartOfWeek.Monday, preference.StartOfWeek, "StartOfWeek");
			Assert.AreEqual(UserPreferenceTimeFormat.Basic, preference.TimeFormat, "TimeFormat");
		}

		[Test]
		public void Test_UserAdminUpdate_Validate()
		{
			var value = new UserAdminUpdate();

			value.Validate();

			Assert.AreEqual(4, value.Errors.Count, "Errors");
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