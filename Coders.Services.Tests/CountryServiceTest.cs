using Coders.Models.Common;
using Coders.Models.Countries;
using Coders.Services.Tests.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
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

			Assert.AreEqual(1, results.Count);
			Assert.AreEqual(1, Country.Countries.Count);
		}

		[Test]
		public void Test_CountryService_Insert()
		{
			var country = new Country();

			this.CountryService.Insert(country);

			Assert.AreEqual(2, country.Id);
		}

		[Test]
		public void Test_CountryService_Update()
		{
			var country = this.CountryService.GetById(1);

			country.Title = "test3";

			this.CountryService.Update(country);

			Assert.AreEqual(1, country.Id);
			Assert.AreEqual("test3", country.Title, "Title");
		}

		[Test]
		public void Test_CountryService_InsertOrUpdate()
		{
			var service = this.CountryService;
			var country = new Country { Title = "test3" };

			service.InsertOrUpdate(country);

			Assert.AreEqual(2, country.Id);
			Assert.AreEqual("test3", country.Title, "Title");

			var update = service.GetById(2);

			update.Title = "test4";

			service.InsertOrUpdate(update);

			Assert.AreEqual(2, update.Id);
			Assert.AreEqual("test4", update.Title, "Title");
		}

		[Test]
		public void Test_CountryService_Delete()
		{
			var service = this.CountryService;
			var country = service.GetById(1);

			service.Delete(country);

			Assert.AreEqual(0, service.Count());
		}
	}
}