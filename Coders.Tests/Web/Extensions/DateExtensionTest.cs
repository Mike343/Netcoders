using System;
using System.Web.Security;
using Coders.Models.Users.Enums;
using Coders.Web.Authentication;
using Coders.Web.Extensions;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class DateExtensionTest
	{
		[Test]
		public void Test_DateExtension_Personalize_NotAuthenticated()
		{
			Assert.AreEqual("Aug 8, 2010 8:08 AM", new DateTime(2010, 8, 8, 8, 8, 0).Personalize(null));
		}

		[Test]
		public void Test_DateExtension_Personalize_Extended()
		{
			var identity = new WebUserIdentity(new FormsAuthenticationTicket("Test", false, 1000), true)
			{
				Session = new UserSession
				{
					Id = 1, 
					TimeZone = "Central Standard Time",
					TimeFormat = UserPreferenceTimeFormat.Extended
				}
			};

			Assert.AreEqual("Aug 8, 2010 8:08 AM", new DateTime(2010, 8, 8, 8, 8, 0).Personalize(identity));
		}

		[Test]
		public void Test_DateExtension_Personalize_Basic()
		{
			var identity = new WebUserIdentity(new FormsAuthenticationTicket("Test", false, 1000), true)
			{
				Session = new UserSession
				{
					Id = 1,
					TimeZone = "Central Standard Time",
					TimeFormat = UserPreferenceTimeFormat.Basic
				}
			};

			Assert.AreEqual("8/8/2010 8:08 AM", new DateTime(2010, 8, 8, 8, 8, 0).Personalize(identity));
		}

		[Test]
		public void Test_DateExtension_Personalize_Adjust()
		{
			var identity = new WebUserIdentity(new FormsAuthenticationTicket("Test", false, 1000), true)
			{
				Session = new UserSession
				{
					Id = 1,
					TimeZone = "Eastern Standard Time"
				}
			};

			Assert.AreEqual("Aug 8, 2010 9:08 AM", new DateTime(2010, 8, 8, 8, 8, 0).Adjust(identity).Personalize(null));
		}
	}
}