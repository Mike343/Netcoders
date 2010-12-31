using System.Threading;
using Coders.Authentication;
using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Authentication
{
	[TestClass]
	public class PrivilegePrincipalPermissionTest
	{
		[TestInitialize]
		public void Initialize()
		{
			Thread.CurrentPrincipal = new FakePrivilegePrincipal();
		}

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_Init()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);

			Assert.IsFalse(permission.AuthorizeOnly);
			Assert.AreEqual(Privileges.Create, permission.Action);
			Assert.AreEqual("Test", permission.Role);
		}

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_Copy()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var copyOfPermission = permission.Copy() as PrivilegePrincipalPermission;

			Assert.AreEqual(permission.AuthorizeOnly, copyOfPermission.AuthorizeOnly);
			Assert.AreEqual(permission.Action, copyOfPermission.Action);
			Assert.AreEqual(permission.Role, copyOfPermission.Role);
		}

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_Intersect()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create | Privileges.Delete);
			var intersect = permission.Intersect(new PrivilegePrincipalPermission("Test", Privileges.Delete)) as PrivilegePrincipalPermission;

			Assert.AreEqual(permission.AuthorizeOnly, intersect.AuthorizeOnly);
			Assert.AreEqual(Privileges.Delete, intersect.Action);
			Assert.AreEqual(permission.Role, intersect.Role);
		}

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_Union()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var intersect = permission.Union(new PrivilegePrincipalPermission("Test", Privileges.Delete)) as PrivilegePrincipalPermission;

			Assert.AreEqual(permission.AuthorizeOnly, intersect.AuthorizeOnly);
			Assert.AreEqual(permission.Action.Add(Privileges.Delete), intersect.Action);
			Assert.AreEqual(permission.Role, intersect.Role);
		}

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_IsSubsetOf_False()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var subset = new PrivilegePrincipalPermission("Test", Privileges.Delete);

			Assert.IsFalse(permission.IsSubsetOf(subset));
		}

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_IsSubsetOf_True()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);
			var subset = new PrivilegePrincipalPermission("Test", Privileges.Create);

			Assert.IsTrue(permission.IsSubsetOf(subset));
		}

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
		public void Test_PrivilegePrincipalPermission_Authorized()
		{
			var permission = new PrivilegePrincipalPermission("Test", Privileges.Create);

			Assert.IsTrue(permission.Authorized());
		}
	}
}