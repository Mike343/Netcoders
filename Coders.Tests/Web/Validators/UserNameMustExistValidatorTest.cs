using System.Linq;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class UserNameMustExistValidatorTest
	{
		[Test]
		public void Test_UserNameMustExistValidator_Valid()
		{
			var value = "test";
			var validator = new UserNameMustExistValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_UserNameMustExistValidator_NotValid()
		{
			var value = "test2";
			var validator = new UserNameMustExistValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test2", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.UserNameNotFound, result.ErrorMessage);
			}
		}
	}
}