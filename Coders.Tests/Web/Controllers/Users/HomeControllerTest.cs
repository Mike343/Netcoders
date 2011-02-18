using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
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
using HomeController = Coders.Web.Controllers.Users.HomeController;

namespace Coders.Tests.Web.Controllers.Users
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

			var controller = new HomeController(new FakeUserService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.HomeController = controller;
		}

		[Test]
		public void Test_HomeController_Index()
		{
			var viewResult = this.HomeController.Index(SortUser.Name, SortOrder.Ascending, 1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<User>;

			Assert.IsNotNull(model, "Model");
		}

		[Test]
		public void Test_HomeController_Detail()
		{
			var viewResult = this.HomeController.Detail(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Detail, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as User;

			Assert.IsNotNull(model, "Model");

			var notFoundResult = this.HomeController.Detail(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");
		}

		[Test]
		public void Test_HomeController_Create()
		{
			var viewResult = this.HomeController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserCreate;

			Assert.IsNotNull(model, "Model");
		}

		[Test]
		public void Test_HomeController_Create_Post()
		{
			var value = new UserCreate
			{
				Name = "fake",
				EmailAddress = "fake@mail.com",
				VerifyEmailAddress = "fake@mail.com", 
				Password = "testing",
				VerifyPassword = "testing"
			};

			var statusResult = this.HomeController.Create(value) as StatusResult;

			Assert.IsNotNull(statusResult, "StatusResult");

			var helper = new ViewHelper(Views.Status);

			statusResult.ViewEngineCollection = helper.ViewEngineCollection;
			statusResult.ExecuteResult(helper.ControllerContext);

			Assert.AreEqual(Messages.UserAccountCreated, statusResult.Message, "Message");

			value.Name = string.Empty;

			var errorResult = this.HomeController.Create(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Create, errorResult.ViewName, "ErrorViewName");
			Assert.AreEqual(3, value.Errors.Count, "Errors");
		}

		[Test]
		public void Test_HomeController_Update()
		{
			PrincipalHelper.Create();

			var viewResult = this.HomeController.Update() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserUpdate;

			Assert.IsNotNull(model, "Model");
		}

		[Test]
		public void Test_HomeController_Update_Post()
		{
			var value = new UserUpdate
			{
				Name = "test",
				CurrentName = "test",
				EmailAddress = "test@mail.com",
				VerifyEmailAddress = "test@mail.com",
				CurrentEmailAddress = "test@mail.com"
			};

			var viewResult = this.HomeController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");
			Assert.AreEqual(Messages.UserAccountUpdated, value.Message.Message);

			value.Name = null;

			var errorResult = this.HomeController.Update(value) as ViewResult;

			Assert.IsNotNull(errorResult, "ErrorResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ErrorViewName");
			Assert.AreEqual(2, value.Errors.Count, "Errors");
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