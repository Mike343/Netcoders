using System.Collections.Generic;
using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
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
	public class RoleControllerTest
	{
		public RoleController RoleController
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

			var controller = new RoleController(
				new FakeAuditService<UserRole, UserRoleAudit>(), 
				new FakeUserService(), 
				new FakeUserRoleService()
			);

			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.RoleController = controller;
		}

		[Test]
		public void Test_RoleController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.RoleController.Index() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IList<UserRole>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Index() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_History()
		{
			PrincipalHelper.Create();

			var viewResult = this.RoleController.History(SortAudit.Created, SortOrder.Descending, 1, null) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.History, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<Audit>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.History(SortAudit.Created, SortOrder.Descending, 1, null) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.RoleController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserRoleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new UserRoleCreateOrUpdate
			{
				Title = "test",
				IsDefault = false,
				IsAdministrator = true,
			};

			var viewResult = this.RoleController.Create(value, new List<UserRoleRelationUpdateValue>()) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserRoleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.UserRoleCreated.FormatInvariant(model.Title), model.Message.Message);

			value = new UserRoleCreateOrUpdate();

			var errorResult = this.RoleController.Create(value, new List<UserRoleRelationUpdateValue>()) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(1, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Create(value, new List<UserRoleRelationUpdateValue>()) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_Update()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.RoleController.Update(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.RoleController.Update(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserRoleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Update(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new UserRoleCreateOrUpdate
			{
				Id = 0,
				Title = "test",
				IsDefault = false,
				IsAdministrator = true,
			};

			var notFoundResult = this.RoleController.Update(value, new List<UserRoleRelationUpdateValue>()) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.RoleController.Update(value, new List<UserRoleRelationUpdateValue>()) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserRoleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.UserRoleUpdated.FormatInvariant(model.Title), model.Message.Message);

			value.Title = null;

			var errorResult = this.RoleController.Update(value, new List<UserRoleRelationUpdateValue>()) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(1, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Update(value, new List<UserRoleRelationUpdateValue>()) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_Delete()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.RoleController.Delete(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.RoleController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserRoleDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_RoleController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new UserRoleDelete { Id = 0 };
			var notFoundResult = this.RoleController.Delete(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var redirectToRouteResult = this.RoleController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersAdministrationRoutes.RoleIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.RoleController.Delete(value) as NotAuthorizedResult;

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