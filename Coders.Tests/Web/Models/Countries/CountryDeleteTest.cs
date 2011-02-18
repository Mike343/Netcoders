using Coders.Models.Countries;
using Coders.Web.Models.Countries;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Countries
{
	[TestFixture]
	public class CountryDeleteTest
	{
		[Test]
		public void Test_CountryDelete()
		{
			var value = new CountryDelete(
				new Country
				{
					Id = 1, 
					Title = "test"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
		}
	}
}