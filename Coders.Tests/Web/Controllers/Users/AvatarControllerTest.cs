using System.Web;
using System.Web.Mvc;
using Coders.Collections;
using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Users;
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
	public class AvatarControllerTest
	{
		public AvatarController AvatarController
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

			var userService = MockRepository.GenerateMock<IUserService>();
			var controller = new AvatarController(userService, new FakeUserAvatarService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.AvatarController = controller;
		}

		[Test]
		public void Test_AvatarController_Index()
		{
			PrincipalHelper.Create();

			var viewResult = this.AvatarController.Index(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Index, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as IPagedCollection<UserAvatar>;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AvatarController.Index(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AvatarController_Assign()
		{
			PrincipalHelper.Create();

			var redirectToRouteResult = this.AvatarController.Assign(1) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersRoutes.AvatarIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AvatarController.Assign(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResul");
		}

		[Test]
		public void Test_AvatarController_Create()
		{
			PrincipalHelper.Create();

			var viewResult = this.AvatarController.Create() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Create, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAvatarCreate;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AvatarController.Create() as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AvatarController_Create_Post()
		{
			var file = MockRepository.GenerateMock<HttpPostedFileBase>();

			file.Stub(x => x.FileName).Return("test.jpg");
			file.Stub(x => x.ContentType).Return("image/jpeg");
			file.Stub(x => x.ContentLength).Return(12345678);

			this.AvatarController.Request.Stub(x => x.Files[0]).Return(file);

			var value = new UserAvatarCreate();

			PrincipalHelper.Create();

			var redirectToRouteResult = this.AvatarController.Create(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersRoutes.AvatarIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AvatarController.Create(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		[Test]
		public void Test_AvatarController_Delete()
		{
			PrincipalHelper.Create();

			var viewResult = this.AvatarController.Delete(1) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Delete, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserAvatarDelete;

			Assert.IsNotNull(model, "Model");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AvatarController.Delete(1) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");

			var notFoundResult = this.AvatarController.Delete(0) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");
		}

		[Test]
		public void Test_AvatarController_Delete_Post()
		{
			var value = new UserAvatarDelete
			{
				Id = 1
			};

			PrincipalHelper.Create();

			var redirectToRouteResult = this.AvatarController.Delete(value) as RedirectToRouteResult;

			Assert.IsNotNull(redirectToRouteResult, "RedirectToRouteResult");
			Assert.AreEqual(UsersRoutes.AvatarIndex, redirectToRouteResult.RouteName, "RouteName");

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.AvatarController.Delete(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");

			value.Id = 0;

			var notFoundResult = this.AvatarController.Delete(value) as HttpNotFoundResult;

			Assert.IsNotNull(notFoundResult, "HttpNotFoundResult");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<IImageService>().To<FakeImageService>();
			}
		}
	}
}