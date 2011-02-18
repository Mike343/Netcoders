using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
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
	public class BanControllerTest
	{
		public BanController BanController
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

			var controller = new BanController(new FakeUserBanService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.BanController = controller;
		}

		[Test]
		public void Test_BanController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.BanController.Index(SortUserBan.Created, SortOrder.Descending, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<UserBan>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Index(SortUserBan.Created, SortOrder.Descending, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_BanController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.BanController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserBanCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_BanController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new UserBanCreateOrUpdate
			{
				Name = "test", 
				Reason = "test", 
				IsPermanent = true
			};

			var viewResult = this.BanController.Create(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserBanCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.UserBanCreated.FormatInvariant(string.Empty), model.Message.Message);

			value.Name = null;

			var errorResult = this.BanController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(2, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_BanController_Update()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.BanController.Update(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.BanController.Update(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserBanCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Update(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_BanController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new UserBanCreateOrUpdate
			{
				Id = 0,
				Reason = "test",
				IsPermanent = true
			};

			var notFoundResult = this.BanController.Update(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.BanController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserBanCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.UserBanUpdated.FormatInvariant(string.Empty), model.Message.Message);

			value.Reason = null;

			var errorResult = this.BanController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(1, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_BanController_Delete()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.BanController.Delete(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.BanController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserBanDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_BanController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new UserBanDelete { Id = 0 };
			var notFoundResult = this.BanController.Delete(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var redirectToRouteResult = this.BanController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersAdministrationRoutes.BanIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.BanController.Delete(value) as NotAuthorizedResult;

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