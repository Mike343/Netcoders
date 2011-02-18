using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.ViewModels;
using NUnit.Framework;

namespace Coders.Tests.Web.ActionResults
{
	[TestFixture]
	public class NotAuthorizedResultTest
	{
		[Test]
		public void Test_NotAuthorizedResult()
		{
			var helper = new ViewHelper(Views.Forbidden);
			var result = new NotAuthorizedResult { ViewEngineCollection = helper.ViewEngineCollection };

			result.ExecuteResult(helper.ControllerContext);

			Assert.AreEqual(Views.Forbidden, result.ViewName, "ViewName");
			Assert.IsInstanceOf(typeof(StatusViewModel), result.Model, "Model");
		}
	}
}