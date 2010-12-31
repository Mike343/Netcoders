using Coders.Authentication;

namespace Coders.Tests.Authentication
{
	public class FakePrivilegePrincipal : PrivilegePrincipal
	{
		public FakePrivilegePrincipal()
			: base(new FakeUserIdentity())
		{

		}

		public override void DetermineRoleActions()
		{
			this.Actions.Clear();
			this.Actions.Add("Test", new PrivilegesValue(Privileges.View | Privileges.Create));
		}
	}
}
