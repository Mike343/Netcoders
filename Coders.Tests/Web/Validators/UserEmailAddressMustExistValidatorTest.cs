using System.Linq;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class UserEmailAddressMustExistValidatorTest
	{
		[Test]
		public void Test_UserEmailAddressMustExistValidator_Valid()
		{
			var value = "test@mail.com";
			var validator = new UserEmailAddressMustExistValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test@mail.com", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_UserEmailAddressMustExistValidator_NotValid()
		{
			var value = "test";
			var validator = new UserEmailAddressMustExistValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.UserEmailAddressNotFound, result.ErrorMessage);
			}
		}
	}
}