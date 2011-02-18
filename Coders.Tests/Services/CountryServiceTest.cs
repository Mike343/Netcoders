using Coders.Models.Common;
using Coders.Models.Countries;
using Coders.Services;
using Coders.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class CountryServiceTest
	{
		public ICountryService CountryService 
		{ 
			get
			{
				return new CountryService(
					MockRepository.GenerateMock<IAuditService<Country, CountryAudit>>(),
					new FakeCountryRepository()
				);
			}
		}

		[Test]
		public void Test_CountryService_GetAll()
		{
			var results = this.CountryService.GetAll();

			Assert.AreEqual(1, results.Count, "Count");
			Assert.AreEqual(1, Country.Countries.Count, "Cache");
		}

		[Test]
		public void Test_CountryService_Insert()
		{
			var country = new Country
			{
				Title = "Test 123"
			};

			this.CountryService.Insert(country);

			Assert.AreEqual(2, country.Id, "Id");
			Assert.AreEqual("test-123", country.Slug, "Slug");
		}

		[Test]
		public void Test_CountryService_Update()
		{
			var country = this.CountryService.GetById(1);

			country.Title = "test3";

			this.CountryService.Update(country);

			Assert.AreEqual(1, country.Id, "Id");
			Assert.AreEqual("test3", country.Title, "Title");
			Assert.AreEqual("test3", country.Slug, "Slug");
		}

		[Test]
		public void Test_CountryService_InsertOrUpdate()
		{
			var service = this.CountryService;
			var country = new Country { Title = "test3" };

			service.InsertOrUpdate(country);

			Assert.AreEqual(2, country.Id, "Id");
			Assert.AreEqual("test3", country.Title, "Title");
		}

		[Test]
		public void Test_CountryService_InsertOrUpdate_Update()
		{
			var service = this.CountryService;
			var country = service.GetById(1);

			country.Title = "test4";

			service.InsertOrUpdate(country);

			Assert.AreEqual(1, country.Id, "Id");
			Assert.AreEqual("test4", country.Title, "Title");
		}

		[Test]
		public void Test_CountryService_Delete()
		{
			var service = this.CountryService;
			var country = service.GetById(1);

			service.Delete(country);

			Assert.AreEqual(0, service.Count(), "Count");
		}
	}
}