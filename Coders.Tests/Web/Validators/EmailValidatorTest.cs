using System.Linq;
using Coders.Strings;
using FluentValidation.Validators;
using NUnit.Framework;
using EmailValidator = Coders.Web.Validators.EmailValidator;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class EmailValidatorTest
	{
		[Test]
		public void Test_EmailValidator_Valid()
		{
			var validator = new EmailValidator();
			var value = "test@mail.com";
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test@mail.com", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_EmailValidator_NotValid()
		{
			var validator = new EmailValidator();
			var value = "testmail.com";
			var results = validator.Validate(new PropertyValidatorContext("test", value, "testmail.com", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.EmailNotValid, result.ErrorMessage);
			}
		}
	}
}