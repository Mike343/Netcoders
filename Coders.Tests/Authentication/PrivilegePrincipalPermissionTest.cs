using System.Threading;
using Coders.Authentication;
using Coders.Extensions;
using Coders.Models;
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
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);

			Assert.AreEqual(Privileges.Create, permission.Privilege, "Privilege");
			Assert.AreEqual(Roles.Privileges, permission.Role, "Role");
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Copy()
		{
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);
			var copyOfPermission = permission.Copy() as PrivilegePrincipalPermission;

			Assert.IsNotNull(copyOfPermission, "NotNull");
			Assert.AreEqual(permission.AuthorizeOnly, copyOfPermission.AuthorizeOnly, "AuthorizeOnly");
			Assert.AreEqual(permission.Privilege, copyOfPermission.Privilege, "Privilege");
			Assert.AreEqual(permission.Role, copyOfPermission.Role, "Role");
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Intersect()
		{
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create | Privileges.Delete);
			var intersect = permission.Intersect(new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Delete)) as PrivilegePrincipalPermission;

			Assert.IsNotNull(intersect, "NotNull");
			Assert.AreEqual(permission.AuthorizeOnly, intersect.AuthorizeOnly, "AuthorizeOnly");
			Assert.AreEqual(Privileges.Delete, intersect.Privilege, "Privilege");
			Assert.AreEqual(permission.Role, intersect.Role, "Role");
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Union()
		{
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);
			var union = permission.Union(new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Delete)) as PrivilegePrincipalPermission;

			Assert.IsNotNull(union, "NotNull");
			Assert.AreEqual(permission.AuthorizeOnly, union.AuthorizeOnly, "AuthorizeOnly");
			Assert.AreEqual(permission.Privilege.Add(Privileges.Delete), union.Privilege, "Privilege");
			Assert.AreEqual(permission.Role, union.Role, "Role");
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_IsSubsetOf_False()
		{
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);
			var subset = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Delete);

			Assert.IsFalse(permission.IsSubsetOf(subset));
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_IsSubsetOf_True()
		{
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);
			var subset = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);

			Assert.IsTrue(permission.IsSubsetOf(subset));
		}

		[Test]
		public void Test_PrivilegePrincipalPermission_Demand()
		{
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);

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
			var permission = new PrivilegePrincipalPermission("Fake", Privileges.Delete);

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
			var permission = new PrivilegePrincipalPermission(Roles.Privileges, Privileges.Create);

			Assert.IsTrue(permission.Authorized());
		}
	}
}