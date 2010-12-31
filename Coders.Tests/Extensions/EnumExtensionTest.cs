using System;
using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class EnumExtensionTest
	{
		// private constants
		private const TestEnum Value = TestEnum.First | TestEnum.Thrid;

		[TestMethod]
		public void Test_EnumExtension_Has()
		{
			Assert.IsTrue(Value.Has(TestEnum.First));
		}

		[TestMethod]
		public void Test_EnumExtension_Has_Multiple()
		{
			Assert.IsTrue(Value.Has(new[] { TestEnum.First, TestEnum.Thrid }));
		}

		[TestMethod]
		public void Test_EnumExtension_Add()
		{
			Assert.IsTrue(Value.Add(TestEnum.Second).Has(TestEnum.Second));
		}

		[TestMethod]
		public void Test_EnumExtension_Remove()
		{
			Assert.IsFalse(Value.Remove(TestEnum.Thrid).Has(TestEnum.Thrid));
		}

		[TestMethod]
		public void Test_EnumExtension_Name()
		{
			Assert.AreEqual("First", TestEnum.First.Name());
		}

		[TestMethod]
		public void Test_EnumExtension_AsEnum()
		{
			Assert.AreEqual(TestEnum.First, "First".AsEnum<TestEnum>());
		}

		[TestMethod]
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