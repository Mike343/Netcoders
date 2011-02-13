using Coders.Authentication;

namespace Coders.Services.Tests.Fakes
{
	public class FakeUserIdentity : UserIdentity
	{
		public FakeUserIdentity()
		{
			base.Id = 1;
			base.Name = "Test";
			base.IsAuthenticated = true;
		}

		public override bool Authenticate(IAuthenticationToken token)
		{
			return true;
		}
	}
}