using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserSearchUserSpecificationTest
	{
		private IQueryable<UserSearch> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<UserSearch>
			{
				new UserSearch { UserId = 1 }, 
				new UserSearch { UserId = 2 }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserSearchUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserSearchUserSpecification(1);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.UserId);
		}

		[Test]
		public void Test_UserSearchUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserSearchUserSpecification(1);
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}