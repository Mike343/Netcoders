using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserRolePrivilegeRoleSpecificationTest
	{
		private IQueryable<UserRoleRelation> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<UserRoleRelation>
			{
				new UserRoleRelation { Id = 1, Role = new UserRole { Id = 1 } },
				new UserRoleRelation { Id = 2, Role = new UserRole { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserRolePrivilegeRoleSpecification_SatisfyEntityFrom()
		{
			var specification = new UserRoleRelationRoleSpecification(1);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[TestMethod]
		public void Test_UserRolePrivilegeRoleSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserRoleRelationRoleSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}