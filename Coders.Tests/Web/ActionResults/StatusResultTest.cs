using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.ViewModels;
using NUnit.Framework;

namespace Coders.Tests.Web.ActionResults
{
	[TestFixture]
	public class StatusResultTest
	{
		[Test]
		public void Test_StatusResult()
		{
			var helper = new ViewHelper(Views.Status);
			var result = new StatusResult("test") { ViewEngineCollection = helper.ViewEngineCollection };

			result.ExecuteResult(helper.ControllerContext);

			Assert.AreEqual("test", result.Message, "Message");
			Assert.AreEqual(Views.Status, result.ViewName, "ViewName");
			Assert.IsInstanceOf(typeof(StatusViewModel), result.Model, "Model");
		}
	}
}