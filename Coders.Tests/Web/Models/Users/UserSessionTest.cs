using System.Collections.Generic;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserSessionTest
	{
		[Test]
		public void Test_UserSession()
		{
			var session = new UserSession(
				new List<UserRoleRelation>()
			);

			Assert.IsNotNull(session.Privileges, "Privileges");
		}
	}
}