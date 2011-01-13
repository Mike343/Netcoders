using System;
using System.Web;
using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class PathExtensionTest
	{
		private HttpContextBase _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = MockRepository.GenerateMock<HttpContextBase>();

			var request = MockRepository.GenerateMock<HttpRequestBase>();

			request.Stub(x => x.ApplicationPath).Return("site");

			_context.Stub(x => x.Request).Return(request);
		}

		[TestMethod]
		public void Test_PathExtension_AsPath()
		{
			var actual = "{0}\\directory".FormatInvariant(AppDomain.CurrentDomain.BaseDirectory);

			Assert.AreEqual(actual, "directory".AsPath());
		}

		[TestMethod]
		public void Test_PathExtension_AsPath_HttpContext()
		{
			Assert.AreEqual("site\\directory", "directory".AsPath(_context));
		}

		[TestMethod]
		public void Test_PathExtension_AsRoot()
		{
			Assert.AreEqual("site/directory", "directory".AsRoot(_context));
		}
	}
}