using Coders.Authentication;
using NUnit.Framework;

namespace Coders.Tests.Authentication
{
	[TestFixture]
	public class PrivilegesValueTest
	{
		private PrivilegesValue _action;

		[SetUp]
		public void Initialize()
		{
			_action = new PrivilegesValue(Privileges.Create);
		}

		[Test]
		public void Test_PrivilegesValue_Permission()
		{
			Assert.AreEqual(_action.Permission, Privileges.Create);
		}

		[Test]
		public void Test_PrivilegesValue_AllowedTo_True()
		{
			Assert.IsTrue(_action.AllowedTo(Privileges.Create));
		}

		[Test]
		public void Test_PrivilegesValue_AllowedTo_False()
		{
			Assert.IsFalse(_action.AllowedTo(Privileges.Delete));
		}
	}
}