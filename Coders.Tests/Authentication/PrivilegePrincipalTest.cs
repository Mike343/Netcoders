using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Authentication
{
	[TestClass]
	public class PrivilegePrincipalTest
	{
		private FakePrivilegePrincipal _principal;

		[TestInitialize]
		public void Initialize()
		{
			_principal = new FakePrivilegePrincipal();
			_principal.DetermineRoleActions();
		}

		[TestMethod]
		public void Test_PrivilegePrincipal_Identity()
		{
			Assert.AreEqual(_principal.Identity.GetType(), typeof(FakeUserIdentity));
		}

		[TestMethod]
		public void Test_PrivilegePrincipal_Actions()
		{
			Assert.IsTrue(_principal.Actions.ContainsKey("Test"));
		}

		[TestMethod]
		public void Test_PrivilegePrincipal_IsInRole()
		{
			Assert.IsTrue(_principal.IsInRole("Test"));
		}

		[TestMethod]
		public void Test_PrivilegePrincipal_AllowedTo()
		{
			Assert.IsTrue(_principal.AllowedTo("Test", Coders.Authentication.Privileges.Create));
		}
	}
}