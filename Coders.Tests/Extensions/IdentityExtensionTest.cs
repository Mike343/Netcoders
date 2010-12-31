using System.Threading;
using Coders.Authentication;
using Coders.Extensions;
using Coders.Tests.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class IdentityExtensionTest
	{
		[TestInitialize]
		public void Initialize()
		{
			var principal = new FakePrivilegePrincipal();

			principal.DetermineRoleActions();

			Thread.CurrentPrincipal = principal;
		}

		[TestMethod]
		public void Test_IsAuthenticated()
		{
			var principal = Thread.CurrentPrincipal;
			var identity = principal.Identity as UserIdentity;

			Assert.IsTrue(identity.IsAuthenticated());
		}

		[TestMethod]
		public void Test_ContainsRole()
		{
			var principal = Thread.CurrentPrincipal as PrivilegePrincipal;

			Assert.IsTrue(principal.ContainsRole("Test"));
		}
	}
}