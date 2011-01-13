using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserHostAddressSpecificationTest
	{
		private IQueryable<UserHost> Values
		{
			get;
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			var values = new List<UserHost>
			{
				new UserHost { HostAddress = "127.0.0.1" }, 
				new UserHost { HostAddress = "127.0.0.2" }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserHostAddressSpecification_SatisfyEntityFrom()
		{
			var specification = new UserHostAddressSpecification("127.0.0.1");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("127.0.0.1", result.HostAddress);
		}

		[TestMethod]
		public void Test_UserHostAddressSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserHostAddressSpecification("127.0.0.1");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}