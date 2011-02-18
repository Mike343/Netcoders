using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Extensions;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Countries;
using Coders.Models.TimeZones;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Models.Users;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using HomeController = Coders.Web.Controllers.Users.Administration.HomeController;

namespace Coders.Tests.Web.Controllers.Users.Administration
{
	[TestFixture]
	public class HomeControllerTest
	{
		public HomeController HomeController
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

			var controller = new HomeController(new FakeAuditService<User, UserAudit>(), new FakeUserService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.HomeController = controller;
		}

		[Test]
		public void Test_HomeController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.HomeController.Index(null, SortUser.Name, SortOrder.Ascending, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<User>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Index(null, SortUser.Name, SortOrder.Ascending, 1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_History()
		{
			PrincipalHelper.Create();

			var viewResult = this.HomeController.History(SortAudit.Created, SortOrder.Descending, 1, null) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.History, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<Audit>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.History(SortAudit.Created, SortOrder.Descending, 1, null) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.HomeController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAdminCreate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_Create_Post()
		{
			PrincipalHelper.Create();

			var value = new UserAdminCreate
			{
				Name = "fake",
				EmailAddress = "fake@mail.com",
				VerifyEmailAddress = "fake@mail.com",
				Password = "testing",
				VerifyPassword = "testing"
			};

			var viewResult = this.HomeController.Create(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAdminUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.UserCreated.FormatInvariant(model.Name), model.Message.Message);

			value = new UserAdminCreate();

			var errorResult = this.HomeController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Create, errorResult.ViewName, "ErrorViewName");
			Assert.AreEqual(6, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_Update()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.HomeController.Update(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.HomeController.Update(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAdminUpdate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Update(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new UserAdminUpdate
			{
				Id = 0,
				Name = "test",
				CurrentName = "test",
				EmailAddress = "test@mail.com",
				VerifyEmailAddress = "test@mail.com",
				CurrentEmailAddress = "test@mail.com"
			};

			var notFoundResult = this.HomeController.Update(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.HomeController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");
			Assert.AreEqual(Messages.UserUpdated.FormatInvariant(value.Name), value.Message.Message);

			value.Name = null;

			var errorResult = this.HomeController.Update(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(2, value.Errors.Count, "Errors");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Update(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_Reset()
		{
			PrincipalHelper.Create();

			var notFoundResult = this.HomeController.Reset(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			var viewResult = this.HomeController.Reset(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Reset, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAuthenticationReset;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Reset(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_HomeController_Reset_Post()
		{
			PrincipalHelper.Create();

			var value = new UserAuthenticationReset
			{
				Id = 0,
				EmailAddress = "test@mail.com"
			};

			var notFoundResult = this.HomeController.Reset(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");

			value.Id = 1;

			var viewResult = this.HomeController.Reset(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAdminUpdate;

			Assert.IsNotNull(model, "Model");
			Assert.AreEqual(Messages.UserPasswordReset.FormatInvariant(model.Name), model.Message.Message);

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.HomeController.Reset(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IUserService>().To<FakeUserService>();
				Bind<ICountryService>().To<FakeCountryService>();
				Bind<ITimeZoneService>().To<FakeTimeZoneService>();
			}
		}
	}
}