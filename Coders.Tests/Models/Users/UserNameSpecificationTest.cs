using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserNameSpecificationTest
	{
		private IQueryable<User> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<User>
			{
				new User { Name = "Test" }, 
				new User { Name = "Test 2" }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserNameSpecification_SatisfyEntityFrom()
		{
			var specification = new UserNameSpecification("Test");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("Test", result.Name);
		}

		[TestMethod]
		public void Test_UserNameSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserNameSpecification("Test");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}