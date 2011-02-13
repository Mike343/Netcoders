using System.Collections.Generic;
using System.Threading;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Services.Tests.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class FormsAuthenticationServiceTest
	{
		public IAuthenticationService AuthenticationService
		{
			get; 
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			Thread.CurrentPrincipal = new FakePrivilegePrincipal();

			Setting.Rebuild(new List<Setting>
			{
			    new Setting { Key = "user.password.salt.length", Value = "4" }
			});

			var user = MockRepository.GenerateMock<IRepository<User>>();

			this.AuthenticationService = new FormsAuthenticationService(
				MockRepository.GenerateMock<IEmailService>(), 
				user
			);
		}

		[Test]
		public void Test_FormsAuthenticationService_GeneratePassword()
		{
			var result = this.AuthenticationService.GeneratePassword(6);

			Assert.IsNotNullOrEmpty(result, "NotNullOrEmpty");
			Assert.AreEqual(6, result.Length, "Length");
		}

		[Test]
		public void Test_FormsAuthenticationService_Authenticate()
		{
			var user = new User
			{
				Password = "Test".HashToSha1("123").Hex(), 
				Salt = "123"
			};

			var result = this.AuthenticationService.Authenticate(user, "Test");

			Assert.IsTrue(result);
		}

		[Test]
		public void Test_FormsAuthenticationService_Reset()
		{
			var password = "Test".HashToSha1("123").Hex();
			var salt = "123";

			var user = new User
			{
				Password = password,
				Salt = salt
			};

			this.AuthenticationService.Reset(user);

			Assert.AreNotEqual(password, user.Password, "Password");
			Assert.AreNotEqual(salt, user.Salt, "Salt");
			Assert.AreEqual(4, user.Salt.Length, "Salt.Length");
		}

		[Test]
		public void Test_FormsAuthenticationService_Update()
		{
			var salt = "123";
			var user = new User { Password = "Test", Salt = salt };

			this.AuthenticationService.Update(user, "Other");

			var password = "Other".HashToSha1(user.Salt).Hex();

			Assert.AreEqual(password, user.Password, "Password");
			Assert.AreNotEqual(salt, user.Salt, "Salt");
			Assert.AreEqual(4, user.Salt.Length, "Salt.Length");
		}
	}
}