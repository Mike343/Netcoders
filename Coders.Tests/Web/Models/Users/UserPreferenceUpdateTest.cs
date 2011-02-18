using Coders.Models.Countries;
using Coders.Models.TimeZones;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserPreferenceUpdateTest
	{
		[Test]
		public void Test_UserPreferenceUpdate()
		{
			var value = new UserPreferenceUpdate(
				new UserPreference
				{
					Dst = UserPreferenceDaylightSavingTime.Auto,
					StartOfWeek = UserPreferenceStartOfWeek.Monday,
					TimeFormat = UserPreferenceTimeFormat.Basic,
					Country = new Country { Id = 1},
					TimeZone = new TimeZone { Id = 1 }
				}
			);

			value.Initialize();

			Assert.AreEqual(1, value.CountryId, "CountryId");
			Assert.AreEqual(1, value.TimeZoneId, "TimeZoneId");
			Assert.AreEqual(UserPreferenceDaylightSavingTime.Auto, value.Dst, "Dst");
			Assert.AreEqual(UserPreferenceStartOfWeek.Monday, value.StartOfWeek, "StartOfWeek");
			Assert.AreEqual(UserPreferenceTimeFormat.Basic, value.TimeFormat, "TimeFormat");
		}

		[Test]
		public void Test_UserPreferenceUpdate_ValueToModel()
		{
			var value = new UserPreferenceUpdate
			{
				Dst = UserPreferenceDaylightSavingTime.Auto,
				StartOfWeek = UserPreferenceStartOfWeek.Monday,
				TimeFormat = UserPreferenceTimeFormat.Basic
			};

			value.Initialize();

			var preference = new UserPreference();

			value.ValueToModel(preference);

			Assert.AreEqual(UserPreferenceDaylightSavingTime.Auto, preference.Dst, "Dst");
			Assert.AreEqual(UserPreferenceStartOfWeek.Monday, preference.StartOfWeek, "StartOfWeek");
			Assert.AreEqual(UserPreferenceTimeFormat.Basic, preference.TimeFormat, "TimeFormat");
		}

		[Test]
		public void Test_UserPreferenceUpdate_Validate()
		{
			var value = new UserPreferenceUpdate();

			value.Validate();

			Assert.AreEqual(0, value.Errors.Count, "Errors");
		}
	}
}