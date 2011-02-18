using System.Linq;
using Coders.Models.Settings;
using Coders.Strings;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class SettingUniqueValidatorTest
	{
		[Test]
		public void Test_SettingUniqueValidator_Valid()
		{
			var value = "test";
			var service = MockRepository.GenerateMock<ISettingService>();

			service.Stub(x => x.Count(new SettingKeySpecification(value))).Return(0);

			var validator = new SettingUniqueValidator(service);
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test", "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_SettingUniqueValidator_NotValid()
		{
			var value = "test";
			var service = MockRepository.GenerateMock<ISettingService>();

			service.Stub(x => x.Count(new SettingKeySpecification(value))).Return(1);

			var validator = new SettingUniqueValidator(service);
			var results = validator.Validate(new PropertyValidatorContext("test", value, "test", "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(Errors.SettingKeyTaken, result.ErrorMessage);
			}
		}
	}
}