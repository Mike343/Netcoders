using Coders.Models.Common;
using Coders.Services;
using NUnit.Framework;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class TextFormattingServiceTest
	{
		public ITextFormattingService TextFormattingService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			this.TextFormattingService = new TextFormattingService();
		}

		[Test]
		public void Test_TextFormattingService_Parse()
		{
			var value = "[b][i][u]Bold[/u][/i][/b] [url]www.test.com[/url]";

			value += "<script type=\"text/javascript\">somejs</script>";

			var expected = "<strong><span style=\"font-style: italic;\">";

			expected += "<span style=\"text-decoration: underline;\">Bold</span></span>";
			expected += "</strong> <a href=\"http://www.test.com\" title=\"test.com\">test.com</a>somejs";

			var result = this.TextFormattingService.Parse(value);

			Assert.AreEqual(expected, result, "Result");
		}

		[Test]
		public void Test_TextFormattingService_Strip()
		{
			var result = this.TextFormattingService.Strip("[b][i][u]Bold[/u][/i][/b] [url]www.test.com[/url]");

			Assert.AreEqual("Bold test.com", result, "Result");
		}
	}
}