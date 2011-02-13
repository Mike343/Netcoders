using System.Threading;
using Coders.Authentication;
using Coders.Extensions;
using Coders.Tests.Authentication;
using NUnit.Framework;

namespace Coders.Tests.Extensions
{
	[TestFixture]
	public class IdentityExtensionTest
	{
		[SetUp]
		public void Initialize()
		{
			var principal = new FakePrivilegePrincipal();

			principal.DetermineRolePrivileges();

			Thread.CurrentPrincipal = principal;
		}

		[Test]
		public void Test_IsAuthenticated()
		{
			var principal = Thread.CurrentPrincipal;
			var identity = principal.Identity as UserIdentity;

			Assert.IsTrue(identity.IsAuthenticated());
		}

		[Test]
		public void Test_ContainsRole()
		{
			var principal = Thread.CurrentPrincipal as PrivilegePrincipal;

			Assert.IsTrue(principal.ContainsRole("Test"));
		}

		[Test]
		public void Test_IsSuper()
		{
			var principal = Thread.CurrentPrincipal;

			Assert.IsTrue(principal.IsSuper());
		}
	}
}