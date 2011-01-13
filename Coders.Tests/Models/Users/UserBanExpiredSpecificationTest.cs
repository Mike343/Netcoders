using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserBanExpiredSpecificationTest
	{
		private IQueryable<UserBan> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<UserBan>
			{
				new UserBan { Expire = new DateTime(2011, 1, 9, 1, 1, 1)  }, 
				new UserBan { Expire = new DateTime(2020, 1, 1, 1, 1, 1) }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserBanExpiredSpecification_SatisfyEntityFrom()
		{
			var specification = new UserBanExpiredSpecification();

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.IsNotNull(result.Expire);
			Assert.AreEqual(2011, result.Expire.Value.Year);
		}

		[TestMethod]
		public void Test_UserBanExpiredSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserBanExpiredSpecification();

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}