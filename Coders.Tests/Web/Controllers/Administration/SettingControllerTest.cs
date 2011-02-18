using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Settings;
using Coders.Models.Settings.Enums;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Administration;
using Coders.Web.Models.Settings;
using Coders.Web.Routes;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Controllers.Administration
{
	[TestFixture]
	public class SettingControllerTest
	{
		public SettingController SettingController
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

			var controller = new SettingController(
				new FakeAuditService<Setting, SettingAudit>(),
				new FakeSettingService()
			);

			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.SettingController = controller;
		}

		[Test]
		public void Test_SettingController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.SettingController.Index(null, SortSetting.Group, SortOrder.Ascending, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<Setting>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Index(null, SortSetting.Group, SortOrder.Ascending, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_History()
		{
			PrincipalHelper.Create();

			var viewResult = this.SettingController.History(SortAudit.Created, SortOrder.Descending, 1, null) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.History, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<Audit>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.History(SortAudit.Created, SortOrder.Descending, 1, null) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.SettingController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as SettingCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new SettingCreateOrUpdate
			{
				Title = "test",
				Group = "test",
				ItemKey = "test",
				ItemValue = "test",
			};

			var viewResult = this.SettingController.Create(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as SettingCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.SettingCreated.FormatInvariant(model.Title), model.Message.Message);

			value = new SettingCreateOrUpdate();

			var errorResult = this.SettingController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(4, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_Update()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.SettingController.Update(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.SettingController.Update(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as SettingCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Update(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new SettingCreateOrUpdate
			{
				Id = 0,
				Title = "test",
				Group = "test",
				CurrentKey = "test",
				ItemKey = "test",
				ItemValue = "test",
			};

			var notFoundResult = this.SettingController.Update(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.SettingController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as SettingCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.SettingUpdated.FormatInvariant(model.Title), model.Message.Message);

			value = new SettingCreateOrUpdate { Id = 1 };

			var errorResult = this.SettingController.Update(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(4, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Update(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_Delete()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.SettingController.Delete(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.SettingController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as SettingDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_SettingController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new SettingDelete { Id = 0 };
			var notFoundResult = this.SettingController.Delete(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var redirectToRouteResult = this.SettingController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(AdministrationRoutes.SettingIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.SettingController.Delete(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IUserService>().To<FakeUserService>();
				Bind<ISettingService>().To<FakeSettingService>();
			}
		}
	}
}