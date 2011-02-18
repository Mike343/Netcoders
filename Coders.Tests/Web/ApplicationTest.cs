using System.Web.Routing;
using Coders.DependencyResolution;
using Coders.Models.Settings;
using Coders.Tests.Web.Fakes;
using Coders.Web;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace Coders.Tests.Web
{
	[TestFixture]
	public class ApplicationTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Load(new TestModule());

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[Test]
		public void Test_Application()
		{
			var application = new Application();

			application.Setup();
			application.Initialize();

			Assert.AreEqual(1, application.Modules.Count, "Modules");
			Assert.AreEqual("test", application.Modules[0].Name, "Modules.Name");
			Assert.AreEqual("1.0", application.Modules[0].Version, "Modules.Version");
			Assert.IsTrue(application.Modules[0].IsActive, "Modules.Version");
			Assert.IsTrue(TestInstaller.IsInitialized, "IsInitialized");
			Assert.AreEqual(1, application.Modules.Count, "Modules");
			Assert.AreEqual("value", Setting.GetByKey("test"), "Setting");
		}

		private class TestModule : NinjectModule
		{
			public override void Load()
			{
				Bind<ISettingService>().To<FakeSettingService>();
			}
		}
	}

	public class TestInstaller : IInstaller
	{
		public string Name
		{
			get
			{
				return "test";
			}
		}

		public string Version
		{
			get
			{
				return "1.0";
			}
		}

		public bool IsActive
		{
			get
			{
				return true;
			}
		}

		public static bool IsInitialized
		{
			get; 
			set;
		}

		public void RegisterRoutes(RouteCollection routes)
		{

		}

		public void Initialize()
		{
			IsInitialized = true;
		}
	}
}