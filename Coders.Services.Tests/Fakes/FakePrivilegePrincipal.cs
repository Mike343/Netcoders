﻿using Coders.Authentication;

namespace Coders.Services.Tests.Fakes
{
	public class FakePrivilegePrincipal : PrivilegePrincipal
	{
		public FakePrivilegePrincipal()
			: base(new FakeUserIdentity())
		{

		}

		public override void DetermineRolePrivileges()
		{
			this.Privileges.Clear();
			this.Privileges.Add("Test", new PrivilegesValue(Coders.Authentication.Privileges.View | Coders.Authentication.Privileges.Create));
		}
	}
}