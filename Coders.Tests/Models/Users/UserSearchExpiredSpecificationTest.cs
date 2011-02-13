using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserSearchExpiredSpecificationTest
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
				new UserSearch { Updated = new DateTime(2010, 1, 9, 1, 1, 1) }, 
				new UserSearch { Updated = DateTime.MaxValue }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserSearchExpiredSpecification_SatisfyEntityFrom()
		{
			var specification = new UserSearchExpiredSpecification();
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(2010, result.Updated.Year);
		}

		[Test]
		public void Test_UserSearchExpiredSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserSearchExpiredSpecification();
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}