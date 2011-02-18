using System.Web.Mvc;
using System.Web.Routing;
using Coders.Tests.Web.Helpers;
using Coders.Web.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class RouteExtensionTest
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
		public void Test_RouteExtension_Route_Link()
		{
			var result = this.Helper.UrlHelper.Route("test").Link("Test");

			Assert.AreEqual("<a href=\"\" title=\"Test\">Test</a>", result.ToString());
		}

		[Test]
		public void Test_RouteExtension_Route_Image()
		{
			var result = this.Helper.UrlHelper.Route("test").Image("test.jpg", "Test");

			Assert.AreEqual("<a href=\"\"><img alt=\"Test\" src=\"/test.jpg\" /></a>", result.ToString());
		}

		[Test]
		public void Test_RouteExtension_CreateArea()
		{
			var routes = RouteTable.Routes;

			routes.CreateArea("testing", "Coders.Web.Tests.Extensions",
				routes.MapRoute("other", "other/fire", new { controller = "other", action = "fire" })
			);

			var route = RouteTable.Routes["other"];

			var path = route.GetVirtualPath(
				this.Helper.ViewContext.RequestContext, 
				new RouteValueDictionary()
			);

			Assert.AreEqual("other/fire", path.VirtualPath);
		}
	}
}