using Coders.Web.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class StringExtensionTest
	{
		[Test]
		public void Test_StringExtension_ValueWhenNullOrEmpty()
		{
			var value = string.Empty;

			Assert.AreEqual("test", value.ValueWhenNullOrEmpty("test"));
		}
	}
}