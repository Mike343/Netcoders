using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserSearchDeleteTest
	{
		[Test]
		public void Test_UserSearchDelete()
		{
			var value = new UserSearchDelete(
				new UserSearch
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