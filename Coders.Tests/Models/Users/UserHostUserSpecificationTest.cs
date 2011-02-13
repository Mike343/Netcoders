using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserHostUserSpecificationTest
	{
		private IQueryable<UserHost> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<UserHost>
			{
				new UserHost { User = new User { Id = 1 } }, 
				new UserHost { User = new User { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserHostUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserHostUserSpecification(1);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.User.Id);
		}

		[Test]
		public void Test_UserHostUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserHostUserSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}