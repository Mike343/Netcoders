using System.Collections.Generic;
using Coders.Models.Countries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Countries
{
	[TestClass]
	public class CountryTest
	{
		[TestMethod]
		public void Test_Country_Cache()
		{
			var countries = new List<Country>
			{
				new Country { Title = "United States" }, 
				new Country { Title = "Canada" }
			};

			Country.Cache(countries);

			Assert.AreEqual(2, Country.Countries.Count);
		}
	}
}