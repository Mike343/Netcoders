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
	public class HostSearchControllerTest
	{
		public HostSearchController HostSearchController
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

			var controller = new HostSearchController(new FakeUserHostSearchService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.HostSearchController = controller;
		}

		[Test]
		public void Test_HostSearchController_Index()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.HostSearchController.Index(0, 1) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.HostSearchController.Index(1, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<UserHost>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HostSearchController.Index(1, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HostSearchController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.HostSearchController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserHostSearchCreate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HostSearchController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HostSearchController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new UserHostSearchCreate();
			var redirectToRouteResult = this.HostSearchController.Create(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersAdministrationRoutes.HostSearchIndex, redirectToRouteResult.RouteName);

			value.Save = true;

			var errorResult = this.HostSearchController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Create, errorResult.ViewName, "ErrorViewName");
			Assert.AreEqual(1, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HostSearchController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HostSearchController_Delete()
		{
			PrincipalHelper.Create();

			var viewResult = this.HostSearchController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserHostSearchDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HostSearchController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HostSearchController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new UserHostSearchDelete
			{
				Id = 1
			};

			var redirectToRouteResult = this.HostSearchController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersAdministrationRoutes.HostSearchCreate, redirectToRouteResult.RouteName);

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HostSearchController.Delete(value) as NotAuthorizedResult;

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