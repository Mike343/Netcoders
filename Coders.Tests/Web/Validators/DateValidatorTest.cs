using System;
using System.Linq;
using Coders.Strings;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class DateValidatorTest
	{
		[Test]
		public void Test_CharacterValidator_Valid()
		{
			var validator = new DateValidator();
			var value = DateTime.Now.ToString("M/d/yyyy");
			var results = validator.Validate(new PropertyValidatorContext("test", value, value, "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_DateValidator_NotValid()
		{
			var validator = new DateValidator();
			var value = "fake";
			var results = validator.Validate(new PropertyValidatorContext("test", value, "fake", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.DateNotValid, result.ErrorMessage);
			}
		}
	}
}