using System.Linq;
using Coders.Strings;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class CharacterValidatorTest
	{
		[Test]
		public void Test_CharacterValidator_Valid()
		{
			var validator = new CharacterValidator();
			var value = "abc123";
			var results = validator.Validate(new PropertyValidatorContext("test", value, "abc123", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_CharacterValidator_NotValid()
		{
			var validator = new CharacterValidator();
			var value = "#2*3*#&@(";
			var results = validator.Validate(new PropertyValidatorContext("test", value, "#2*3*#&@(", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.CharacterNotValid, result.ErrorMessage);
			}
		}
	}
}