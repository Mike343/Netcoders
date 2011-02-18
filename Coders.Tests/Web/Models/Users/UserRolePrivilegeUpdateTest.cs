using System.Collections.Generic;
using System.Linq;
using Coders.Authentication;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserRolePrivilegeUpdateTest
	{
		[Test]
		public void Test_UserRolePrivilegeUpdate_Initialize()
		{
			var role = new UserRole
			{
				Id = 1,
				Title = "test",
				IsDefault = true,
				Privilege = Privileges.Create
			};

			var value = new UserRolePrivilegeUpdate();
			var roles = new List<UserRole> { role };

			var relations = new List<UserRoleRelation>
			{
				new UserRoleRelation
				{
					Id = 1, 
					Privilege = Privileges.Update, 
					Role = role, 
					User = new User {Id = 1}
				}
			};

			value.Initialize(new User { Id = 1 }, roles, relations);

			Assert.AreEqual(1, value.Values.Count, "Values");
			Assert.AreEqual("test", value.Values[0].Role.Title, "Role.Title");
			Assert.AreEqual(2, value.Values[0].Privileges.Count(x => x.Selected), "Privilege");
		}
	}
}