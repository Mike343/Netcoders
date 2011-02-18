using System.Web.Security;
using Coders.Web.Authentication;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Authentication
{
	[TestFixture]
	public class WebUserIdentityTest
	{
		[Test]
		public void Test_WebUserIdentityTest()
		{
			var identity = new WebUserIdentity(new FormsAuthenticationTicket("Test", false, 1000), true)
			{
				Session = new UserSession { Id = 1, Name = "Test" }
			};

			Assert.AreEqual(1, identity.Id, "Id");
			Assert.AreEqual("Test", identity.Name, "Name");
			Assert.IsTrue(identity.IsAuthenticated, "IsAuthenticated");
			Assert.IsNotNull(identity.Session, "Session");
		}
	}
}