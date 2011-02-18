using Coders.Models;
using NUnit.Framework;

namespace Coders.Tests.Authentication
{
	[TestFixture]
	public class PrivilegePrincipalTest
	{
		private FakePrivilegePrincipal _principal;

		[SetUp]
		public void Initialize()
		{
			_principal = new FakePrivilegePrincipal();
			_principal.DetermineRolePrivileges();
		}

		[Test]
		public void Test_PrivilegePrincipal_Identity()
		{
			Assert.AreEqual(_principal.Identity.GetType(), typeof(FakeUserIdentity));
		}

		[Test]
		public void Test_PrivilegePrincipal_Privileges()
		{
			Assert.IsTrue(_principal.Privileges.ContainsKey(Roles.Privileges));
		}

		[Test]
		public void Test_PrivilegePrincipal_IsInRole()
		{
			Assert.IsTrue(_principal.IsInRole(Roles.Privileges));
		}

		[Test]
		public void Test_PrivilegePrincipal_AllowedTo()
		{
			Assert.IsTrue(_principal.AllowedTo(Roles.Privileges, Coders.Authentication.Privileges.Create));
		}
	}
}