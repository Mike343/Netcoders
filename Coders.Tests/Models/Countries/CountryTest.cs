using System.Collections.Generic;
using Coders.Models.Countries;
using NUnit.Framework;

namespace Coders.Tests.Models.Countries
{
	[TestFixture]
	public class CountryTest
	{
		[Test]
		public void Test_Country_Cache()
		{
			var countries = new List<Country>
			{
				new Country { Title = "United States" }
			};

			Country.Cache(countries);

			Assert.AreEqual(1, Country.Countries.Count);
		}
	}
}