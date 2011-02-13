using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserAvatarTest
	{
		[Test]
		public void Test_UserAvatar_FullPath()
		{
			var avatar = new UserAvatar
			{
				FilePath = "directory", 
				FileDiskName = "test.zip"
			};

			Assert.AreEqual("directory/test.zip", avatar.FullPath);
		}
	}
}