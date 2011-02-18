using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Tests.Web.Fakes;
using Coders.Web.Models.Users;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserAuthenticationUpdateTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[Test]
		public void Test_UserAuthenticationUpdate_Validate()
		{
			var value = new UserAuthenticationUpdate();

			value.Validate();

			Assert.AreEqual(4, value.Errors.Count, "Errors");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IUserService>().To<FakeUserService>();
			}
		}
	}
}