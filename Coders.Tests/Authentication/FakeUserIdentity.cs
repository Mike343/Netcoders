using Coders.Authentication;

namespace Coders.Tests.Authentication
{
	public class FakeUserIdentity : UserIdentity
	{
		public FakeUserIdentity()
		{
			base.Id = 1;
			base.Name = "test";
			base.IsAuthenticated = true;
		}

		public override bool Authenticate(IAuthenticationToken token)
		{
			return true;
		}
	}
}