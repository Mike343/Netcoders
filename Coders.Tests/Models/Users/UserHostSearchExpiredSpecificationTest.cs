using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserHostSearchExpiredSpecificationTest
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
				new UserHostSearch { Updated = new DateTime(2010, 1, 9, 1, 1, 1) }, 
				new UserHostSearch { Updated = new DateTime(2020, 1, 1, 1, 1, 1) }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserHostSearchExpiredSpecification_SatisfyEntityFrom()
		{
			var specification = new UserHostSearchExpiredSpecification();
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(2010, result.Updated.Year);
		}

		[Test]
		public void Test_UserHostSearchExpiredSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserHostSearchExpiredSpecification();
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}