using System;
using System.Web;
using Coders.Extensions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Extensions
{
	[TestFixture]
	public class PathExtensionTest
	{
		private HttpContextBase _context;

		[SetUp]
		public void TestInitialize()
		{
			_context = MockRepository.GenerateMock<HttpContextBase>();

			var request = MockRepository.GenerateMock<HttpRequestBase>();

			request.Stub(x => x.ApplicationPath).Return("site");

			_context.Stub(x => x.Request).Return(request);
		}

		[Test]
		public void Test_PathExtension_AsPath()
		{
			var actual = "{0}\\directory".FormatInvariant(AppDomain.CurrentDomain.BaseDirectory);

			Assert.AreEqual(actual, "directory".AsPath());
		}

		[Test]
		public void Test_PathExtension_AsPath_HttpContext()
		{
			Assert.AreEqual("site\\directory", "directory".AsPath(_context));
		}

		[Test]
		public void Test_PathExtension_AsRoot()
		{
			Assert.AreEqual("site/directory", "directory".AsRoot(_context));
		}
	}
}