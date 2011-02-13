using Coders.Models.Users;
using Coders.Models.Users.Enums;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserAuditTest
	{
		[Test]
		public void Test_UserAudit_ValueToAudit()
		{
			var audit = new UserAudit();

			audit.ValueToAudit(new User
			{
				Reputation = 10,
				Name = "test",
				Title = "test2",
				EmailAddress = "test3",
				Signature = "test4",
				Status = UserStatus.Activated
			});

			Assert.AreEqual(10, audit.Reputation, "Reputation");
			Assert.AreEqual("test", audit.Name, "Name");
			Assert.AreEqual("test2", audit.Title, "Title");
			Assert.AreEqual("test3", audit.EmailAddress, "EmailAddress");
			Assert.AreEqual("test4", audit.Signature, "Signature");
			Assert.AreEqual(UserStatus.Activated, audit.Status, "Status");
		}
	}
}