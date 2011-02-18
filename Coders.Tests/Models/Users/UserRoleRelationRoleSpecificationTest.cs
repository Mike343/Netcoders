using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserRoleRelationRoleSpecificationTest
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
				new UserRoleRelation { Id = 1, Role = new UserRole { Id = 1 } },
				new UserRoleRelation { Id = 2, Role = new UserRole { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserRoleRelationRoleSpecification_SatisfyEntityFrom()
		{
			var specification = new UserRoleRelationRoleSpecification(1);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserRoleRelationRoleSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserRoleRelationRoleSpecification(1);
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}