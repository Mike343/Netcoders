using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserBanDeleteTest
	{
		[Test]
		public void Test_UserBanDelete()
		{
			var value = new UserBanDelete(
				new UserBan
				{
					Id = 1, 
					User = new User()
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.IsNotNull(value.User, "User");
		}
	}
}