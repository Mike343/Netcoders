using Coders.Authentication;

namespace Coders.Tests.Authentication
{
	public class FakeUserIdentity : UserIdentity
	{
		public FakeUserIdentity()
		{
			base.IsAuthenticated = true;
		}
	}
}