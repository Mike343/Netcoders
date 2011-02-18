using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserBanUserSpecificationTest
	{
		private IQueryable<UserBan> Values
		{
			get;
			set;
		}

		[SetUp]
		public void TestInitialize()
		{
			var values = new List<UserBan>
			{
				new UserBan { Id = 1, User = new User { Id = 1 } }, 
				new UserBan { Id = 2, User = new User { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserBanUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserBanUserSpecification(1);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserBanUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserBanUserSpecification(1);
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}