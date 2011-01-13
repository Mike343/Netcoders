using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserTest
	{
		[TestMethod]
		public void Test_User_UpdateReputation()
		{
			var user = new User();

			user.UpdateReputation();

			Assert.AreEqual(1, user.Reputation);

			user.UpdateReputation(1, false);

			Assert.AreEqual(0, user.Reputation);
		}
	}
}