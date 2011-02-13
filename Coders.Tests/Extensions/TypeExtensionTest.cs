using Coders.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Extensions
{
	[TestFixture]
	public class TypeExtensionTest
	{
		[Test]
		public void Test_TypeExtension_GetProperty()
		{
			var value = new Value
			{
				Name = "Test"
			};

			var name = "Name".GetProperty(value) as string;

			Assert.AreEqual("Test", name);
		}

		[Test]
		public void Test_TypeExtension_GetTypesThatImplement()
		{
			var types = typeof(TypeExtensionTest).Assembly.GetTypesThatImplement<ITest>();

			foreach (var type in types)
			{
				Assert.AreEqual("Rock", type.Name);
			}
		}

		private class Value
		{
			public string Name
			{
				get;
				set;
			}
		}

		public interface ITest
		{

		}

		public class Rock : ITest
		{
			
		}
	}
}