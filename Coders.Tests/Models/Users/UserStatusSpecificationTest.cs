using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Coders.Models.Users.Enums;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserStatusSpecificationTest
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
				new User { Id = 1, Status = UserStatus.Activated }, 
				new User { Id = 2, Status = UserStatus.Pending }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserStatusSpecification_SatisfyEntityFrom()
		{
			var specification = new UserStatusSpecification(UserStatus.Activated);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.Id);
		}

		[Test]
		public void Test_UserStatusSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserStatusSpecification(UserStatus.Activated);
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}