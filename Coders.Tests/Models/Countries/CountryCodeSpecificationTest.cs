using System.Collections.Generic;
using System.Linq;
using Coders.Models.Countries;
using NUnit.Framework;

namespace Coders.Tests.Models.Countries
{
	[TestFixture]
	public class CountryCodeSpecificationTest
	{
		private IQueryable<Country> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<Country>
			{
				new Country { Code = "us" }, 
				new Country { Code = "ca" }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_CountryCodeSpecification_SatisfyEntityFrom()
		{
			var specification = new CountryCodeSpecification("us");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("us", result.Code);
		}

		[Test]
		public void Test_CountryCodeSpecification_SatisfyEntitiesFrom()
		{
			var specification = new CountryCodeSpecification("us");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}