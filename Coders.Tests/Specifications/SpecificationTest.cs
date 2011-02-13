using System.Collections.Generic;
using System.Linq;
using Coders.Specifications;
using NUnit.Framework;

namespace Coders.Tests.Specifications
{
	[TestFixture]
	public class SpecificationTest
	{
		public Specification<string> Specification
		{
			get; 
			private set; 
		}

		public IList<Product> Products
		{
			get; 
			private set; 
		}

		[SetUp]
		public void Initialize()
		{
			this.Specification = new Specification<string>
			{
				Page = 1, 
				Limit = 10
			};

			this.Products = new List<Product>
			{
				new Product("Lamp", false),
				new Product("Table", true),
				new Product("Chair", true)
			};
		}

		[Test]
		public void Test_Specification_PageOrDefault()
		{
			Assert.AreEqual(this.Specification.PageOrDefault, 1);
		}

		[Test]
		public void Test_Specification_LimitOrDefault()
		{
			Assert.AreEqual(this.Specification.LimitOrDefault, 10);
		}

		[Test]
		public void Test_Specification_First()
		{
			Assert.AreEqual(this.Specification.First, 0);
		}

		[Test]
		public void Test_Specification_Last()
		{
			Assert.AreEqual(this.Specification.Last, 10);
		}

		[Test]
		public void Test_Specification_And()
		{
			var specification = new ProductTitleSpecification("Table").And(new ProductOnSaleSpecification());
			var products = specification.SatisfyEntitiesFrom(this.Products.AsQueryable());

			Assert.AreEqual(products.Count(), 1);
		}

		[Test]
		public void Test_Specification_Or()
		{
			var specification = new ProductTitleSpecification("Lamp").Or(new ProductTitleSpecification("Table"));
			var products = specification.SatisfyEntitiesFrom(this.Products.AsQueryable());

			Assert.AreEqual(products.Count(), 2);
		}

		[Test]
		public void Test_Specification_SatisfyEntityFrom()
		{
			var specification = new ProductOnSaleSpecification();
			var product = specification.SatisfyEntityFrom(this.Products.AsQueryable());

			Assert.IsNotNull(product);
		}

		[Test]
		public void Test_Specification_SatisfyEntitiesFrom()
		{
			var specification = new ProductOnSaleSpecification();
			var products = specification.SatisfyEntitiesFrom(this.Products.AsQueryable());

			Assert.AreEqual(products.Count(), 2);
		}
	}
}