using System;
using Coders.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Extensions
{
	[TestFixture]
	public class EnumExtensionTest
	{
		// private constants
		private const TestEnum Value = TestEnum.First | TestEnum.Thrid;

		[Test]
		public void Test_EnumExtension_Has()
		{
			Assert.IsTrue(Value.Has(TestEnum.First));
		}

		[Test]
		public void Test_EnumExtension_Has_Multiple()
		{
			Assert.IsTrue(Value.Has(new[] { TestEnum.First, TestEnum.Thrid }));
		}

		[Test]
		public void Test_EnumExtension_Add()
		{
			Assert.IsTrue(Value.Add(TestEnum.Second).Has(TestEnum.Second));
		}

		[Test]
		public void Test_EnumExtension_Remove()
		{
			Assert.IsFalse(Value.Remove(TestEnum.Thrid).Has(TestEnum.Thrid));
		}

		[Test]
		public void Test_EnumExtension_Name()
		{
			Assert.AreEqual("First", TestEnum.First.Name());
		}

		[Test]
		public void Test_EnumExtension_AsEnum()
		{
			Assert.AreEqual(TestEnum.First, "First".AsEnum<TestEnum>());
		}

		[Test]
		public void Test_EnumExtension_AsEnum_Object()
		{
			Assert.AreEqual(TestEnum.First, (TestEnum.First as object).AsEnum<TestEnum>());
		}

		[Flags]
		public enum TestEnum
		{
			None = 0,
			First = 1,
			Second = 2,
			Thrid = 3
		}
	}
}