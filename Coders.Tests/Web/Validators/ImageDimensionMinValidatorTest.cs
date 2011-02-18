using System.Linq;
using System.Web;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Strings;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class ImageDimensionMinValidatorTest
	{
		[Test]
		public void Test_ImageDimensionMinValidator_Valid()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();
			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.GetImageDimensions(file)).Return(new[] { 100, 100 });

			var validator = new ImageDimensionMinValidator(100, 100, image);
			var results = validator.Validate(new PropertyValidatorContext("test", file, file, "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_ImageDimensionMinValidator_NotValid()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();
			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.GetImageDimensions(file)).Return(new[] { 100, 100 });

			var validator = new ImageDimensionMinValidator(200, 200, image);
			var results = validator.Validate(new PropertyValidatorContext("test", file, file, "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(
					Errors.ImageDimensionMinNotValid.FormatInvariant(200, 200),
					result.ErrorMessage
				);
			}
		}
	}
}