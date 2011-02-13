using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Users
{
	[TestFixture]
	public class UserAvatarUserSpecificationTest
	{
		private IQueryable<UserAvatar> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<UserAvatar>
			{
				new UserAvatar { UserId = 1 }, 
				new UserAvatar { UserId = 2 }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_UserAvatarUserSpecification_SatisfyEntityFrom()
		{
			var specification = new UserAvatarUserSpecification(1);
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual(1, result.UserId);
		}

		[Test]
		public void Test_UserAvatarUserSpecification_SatisfyEntitiesFrom()
		{
			var specification = new UserAvatarUserSpecification(1);

			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}