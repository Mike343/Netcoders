using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserProtectedSpecificationTest
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
				new User { Id = 1, Name = "test", IsProtected = true }, 
				new User { Id = 2, Name = "test2", IsProtected = false }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserProtectedSpecification_SatisfyEntityFrom()
		{
			var specification = new UserProtectedSpecification("test");
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserProtectedSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserProtectedSpecification("test");
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}