using System.Web.Mvc;
using Coders.Collections;
using Coders.Models.Users;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Users;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
using NUnit.Framework;

namespace Coders.Tests.Web.Controllers.Users
{
	[TestFixture]
	public class SearchControllerTest
	{
		public SearchController SearchController
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var controller = new SearchController(new FakeUserSearchService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.SearchController = controller;
		}

		[Test]
		public void Test_SearchController_Index()
		{
			var viewResult = this.SearchController.Index(1, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<User>;

			Assert.IsNotNull(model, "Model");
		}

		[Test]
		public void Test_SearchController_Create()
		{
			var viewResult = this.SearchController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserSearchCreate;

			Assert.IsNotNull(model, "Model");
		}

		[Test]
		public void Test_SearchController_Create_Post()
		{
			var value = new UserSearchCreate();
			var redirectToRouteResult = this.SearchController.Create(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersRoutes.SearchIndex, redirectToRouteResult.RouteName);

			value.Save = true;

			var errorResult = this.SearchController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Create, errorResult.ViewName, "ErrorViewName");
			Assert.AreEqual(1, value.Errors.Count, "Errors");
		}
	}
}