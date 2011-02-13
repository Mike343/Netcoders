using System;
using Coders.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Extensions
{
	[TestFixture]
	public class ObjectExtensionTest
	{
		[Test]
		public void Test_ObjectExtension_AsInt()
		{
			var value = "1".AsInt();

			Assert.AreEqual(1, value);
		}

		[Test]
		public void Test_ObjectExtension_AsBoolean()
		{
			var value = "true".AsBoolean();

			Assert.AreEqual(true, value);
		}

		[Test]
		public void Test_ObjectExtension_Object_AsBoolean()
		{
			object value = true;

			Assert.AreEqual(true, value.AsBoolean());
		}

		[Test]
		public void Test_ObjectExtension_AsDateTime()
		{
			var value = "2011-02-05 00:00:00.000".AsDateTime();

			Assert.AreEqual(new DateTime(2011, 2, 5, 0, 0, 0, 0), value);
		}
	}
}