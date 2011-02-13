using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserIdentifiersSpecificationTest
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
				new User { Id = 1 }, 
				new User { Id = 2 }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserIdentifiersSpecification_SatisfyEntityFrom()
		{
			var specification = new UserIdentifiersSpecification(new List<int> { 1 });
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserIdentifiersSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserIdentifiersSpecification(new List<int> { 1, 2 });
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(2, results.Count());
		}
	}
}