using Coders.Models.Countries;
using Coders.Web.Models.Countries;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Countries
{
	[TestFixture]
	public class CountryCreateOrUpdateTest
	{
		[Test]
		public void Test_CountryCreateOrUpdate()
		{
			var value = new CountryCreateOrUpdate(
				new Country
				{
					Id = 1, 
					Title = "test", 
					Code = "test2"
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
			Assert.AreEqual("test2", value.Code, "Code");
		}

		[Test]
		public void Test_CountryCreateOrUpdate_ValueToModel()
		{
			var value = new CountryCreateOrUpdate
			{
				Title = "test",
				Code = "test2"
			};

			var country = new Country();

			value.ValueToModel(country);

			Assert.AreEqual("test", country.Title, "Title");
			Assert.AreEqual("test2", country.Code, "Code");
		}

		[Test]
		public void Test_CountryCreateOrUpdate_Validate()
		{
			var value = new CountryCreateOrUpdate();

			value.Validate();

			Assert.AreEqual(2, value.Errors.Count, "Errors");
		}
	}
}