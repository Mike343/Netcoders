using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserRolePrivilegeUserSpecificationTest
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
				new UserRoleRelation { Id = 1, User = new User { Id = 1 } },
				new UserRoleRelation { Id = 2, User = new User { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserRolePrivilegeUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserRoleRelationUserSpecification(1);

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[TestMethod]
		public void Test_UserRolePrivilegeUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserRoleRelationUserSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}