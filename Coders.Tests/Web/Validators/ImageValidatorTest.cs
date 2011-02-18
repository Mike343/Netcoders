using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Strings;
using Coders.Web.Validators;
using FluentValidation.Validators;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Validators
{
	[TestFixture]
	public class ImageValidatorTest
	{
		[SetUp]
		public void Initialize()
		{
			var settings = new List<Setting>
			{
				new Setting { Key = "image.extension", Value = "jpg,png,gif" }
			};

			Setting.Rebuild(settings);
		}

		[Test]
		public void Test_ImageValidator_Valid()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();
			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.IsImage(file)).Return(true);

			var validator = new ImageValidator(image);
			var results = validator.Validate(new PropertyValidatorContext("test", file, file, "value"));

			Assert.IsEmpty(results.ToList());
		}

		[Test]
		public void Test_ImageDimensionMaxValidator_NotValid()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();

			file.Stub(x => x.ContentType).Return("application/zip");

			var image = MockRepository.GenerateMock<IImageService>();

			image.Stub(x => x.IsImage(file)).Return(false);

			var validator = new ImageValidator(image);
			var results = validator.Validate(new PropertyValidatorContext("test", file, file, "value"));

			foreach (var result in results)
			{
				Assert.AreEqual(
					Errors.ImageContentTypeNotValid.FormatInvariant(
						file.ContentType, 
						Setting.ImageExtension.Value
					),
					result.ErrorMessage
				);
			}
		}
	}
}