using Coders.Users.Web;
using NUnit.Framework;

namespace Coders.Users.Tests.Web
{
	[TestFixture]
	public class UserInstallerTest
	{
		[Test]
		public void Test_UserInstaller()
		{
			var installer = new UserInstaller();

			Assert.AreEqual("Users", installer.Name, "Name");
			Assert.AreEqual("1.0", installer.Version, "Version");
			Assert.IsTrue(installer.IsActive, "IsActive");
		}
	}
}