using System.Collections.Generic;
using System.Web;
using Coders.Models.Countries;
using Coders.Models.Settings;
using Coders.Web.Extensions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class CountryExtensionTest
	{
		public HttpContextBase Context
		{
			get; 
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var context = MockRepository.GenerateMock<HttpContextBase>();
			var request = MockRepository.GenerateMock<HttpRequestBase>();

			request.Stub(x => x.ApplicationPath).Return("/");

			context.Stub(x => x.Request).Return(request);

			var settings = new List<Setting>
			{
			    new Setting { Key = "country.flag.path", Value = "images/icons" }
			};

			Setting.Rebuild(settings);

			this.Context = context;
		}

		[Test]
		public void Test_CountryExtension_FlagImage()
		{
			var country = new Country { Title = "United States", Code = "us" };
			var flag = country.FlagImage(this.Context);

			Assert.AreEqual("<img alt=\"United States\" src=\"/images/icons/us.gif\" />", flag.ToString());
		}
	}
}