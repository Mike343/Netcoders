using System;
using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserUpdatedGreaterThanSpecificationTest
	{
		private IQueryable<User> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<User>
			{
				new User { Id = 1, Updated = DateTime.Now }, 
				new User { Id = 2, Updated = DateTime.Now.AddMinutes(-25) }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserUpdatedGreaterThanSpecification_SatisfyEntityFrom()
		{
			var specification = new UserUpdatedGreaterThanSpecification(DateTime.Now.AddMinutes(-15));
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserUpdatedGreaterThanSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserUpdatedGreaterThanSpecification(DateTime.Now.AddMinutes(-15));
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}