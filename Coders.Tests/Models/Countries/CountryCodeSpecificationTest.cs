using System.Collections.Generic;
using System.Linq;
using Coders.Models.Countries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Countries
{
	[TestClass]
	public class CountryCodeSpecificationTest
	{
		private IQueryable<Country> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<Country>
			{
				new Country { Code = "us" }, 
				new Country { Code = "ca" }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_CountryCodeSpecification_SatisfyEntityFrom()
		{
			var specification = new CountryCodeSpecification("us");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("us", result.Code);
		}

		[TestMethod]
		public void Test_CountryCodeSpecification_SatisfyEntitiesFrom()
		{
			var specification = new CountryCodeSpecification("us");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}