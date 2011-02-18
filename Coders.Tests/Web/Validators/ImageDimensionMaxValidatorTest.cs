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
	public class ImageDimensionMaxValidatorTest
	{
		[Test]
		public void Test_ImageDimensionMaxValidator_Valid()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();
			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.GetImageDimensions(file)).Return(new[] { 100, 100 });

			var validator = new ImageDimensionMaxValidator(100, 100, image);
			var results = validator.Validate(new PropertyValidatorContext("test", file, file, "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_ImageDimensionMaxValidator_NotValid()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();
			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.GetImageDimensions(file)).Return(new[] { 200, 200 });

			var validator = new ImageDimensionMaxValidator(100, 100, image);
			var results = validator.Validate(new PropertyValidatorContext("test", file, file, "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(
					Errors.ImageDimensionMaxNotValid.FormatInvariant(100, 100), 
					result.ErrorMessage
				);
			}
		}
	}
}