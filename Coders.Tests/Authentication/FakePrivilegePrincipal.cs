using Coders.Authentication;
using Coders.Models;

namespace Coders.Tests.Authentication
{
	public class FakePrivilegePrincipal : PrivilegePrincipal
	{
		private const Privileges All = Coders.Authentication.Privileges.View |
			Coders.Authentication.Privileges.ViewAny |
			Coders.Authentication.Privileges.Create |
			Coders.Authentication.Privileges.Update |
			Coders.Authentication.Privileges.UpdateAny |
			Coders.Authentication.Privileges.Delete |
			Coders.Authentication.Privileges.DeleteAny;

		public FakePrivilegePrincipal()
			: base(new FakeUserIdentity())
		{

		}

		public override void DetermineRolePrivileges()
		{
			this.Privileges.Clear();
			this.Privileges.Add(Roles.Audits, new PrivilegesValue(All));
			this.Privileges.Add(Roles.Attachments, new PrivilegesValue(All));
			this.Privileges.Add(Roles.AttachmentsRules, new PrivilegesValue(All));
			this.Privileges.Add(Roles.Countries, new PrivilegesValue(All));
			this.Privileges.Add(Roles.Users, new PrivilegesValue(All));
			this.Privileges.Add(Roles.UsersAvatars, new PrivilegesValue(All));
			this.Privileges.Add(Roles.UsersBans, new PrivilegesValue(All));
			this.Privileges.Add(Roles.Privileges, new PrivilegesValue(All));
			this.Privileges.Add(Roles.UsersSearches, new PrivilegesValue(All));
			this.Privileges.Add(Roles.UsersHostsSearches, new PrivilegesValue(All));
			this.Privileges.Add(Roles.Settings, new PrivilegesValue(All));
			this.Privileges.Add(Roles.TimeZones, new PrivilegesValue(All));
		}
	}
}
