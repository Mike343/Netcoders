using Coders.Tests.Web.Helpers;
using Coders.Web.Extensions;
using Coders.Web.Models;
using NUnit.Framework;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class FilterExtensionTest
	{
		public ViewHelper Helper
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			this.Helper = new ViewHelper();
		}

		[Test]
		public void Test_FilterExtension_IsSelected()
		{
			var filter = new Filter("Test", new { controller = "test", action = "fire" });
			var result = filter.IsSelected(this.Helper.HtmlHelper);

			Assert.IsTrue(result);
		}

		[Test]
		public void Test_FilterExtension_IsSelected_False()
		{
			var filter = new Filter("Test", new { controller = "test", action = "other" });
			var result = filter.IsSelected(this.Helper.HtmlHelper);

			Assert.IsFalse(result);
		}
	}
}