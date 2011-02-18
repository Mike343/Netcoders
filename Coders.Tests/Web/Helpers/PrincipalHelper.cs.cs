using System.Threading;
using Coders.Tests.Authentication;

namespace Coders.Tests.Web.Helpers
{
	public class PrincipalHelper
	{
		/// <summary>
		/// Creates this instance.
		/// </summary>
		public static void Create()
		{
			var principal = new FakePrivilegePrincipal();

			principal.DetermineRolePrivileges();

			Thread.CurrentPrincipal = principal;
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public static void Clear()
		{
			Thread.CurrentPrincipal = null;
		}
	}
}