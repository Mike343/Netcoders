using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.ViewModels;
using NUnit.Framework;

namespace Coders.Tests.Web.ActionResults
{
	[TestFixture]
	public class ErrorResultTest
	{
		[Test]
		public void Test_ErrorResult()
		{
			var helper = new ViewHelper(Views.Error);
			var result = new ErrorResult("test") { ViewEngineCollection = helper.ViewEngineCollection };

			result.ExecuteResult(helper.ControllerContext);

			Assert.AreEqual("test", result.Message, "Message");
			Assert.AreEqual(Views.Error, result.ViewName, "ViewName");
			Assert.IsInstanceOf(typeof(ErrorViewModel), result.Model, "Model");
		}
	}
}