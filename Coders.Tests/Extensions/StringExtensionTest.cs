using Coders.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Extensions
{
	[TestFixture]
	public class StringExtensionTest
	{
		[Test]
		public void Test_StringExtension_GetValueOrNull_Null()
		{
			Assert.AreEqual(null, string.Empty.GetValueOrNull());
		}

		[Test]
		public void Test_StringExtension_GetValueOrNull_Value()
		{
			Assert.AreEqual("value", "value".GetValueOrNull());
		}

		[Test]
		public void Test_StringExtension_Slug()
		{
			Assert.AreEqual("this-is-a-teststring", "This is a test-string # ".Slug());
		}

		[Test]
		public void Test_StringExtension_Indent()
		{
			Assert.AreEqual("-- value", "value".Indent(2, "-"));
		}

		[Test]
		public void Test_StringExtension_FormatInvariant()
		{
			Assert.AreEqual("value (value)", "value ({0})".FormatInvariant("value"));
		}

		[Test]
		public void Test_StringExtension_Truncate()
		{
			Assert.AreEqual("value...", "value value value".Truncate(5));
		}

		[Test]
		public void Test_StringExtension_UppercaseFirst()
		{
			Assert.AreEqual("Value", "value".UppercaseFirst());
		}

		[Test]
		public void Test_StringExtension_AsReadableSize()
		{
			Assert.AreEqual("40 KB", 40960.AsReadableSize());
		}
	}
}