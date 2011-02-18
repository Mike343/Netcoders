using System.Linq;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class UserNameNotProtectedValidatorTest
	{
		[Test]
		public void Test_UserNameNotProtectedValidator_Valid()
		{
			var value = "test";
			var validator = new UserNameNotProtectedValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_UserNameNotProtectedValidator_NotValid()
		{
			var value = "other";
			var validator = new UserNameNotProtectedValidator(new FakeUserService());
			var results = validator.Validate(new PropertyValidatorContext("test", value, "other", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.UserProtected, result.ErrorMessage);
			}
		}
	}
}