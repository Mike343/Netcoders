﻿using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Models.Users
{
	[TestClass]
	public class UserEmailAddressSpecificationTest
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
				new User { EmailAddress = "test@test.com" }, 
				new User { EmailAddress = "test@other.com" }
			};

			this.Values = values.AsQueryable();
		}

		[TestMethod]
		public void Test_UserEmailAddressSpecification_SatisfyEntityFrom()
		{
			var specification = new UserEmailAddressSpecification("test@test.com");

			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("test@test.com", result.EmailAddress);
		}

		[TestMethod]
		public void Test_UserEmailAddressSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserEmailAddressSpecification("test@test.com");

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}