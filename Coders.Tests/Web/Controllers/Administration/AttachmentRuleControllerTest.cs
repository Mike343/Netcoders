using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Attachments;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Administration;
using Coders.Web.Models.Attachments;
using Coders.Web.Routes;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Controllers.Administration
{
	[TestFixture]
	public class AttachmentRuleControllerTest
	{
		public AttachmentRuleController AttachmentRuleController
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

			var controller = new AttachmentRuleController(
				new FakeAuditService<AttachmentRule, AttachmentRuleAudit>(),
				new FakeAttachmentRuleService()
			);

			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.AttachmentRuleController = controller;
		}

		[Test]
		public void Test_AttachmentRuleController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.AttachmentRuleController.Index(null, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<AttachmentRule>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Index(null, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_History()
		{
			PrincipalHelper.Create();

			var viewResult = this.AttachmentRuleController.History(SortAudit.Created, SortOrder.Descending, 1, null) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.History, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<Audit>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.History(SortAudit.Created, SortOrder.Descending, 1, null) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.AttachmentRuleController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as AttachmentRuleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new AttachmentRuleCreateOrUpdate
			{
				Group = "test",
				FileType = "test",
				FileExtension = "test"
			};

			var viewResult = this.AttachmentRuleController.Create(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as AttachmentRuleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.AttachmentRuleCreated.FormatInvariant(model.FileType, model.Group), model.Message.Message);

			value = new AttachmentRuleCreateOrUpdate();

			var errorResult = this.AttachmentRuleController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(3, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_Update()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.AttachmentRuleController.Update(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.AttachmentRuleController.Update(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as AttachmentRuleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Update(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new AttachmentRuleCreateOrUpdate
			{
				Id = 0,
				Group = "test",
				FileType = "test",
				FileExtension = "test"
			};

			var notFoundResult = this.AttachmentRuleController.Update(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.AttachmentRuleController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as AttachmentRuleCreateOrUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.AttachmentRuleUpdated.FormatInvariant(model.FileType, model.Group), model.Message.Message);

			value = new AttachmentRuleCreateOrUpdate { Id = 1 };

			var errorResult = this.AttachmentRuleController.Update(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(3, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Update(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_Delete()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.AttachmentRuleController.Delete(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.AttachmentRuleController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as AttachmentRuleDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AttachmentRuleController_Delete_Post()
		{
			PrincipalHelper.Create();

			var value = new AttachmentRuleDelete { Id = 0 };
			var notFoundResult = this.AttachmentRuleController.Delete(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var redirectToRouteResult = this.AttachmentRuleController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(AdministrationRoutes.AttachmentRuleIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AttachmentRuleController.Delete(value) as NotAuthorizedResult;

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