using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserRoleAuditTest
	{
		[Test]
		public void Test_UserRoleAudit_ValueToAudit()
		{
			var audit = new UserRoleAudit();

			audit.ValueToAudit(new UserRole
			{
			    Title = "test", 
				IsDefault = false, 
				IsAdministrator = true
			});

			Assert.AreEqual("test", audit.Title, "Title");
			Assert.AreEqual(false, audit.IsDefault, "IsDefault");
			Assert.AreEqual(true, audit.IsAdministrator, "IsAdministrator");
		}
	}
}