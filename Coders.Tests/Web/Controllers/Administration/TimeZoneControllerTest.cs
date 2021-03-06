﻿using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.TimeZones;
using Coders.Models.TimeZones.Enums;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Administration;
using Coders.Web.Models.TimeZones;
using Coders.Web.Routes;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Controllers.Administration
{
	[TestFixture]
	public class TimeZoneControllerTest
	{
		public TimeZoneController TimeZoneController
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

			var controller = new TimeZoneController(
				new FakeAuditService<TimeZone, TimeZoneAudit>(),
				new FakeTimeZoneService()
			);

			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.TimeZoneController = controller;
		}

		[Test]
		public void Test_TimeZoneController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.TimeZoneController.Index(SortTimeZone.Title, SortOrder.Ascending, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<TimeZone>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Index(SortTimeZone.Title, SortOrder.Ascending, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_History()
		{
			PrincipalHelper.Create();

			var viewResult = this.TimeZoneController.History(SortAudit.Created, SortOrder.Descending, 1, null) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.History, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<Audit>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.History(SortAudit.Created, SortOrder.Descending, 1, null) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.TimeZoneController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as TimeZoneCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new TimeZoneCreateOrUpdate
			{
				Title = "test",
				Display = "test",
				Offset = 0
			};

			var viewResult = this.TimeZoneController.Create(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as TimeZoneCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.TimeZoneCreated.FormatInvariant(model.Title), model.Message.Message);

			value = new TimeZoneCreateOrUpdate();

			var errorResult = this.TimeZoneController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(2, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_Update()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.TimeZoneController.Update(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.TimeZoneController.Update(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as TimeZoneCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Update(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new TimeZoneCreateOrUpdate
			{
				Id = 0,
				Title = "test",
				Display = "test",
				Offset = 0
			};

			var notFoundResult = this.TimeZoneController.Update(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.TimeZoneController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as TimeZoneCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.TimeZoneUpdated.FormatInvariant(model.Title), model.Message.Message);

			value = new TimeZoneCreateOrUpdate { Id = 1 };

			var errorResult = this.TimeZoneController.Update(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(2, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Update(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_Delete()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.TimeZoneController.Delete(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.TimeZoneController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as TimeZoneDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_TimeZoneController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new TimeZoneDelete { Id = 0 };
			var notFoundResult = this.TimeZoneController.Delete(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var redirectToRouteResult = this.TimeZoneController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(AdministrationRoutes.TimeZoneIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.TimeZoneController.Delete(value) as NotAuthorizedResult;

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