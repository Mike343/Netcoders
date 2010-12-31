using Coders.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Authentication
{
	[TestClass]
	public class PrivilegesValueTest
	{
		private PrivilegesValue _action;

		[TestInitialize]
		public void Initialize()
		{
			_action = new PrivilegesValue(Privileges.Create);
		}

		[TestMethod]
		public void Test_PrivilegesValue_Permission()
		{
			Assert.AreEqual(_action.Permission, Privileges.Create);
		}

		[TestMethod]
		public void Test_PrivilegesValue_AllowedTo_True()
		{
			Assert.IsTrue(_action.AllowedTo(Privileges.Create));
		}

		[TestMethod]
		public void Test_PrivilegesValue_AllowedTo_False()
		{
			var action = new PrivilegesValue(Privileges.Create);

			Assert.IsFalse(_action.AllowedTo(Privileges.Delete));
		}
	}
}