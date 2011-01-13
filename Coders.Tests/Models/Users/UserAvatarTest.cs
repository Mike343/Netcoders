using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserAvatarTest
	{
		[TestMethod]
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