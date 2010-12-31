using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Coders.Tests
{
	[TestClass]
	public class ServiceLocatorTest
	{
		[TestInitialize]
		public void TestInitialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Bind<IWeapon>().To(typeof(Dagger));
			kernel.Bind<IWarrior>().To(typeof(Samurai));

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			ServiceLocator.SetLocatorProvider(null);
		}

		[TestMethod]
		public void Test_ServiceLocator()
		{
			var warrior = ServiceLocator.Current.GetInstance<IWarrior>();

			Assert.IsInstanceOfType(warrior.Weapon, typeof(Dagger));
		}
	}
}
