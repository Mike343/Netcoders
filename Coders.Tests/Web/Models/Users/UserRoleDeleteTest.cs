using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserRoleDeleteTest
	{
		[Test]
		public void Test_UserRoleDelete()
		{
			var value = new UserRoleDelete(
				new UserRole
				{
					Id = 1, 
					Title = "test"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
		}
	}
}