using Coders.Specifications;

namespace Coders.Tests.Specifications
{
	public class Product
	{
		public Product(string title, bool isOnSale)
		{
			this.Title = title;
			this.IsOnSale = isOnSale;
		}

		public string Title
		{
			get; 
			set; 
		}

		public bool IsOnSale
		{
			get; 
			set; 
		}
	}

	public class ProductTitleSpecification : Specification<Product>
	{
		public ProductTitleSpecification(string title)
			: base(x => x.Title == title)
		{

		}
	}

	public class ProductOnSaleSpecification : Specification<Product>
	{
		public ProductOnSaleSpecification() 
			: base(x => x.IsOnSale)
		{
			
		}
	}
}