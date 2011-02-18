using System.Linq;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class UserUniqueEmailAddressValidatorTest
	{
		[Test]
		public void Test_UserUniqueEmailAddressValidator_Valid()
		{
			var value = "fake";
			var validator = new UserUniqueEmailAddressValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "fake", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_UserUniqueEmailAddressValidator_NotValid()
		{
			var value = "test";
			var validator = new UserUniqueEmailAddressValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.UserEmailAddressTaken, result.ErrorMessage);
			}
		}
	}
}