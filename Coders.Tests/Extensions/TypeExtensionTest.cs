using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class TypeExtensionTest
	{
		[TestMethod]
		public void Test_TypeExtension_GetProperty()
		{
			var value = new Value
			{
				Name = "Test"
			};

			var name = "Name".GetProperty(value) as string;

			Assert.AreEqual("Test", name);
		}

		private class Value
		{
			public string Name
			{
				get;
				set;
			}
		}
	}
}