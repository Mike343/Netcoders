using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class StringExtensionTest
	{
		[TestMethod]
		public void Test_StringExtension_GetValueOrNull_Null()
		{
			Assert.AreEqual(null, string.Empty.GetValueOrNull());
		}

		[TestMethod]
		public void Test_StringExtension_GetValueOrNull_Value()
		{
			Assert.AreEqual("value", "value".GetValueOrNull());
		}

		[TestMethod]
		public void Test_StringExtension_Slug()
		{
			Assert.AreEqual("this-is-a-teststring", "This is a test-string # ".Slug());
		}

		[TestMethod]
		public void Test_StringExtension_Indent()
		{
			Assert.AreEqual("-- value", "value".Indent(2, "-"));
		}

		[TestMethod]
		public void Test_StringExtension_FormatInvariant()
		{
			Assert.AreEqual("value (value)", "value ({0})".FormatInvariant("value"));
		}

		[TestMethod]
		public void Test_StringExtension_Truncate()
		{
			Assert.AreEqual("value...", "value value value".Truncate(5));
		}

		[TestMethod]
		public void Test_StringExtension_UppercaseFirst()
		{
			Assert.AreEqual("Value", "value".UppercaseFirst());
		}

		[TestMethod]
		public void Test_StringExtension_AsReadableSize()
		{
			Assert.AreEqual("40 KB", 40960.AsReadableSize());
		}
	}
}