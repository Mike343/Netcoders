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
	public class UserUpdateTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[Test]
		public void Test_UserUpdate()
		{
			var value = new UserUpdate(
				new User
				{
					Name = "test",
					Title = "test2",
					EmailAddress = "testing@mail.com",
					Signature = "test3"
				}
			);

			Assert.AreEqual("test", value.Name, "Name");
			Assert.AreEqual("test", value.CurrentName, "CurrentName");
			Assert.AreEqual("test2", value.Title, "Title");
			Assert.AreEqual("testing@mail.com", value.EmailAddress, "EmailAddress");
			Assert.AreEqual("testing@mail.com", value.VerifyEmailAddress, "VerifyEmailAddress");
			Assert.AreEqual("testing@mail.com", value.CurrentEmailAddress, "CurrentEmailAddress");
			Assert.AreEqual("test3", value.Signature, "Signature");
		}

		[Test]
		public void Test_UserUpdate_ValueToModel()
		{
			var value = new UserUpdate
			{
				Name = "test",
				Title = "test2",
				EmailAddress = "testing@mail.com",
				Signature = "test3"
			};

			var user = new User();

			value.ValueToModel(user);

			Assert.AreEqual("test", user.Name, "Name");
			Assert.AreEqual("test2", user.Title, "Title");
			Assert.AreEqual("testing@mail.com", user.EmailAddress, "EmailAddress");
			Assert.AreEqual("test3", user.Signature, "Signature");
		}

		[Test]
		public void Test_UserUpdate_Validate()
		{
			var value = new UserUpdate();

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