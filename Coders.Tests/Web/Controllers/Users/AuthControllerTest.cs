using System.Web.Mvc;
using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Users;
using Coders.Web.Models.Users;
using Coders.Web.Routes;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Controllers.Users
{
	[TestFixture]
	public class AuthControllerTest
	{
		public AuthController AuthController
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

			var service = MockRepository.GenerateMock<IUserService>();

			service.Stub(x => x.GetById(1)).Return(new User { EmailAddress = "test" });
			service.Stub(x => x.GetBy(new UserEmailAddressSpecification("test"))).Return(new User { EmailAddress = "test@mail.com" });

			var controller = new AuthController(service);
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.AuthController = controller;
		}

		[Test]
		public void Test_AuthController_LogOn()
		{
			this.AuthController.Request.Stub(x => x["returnUrl"]).Return("test");

			var viewResult = this.AuthController.LogOn() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.LogOn, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAuthentication;

			Assert.IsNotNull(model, "UserAuthentication");
			Assert.AreEqual("test", model.Redirect, "Redirect");
		}

		[Test]
		public void Test_AuthController_LogOn_Post()
		{
			var value = new UserAuthentication
			{
				EmailAddress = "test@mail.com", 
				Password = "test", 
				Redirect = "test", 
				IsPersistent = true
			};

			var redirectResult = this.AuthController.LogOn(value) as RedirectResult;

			Assert.IsNotNull(redirectResult, "RedirectResult");
		}

		[Test]
		public void Test_AuthController_LogOn_Post_Error()
		{
			var value = new UserAuthentication();

			var viewResult = this.AuthController.LogOn(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.LogOn, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAuthentication;

			Assert.IsNotNull(model, "UserAuthentication");
			Assert.AreEqual(2, model.Errors.Count, "Errors");
		}

		[Test]
		public void Test_AuthController_LogOff()
		{
			var redirectToRouteResult = this.AuthController.LogOff() as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(CommonRoutes.Index, redirectToRouteResult.RouteName);
		}

		[Test]
		public void Test_AuthController_Reset()
		{
			var viewResult = this.AuthController.Reset() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Reset, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAuthenticationReset;

			Assert.IsNotNull(model, "UserAuthenticationReset");
		}

		[Test]
		public void Test_AuthController_Reset_Post()
		{
			var value = new UserAuthenticationReset
			{
				EmailAddress = "test@mail.com"
			};

			var statusResult = this.AuthController.Reset(value) as StatusResult;

			Assert.IsNotNull(statusResult, "StatusResult");

			var helper = new ViewHelper(Views.Status);

			statusResult.ViewEngineCollection = helper.ViewEngineCollection;
			statusResult.ExecuteResult(helper.ControllerContext);

			Assert.AreEqual(Messages.UserAccountPasswordReset, statusResult.Message, "Message");
		}

		[Test]
		public void Test_AuthController_Reset_Post_Error()
		{
			var value = new UserAuthenticationReset();
			var viewResult = this.AuthController.Reset(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Reset, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAuthenticationReset;

			Assert.IsNotNull(model, "UserAuthenticationReset");
			Assert.AreEqual(1, model.Errors.Count, "Errors");
		}

		[Test]
		public void Test_AuthController_Update()
		{
			var viewResult = this.AuthController.Update() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAuthenticationUpdate;

			Assert.IsNotNull(model, "UserAuthenticationUpdate");
		}

		[Test]
		public void Test_AuthController_Update_Post()
		{
			var value = new UserAuthenticationUpdate
			{
				EmailAddress = "test@mail.com",
				CurrentPassword = "testing",
				NewPassword = "testing",
				VerifyNewPassword = "testing"
			};

			var redirectToRouteResult = this.AuthController.Update(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersRoutes.AuthLogOn, redirectToRouteResult.RouteName);
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