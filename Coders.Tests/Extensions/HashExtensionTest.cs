using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class HashExtensionTest
	{
		[TestMethod]
		public void Test_HashExtensionTest_HashToSha1()
		{
			Assert.AreEqual("a94a8fe5ccb19ba61c4c0873d391e987982fbbd3", "test".HashToSha1().Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha1_Salt()
		{
			Assert.AreEqual("7288edd0fc3ffcbe93a0cf06e3568e28521687bc", "test".HashToSha1("123").Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha256()
		{
			Assert.AreEqual("9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08", "test".HashToSha256().Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha256_Salt()
		{
			Assert.AreEqual("ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae", "test".HashToSha256("123").Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha384()
		{
			Assert.AreEqual("768412320f7b0aa5812fce428dc4706b3cae50e02a64caa16a782249bfe8efc4b7ef1ccb126255d196047dfedf17a0a9", "test".HashToSha384().Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha384_Salt()
		{
			Assert.AreEqual("c51dcc4e0deaee00fa5af30e29b6e1acbf32fcb3cea8c4b5cdeaecece8e248df439b2da1b834cb67bbafd2fa07d02f49", "test".HashToSha384("123").Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha512()
		{
			Assert.AreEqual("ee26b0dd4af7e749aa1a8ee3c10ae9923f618980772e473f8819a5d4940e0db27ac185f8a0e1d5f84f88bc887fd67b143732c304cc5fa9ad8e6f57f50028a8ff", "test".HashToSha512().Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToSha512_Salt()
		{
			Assert.AreEqual("daef4953b9783365cad6615223720506cc46c5167cd16ab500fa597aa08ff964eb24fb19687f34d7665f778fcb6c5358fc0a5b81e1662cf90f73a2671c53f991", "test".HashToSha512("123").Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToMd5()
		{
			Assert.AreEqual("098f6bcd4621d373cade4e832627b4f6", "test".HashToMd5().Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_HashToMd5_Salt()
		{
			Assert.AreEqual("cc03e747a6afbbcbf8be7668acfebee5", "test".HashToMd5("123").Hex());
		}

		[TestMethod]
		public void Test_HashExtensionTest_Hex()
		{
			var test = new byte[] { 1 };

			Assert.AreEqual("01", test.Hex());
		}
	}
}