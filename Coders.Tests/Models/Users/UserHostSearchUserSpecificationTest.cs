using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserHostSearchUserSpecificationTest
	{
		private IQueryable<UserHostSearch> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<UserHostSearch>
			{
				new UserHostSearch { UserId = 1 }, 
				new UserHostSearch { UserId = 2 }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserHostSearchUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserHostSearchUserSpecification(1);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.UserId);
		}

		[Test]
		public void Test_UserHostSearchUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserHostSearchUserSpecification(1);
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}