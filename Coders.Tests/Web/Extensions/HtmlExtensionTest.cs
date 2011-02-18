using System;
using System.Collections.Generic;
using Coders.Collections;
using Coders.Tests.Web.Helpers;
using Coders.Web.Extensions;
using Coders.Web.Models;
using NUnit.Framework;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class HtmlExtensionTest
	{
		public ViewHelper Helper
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			this.Helper = new ViewHelper();
		}

		[Test]
		public void Test_HtmlExtension_Pager()
		{
			var list = new PagedCollection<int>(new List<int>(), 1, 1, 2);

			this.Helper.HtmlHelper.Pager(list, "test");

			Assert.AreEqual(331, this.Helper.ViewContext.Writer.ToString().Length);
		}

		[Test]
		public void Test_HtmlExtension_Message()
		{
			var list = new PagedCollection<int>(new List<int>(), 1, 1, 2);

			var value = new TestValue();

			value.SuccessMessage("Test");

			this.Helper.HtmlHelper.Message(value);

			Assert.AreEqual(353, this.Helper.ViewContext.Writer.ToString().Length);
		}

		[Test]
		public void Test_HtmlExtension_ValidationResult()
		{
			var value = new TestValue();

			value.SuccessMessage("Test");

			this.Helper.HtmlHelper.ViewData.ModelState.AddModelError("Error", "Error");
			this.Helper.HtmlHelper.ValidationResult();

			Assert.AreEqual(371, this.Helper.ViewContext.Writer.ToString().Length);
		}

		private class TestValue : Value
		{
			public override void Validate()
			{
				throw new NotImplementedException();
			}
		}
	}
}