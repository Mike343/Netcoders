using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserAvatarDeleteTest
	{
		[Test]
		public void Test_UserAvatarDelete()
		{
			var value = new UserAvatarDelete(
				new UserAvatar { Id = 1 }
			);

			Assert.AreEqual(1, value.Id, "Id");
		}
	}
}