using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class ObjectExtensionTest
	{
		[TestMethod]
		public void Test_ObjectExtension_AsInt()
		{
			var value = "1".AsInt();

			Assert.AreEqual(1, value);
		}

		[TestMethod]
		public void Test_ObjectExtension_AsBooleant()
		{
			var value = "true".AsBoolean();

			Assert.AreEqual(true, value);
		}
	}
}