using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Users.Administration;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Controllers.Users.Administration
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
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));

			var controller = new SearchController(new FakeUserSearchService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.SearchController = controller;
		}

		[Test]
		public void Test_SearchController_Index()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.SearchController.Index(0, 1) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.SearchController.Index(1, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<User>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SearchController.Index(1, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SearchController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.SearchController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserSearchCreate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SearchController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SearchController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new UserSearchCreate();
			var redirectToRouteResult = this.SearchController.Create(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersAdministrationRoutes.SearchIndex, redirectToRouteResult.RouteName);

			value.Save = true;

			var errorResult = this.SearchController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Create, errorResult.ViewName, "ErrorViewName");
			Assert.AreEqual(1, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SearchController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SearchController_Delete()
		{
			PrincipalHelper.Create();

			var viewResult = this.SearchController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserSearchDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SearchController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SearchController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new UserSearchDelete
			{
				Id = 1
			};

			var redirectToRouteResult = this.SearchController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersAdministrationRoutes.SearchCreate, redirectToRouteResult.RouteName);

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SearchController.Delete(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IUserService>().To<FakeUserService>();
			}
		}
	}
}