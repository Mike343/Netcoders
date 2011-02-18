using Coders.DependencyResolution;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NUnit.Framework;

namespace Coders.Tests
{
	[TestFixture]
	public class ServiceLocatorTest
	{
		[SetUp]
		public void Initialize()
		{
			IKernel kernel = new StandardKernel();

			kernel.Bind<IWeapon>().To(typeof(Dagger));
			kernel.Bind<IWarrior>().To(typeof(Samurai));

			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		[TearDown]
		public void Cleanup()
		{
			ServiceLocator.SetLocatorProvider(null);
		}

		[Test]
		public void Test_ServiceLocator()
		{
			var warrior = ServiceLocator.Current.GetInstance<IWarrior>();

			Assert.IsInstanceOf(typeof (Dagger), warrior.Weapon);
		}
	}
}
