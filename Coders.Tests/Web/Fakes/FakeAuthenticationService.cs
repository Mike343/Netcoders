using System;
using Coders.Authentication;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Tests.Authentication;

namespace Coders.Tests.Web.Fakes
{
	public class FakeAuthenticationService : IAuthenticationService
	{
		public string ReturnUrl
		{
			get { throw new NotImplementedException(); }
		}

		public string LogOnUrl
		{
			get { throw new NotImplementedException(); }
		}

		public string GeneratePassword()
		{
			throw new NotImplementedException();
		}

		public string GeneratePassword(int length)
		{
			throw new NotImplementedException();
		}

		public bool Authenticate(User user, string password)
		{
			return true;
		}

		public PrivilegePrincipal Principal
		{
			get { throw new NotImplementedException(); }
		}

		public UserIdentity Identity
		{
			get
			{
				return new FakeUserIdentity();
			}
		}

		public void LogOn(User user)
		{

		}

		public void LogOff()
		{

		}

		public void Reset(User user)
		{

		}

		public void Update(User user, string password)
		{

		}
	}
}