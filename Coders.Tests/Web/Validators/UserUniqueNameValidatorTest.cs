using System.Linq;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class UserUniqueNameValidatorTest
	{
		[Test]
		public void Test_UserUniqueEmailAddressValidator_Valid()
		{
			var value = "fake@mail.com";
			var validator = new UserUniqueNameValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "fake@mail.co", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_UserUniqueEmailAddressValidator_NotValid()
		{
			var value = "test@mail.com";
			var validator = new UserUniqueNameValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test@mail.com", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.UserNameTaken, result.ErrorMessage);
			}
		}
	}
}