using Coders.Web.Models;
using NUnit.Framework;

namespace Coders.Tests.Web.Models
{
	[TestFixture]
	public class FilterTest
	{
		[Test]
		public void Test_Filter()
		{
			var filter = new Filter(
				"test", 
				new { test = "other" }, 
				new { test = "other" }
			);

			Assert.AreEqual("test", filter.Title, "Title");
			Assert.IsNotNull(filter.Parameters, "Parameters");
			Assert.IsNotNull(filter.Conditions, "Conditions");
		}
	}
}