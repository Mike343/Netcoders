using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserRoleRelationUserSpecificationTest
	{
		private IQueryable<UserRoleRelation> Values
		{
			get;
			set;
		}

		[SetUp]
		public void TestInitialize()
		{
			var values = new List<UserRoleRelation>
			{
				new UserRoleRelation { Id = 1, User = new User { Id = 1 } },
				new UserRoleRelation { Id = 2, User = new User { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserRoleRelationUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserRoleRelationUserSpecification(1);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserRoleRelationUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserRoleRelationUserSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}