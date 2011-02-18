using Coders.Authentication;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserRoleCreateOrUpdateTest
	{
		[Test]
		public void Test_UserRoleCreateOrUpdate()
		{
			var value = new UserRoleCreateOrUpdate(
				new UserRole
				{
					Id = 1,
					Title = "test",
					IsDefault = true,
					IsAdministrator = true,
					Privilege = Privileges.Create
				}
			);

			Assert.AreEqual(1, value.Id, "Id");
			Assert.AreEqual("test", value.Title, "Title");
			Assert.AreEqual(true, value.IsDefault, "IsDefault");
			Assert.AreEqual(true, value.IsAdministrator, "IsAdministrator");
			Assert.AreEqual(Privileges.Create, value.Privilege, "Privilege");
		}

		[Test]
		public void Test_UserRoleCreateOrUpdate_ValueToModel()
		{
			var value = new UserRoleCreateOrUpdate
			{
				Title = "test",
				IsDefault = true,
				IsAdministrator = true,
				Privilege = Privileges.Create
			};

			var role = new UserRole();

			value.ValueToModel(role);

			Assert.AreEqual("test", role.Title, "Title");
			Assert.AreEqual(true, role.IsDefault, "IsDefault");
			Assert.AreEqual(true, role.IsAdministrator, "IsAdministrator");
		}

		[Test]
		public void Test_UserRoleCreateOrUpdate_Validate()
		{
			var value = new UserRoleCreateOrUpdate();

			value.Validate();

			Assert.AreEqual(1, value.Errors.Count, "Errors");
		}
	}
}