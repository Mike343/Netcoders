using System.Linq;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Specifications;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class UserAuthorizeValidatorTest
	{
		[Test]
		public void Test_UserAuthorizeValidator_Valid()
		{
			var test = new Test();
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();
			var userService = new FakeUserService();

			authentication.Stub(x => x.Authenticate(userService.GetBy(new Specification<User>()), test.Password)).Return(true);

			var validator = new UserAuthorizeValidator<Test>(x => x.EmailAddress, authentication, userService);
			var results = validator.Validate(new PropertyValidatorContext("test", test, test.Password, "Password"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_UserAuthorizeValidator_NotValid()
		{
			var test = new Test();
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();
			var userService = new FakeUserService();

			authentication.Stub(x => x.Authenticate(userService.GetBy(new Specification<User>()), test.Password)).Return(false);

			var validator = new UserAuthorizeValidator<Test>(x => x.EmailAddress, authentication, userService);
			var results = validator.Validate(new PropertyValidatorContext("test", test, test.Password, "Password"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.UserAuthenticationFailed, result.ErrorMessage);
			}
		}

		public class Test
		{
			public string EmailAddress
			{
				get
				{
					return "test@mail.com";
				}
			}

			public string Password
			{
				get
				{
					return "pass";
				}
			}
		}
	}
}