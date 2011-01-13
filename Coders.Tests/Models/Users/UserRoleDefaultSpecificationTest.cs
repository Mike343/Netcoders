using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserRoleDefaultSpecificationTest
	{
		private IQueryable<UserRole> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<UserRole>
			{
				new UserRole { IsDefault = true }, 
				new UserRole { IsDefault = false }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserRoleDefaultSpecification_SatisfyEntityFrom()
		{
			var specification = new UserRoleDefaultSpecification();

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.IsTrue(result.IsDefault);
		}

		[TestMethod]
		public void Test_UserRoleDefaultSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserRoleDefaultSpecification();

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}