using System.Threading;
using Coders.Authentication;
using Coders.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Authentication
{
	[TestFixture]
	public class PrivilegePrincipalPermissionTest
	{
		[SetUp]
		public void Initialize()
		{
			Thread.CurrentPrincipal = new FakePrivilegePrincipal();
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Init()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);

			Assert.IsFalse(permission.AuthorizeOnly);
			Assert.AreEqual(Privileges.Create, permission.Privilege);
			Assert.AreEqual("Test", permission.Role);
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Copy()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var copyOfPermission = permission.Copy() as PrivilegePrincipalPermission;

			Assert.AreEqual(permission.AuthorizeOnly, copyOfPermission.AuthorizeOnly);
			Assert.AreEqual(permission.Privilege, copyOfPermission.Privilege);
			Assert.AreEqual(permission.Role, copyOfPermission.Role);
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Intersect()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create | Privileges.Delete);
			var intersect = permission.Intersect(new PrivilegePrincipalPermission("Test", Privileges.Delete)) as PrivilegePrincipalPermission;

			Assert.AreEqual(permission.AuthorizeOnly, intersect.AuthorizeOnly);
			Assert.AreEqual(Privileges.Delete, intersect.Privilege);
			Assert.AreEqual(permission.Role, intersect.Role);
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Union()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var intersect = permission.Union(new PrivilegePrincipalPermission("Test", Privileges.Delete)) as PrivilegePrincipalPermission;

			Assert.AreEqual(permission.AuthorizeOnly, intersect.AuthorizeOnly);
			Assert.AreEqual(permission.Privilege.Add(Privileges.Delete), intersect.Privilege);
			Assert.AreEqual(permission.Role, intersect.Role);
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_IsSubsetOf_False()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var subset = new PrivilegePrincipalPermission("Test", Privileges.Delete);

			Assert.IsFalse(permission.IsSubsetOf(subset));
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_IsSubsetOf_True()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var subset = new PrivilegePrincipalPermission("Test", Privileges.Create);

			Assert.IsTrue(permission.IsSubsetOf(subset));
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Demand()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);

			try
			{
				permission.Demand();
			}
			catch
			{
				Assert.Fail();
			}
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Demand_Fail()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Delete);

			try
			{
				permission.Demand();
			}
			catch
			{
				return;
			}

			Assert.Fail();
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Authorized()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);

			Assert.IsTrue(permission.Authorized());
		}
	}
}