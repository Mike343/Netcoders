using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserBanExpiredSpecificationTest
	{
		private IQueryable<UserBan> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<UserBan>
			{
				new UserBan { Expire = new DateTime(2010, 1, 9, 1, 1, 1), User = new User { Id = 1 } }, 
				new UserBan { Expire = new DateTime(2020, 1, 1, 1, 1, 1), User = new User { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserBanExpiredSpecification_SatisfyEntityFrom()
		{
			var specification = new UserBanExpiredSpecification();
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.IsNotNull(result.Expire, "NotNull");
			Assert.AreEqual(2010, result.Expire.Value.Year, "Expire");
		}

		[Test]
		public void Test_UserBanExpiredSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserBanExpiredSpecification();
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}