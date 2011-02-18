using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserTest
	{
		[Test]
		public void Test_User_UpdateReputation()
		{
			var user = new User();

			user.UpdateReputation();

			Assert.AreEqual(1, user.Reputation, "Add");

			user.UpdateReputation(1, false);

			Assert.AreEqual(0, user.Reputation, "Subtract");
		}
	}
}