using System.Web.Mvc;
using Coders.DependencyResolution;
using Coders.Models.Common;
using Coders.Models.Countries;
using Coders.Models.TimeZones;
using Coders.Strings;
using Coders.Tests.Web.Fakes;
using Coders.Tests.Web.Helpers;
using Coders.Web.ActionResults;
using Coders.Web.Controllers;
using Coders.Web.Controllers.Users;
using Coders.Web.Models.Users;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web.Controllers.Users
{
	[TestFixture]
	public class PreferenceControllerTest
	{
		public PreferenceController PreferenceController
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

			var controller = new PreferenceController(new FakeUserService());
			var helper = new ViewHelper();

			controller.ControllerContext = helper.ControllerContext;
			controller.Url = helper.UrlHelper;

			this.PreferenceController = controller;
		}

		[Test]
		public void Test_PreferenceController_Update()
		{
			PrincipalHelper.Create();

			var viewResult = this.PreferenceController.Update() as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");

			var model = viewResult.Model as UserPreferenceUpdate;

			Assert.IsNotNull(model, "Model");
		}

		[Test]
		public void Test_PreferenceController_Update_Post()
		{
			PrincipalHelper.Create();

			var value = new UserPreferenceUpdate();
			var viewResult = this.PreferenceController.Update(value) as ViewResult;

			Assert.IsNotNull(viewResult, "ViewResult");
			Assert.AreEqual(Views.Update, viewResult.ViewName, "ViewName");
			Assert.AreEqual(Messages.UserAccountPreferenceUpdated, value.Message.Message);

			PrincipalHelper.Clear();

			var notAuthorizedResult = this.PreferenceController.Update(value) as NotAuthorizedResult;

			Assert.IsNotNull(notAuthorizedResult, "NotAuthorizedResult");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<IAuthenticationService>().To<FakeAuthenticationService>();
				Bind<ICountryService>().To<FakeCountryService>();
				Bind<ITimeZoneService>().To<FakeTimeZoneService>();
			}
		}
	}
}