using Coders.Models.Countries;
using NUnit.Framework;

namespace Coders.Tests.Models.Countries
{
	[TestFixture]
	public class CountryAuditTest
	{
		[Test]
		public void Test_CountryAudit_ValueToAudit()
		{
			var audit = new CountryAudit();

			audit.ValueToAudit(new Country
			{
				Title = "test", 
				Code = "test2"
			});

			Assert.AreEqual("test", audit.Title, "Title");
			Assert.AreEqual("test2", audit.Code, "Code");
		}
	}
}