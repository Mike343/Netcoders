using System.Collections.Generic;
using System.Web.Security;
using Coders.Authentication;
using Coders.Models.Users;
using Coders.Web.Authentication;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Authentication
{
	[TestFixture]
	public class WebPrivilegePrincipalTest
	{
		[Test]
		public void Test_WebPrivilegePrincipal_DetermineRolePrivileges()
		{
			var privileges = new List<UserRoleRelation>
			{
				new UserRoleRelation
				{
					Id = 1, 
					Privilege = Privileges.Create, 
					User = new User { Id = 1 }, 
					Role = new UserRole { Id = 1, Title = "test" }
				}
			};

			var identity = new WebUserIdentity(new FormsAuthenticationTicket("Test", false, 1000), true)
			{
			    Session = new UserSession(privileges) { Id = 1, Name = "Test" }
			};

			var principal = new WebPrivilegePrincipal(identity);

			principal.DetermineRolePrivileges();

			Assert.AreEqual(1, principal.Privileges.Count, "Count");
		}
	}
}