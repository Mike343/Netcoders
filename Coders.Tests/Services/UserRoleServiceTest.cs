using System.Collections.Generic;
using Coders.Authentication;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Services;
using Coders.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Services
{
	[TestFixture]
	public class UserRoleServiceTest
	{
		public IUserRoleService UserRoleService
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			this.UserRoleService = new UserRoleService(
				MockRepository.GenerateMock<IAuditService<UserRole, UserRoleAudit>>(),
				new FakeUserRoleRepository(),
				new FakeUserRoleRelationRepository()
			);
		}

		[Test]
		public void Test_UserRoleService_GetPrivileges()
		{
			var privileges = this.UserRoleService.GetPrivileges(new UserRoleRelationUserSpecification(1));

			Assert.AreEqual(1, privileges.Count, "Count");
			Assert.AreEqual(Privileges.Create, privileges[0].Privilege, "Privilege");
		}

		[Test]
		public void Test_UserRoleService_Insert()
		{
			var role = new UserRole { Title = "Test2" };

			this.UserRoleService.Insert(role);

			Assert.AreEqual("test2", role.Slug, "Slug");
		}

		[Test]
		public void Test_UserRoleService_Update()
		{
			var role = this.UserRoleService.GetById(1);

			role.Title = "Test3";

			this.UserRoleService.Insert(role);

			Assert.AreEqual("test3", role.Slug, "Slug");
		}

		[Test]
		public void Test_UserRoleService_InsertOrUpdate()
		{
			var privileges = new List<UserRoleRelationUpdateValue>
			{
			    new UserRoleRelationUpdateValue {Privilege = 2, Selected = true},
			    new UserRoleRelationUpdateValue {Privilege = 4, Selected = true}
			};

			var role = new UserRole { Title = "test4" };

			this.UserRoleService.InsertOrUpdate(role, privileges);

			Assert.AreEqual(2, role.Id, "Id");
			Assert.AreEqual("test4", role.Title, "Title");
			Assert.AreEqual(Privileges.ViewAny | Privileges.Create, role.Privilege, "Privilege");
		}

		[Test]
		public void Test_UserRoleService_InsertOrUpdate_Update()
		{
			var privileges = new List<UserRoleRelationUpdateValue>
			{
			    new UserRoleRelationUpdateValue {Privilege = 2, Selected = true},
			    new UserRoleRelationUpdateValue {Privilege = 4, Selected = true}
			};

			var role = this.UserRoleService.GetById(1);

			role.Title = "test5";

			this.UserRoleService.InsertOrUpdate(role, privileges);

			Assert.AreEqual(1, role.Id, "Id");
			Assert.AreEqual("test5", role.Title, "Title");
			Assert.AreEqual(Privileges.ViewAny | Privileges.Create, role.Privilege, "Privilege");
		}

		[Test]
		public void Test_UserRoleService_UpdatePrivileges()
		{
			var values = new List<UserRoleRelationUpdate>
			{
			    new UserRoleRelationUpdate
			    {
			        RoleId = 1,
			        Selected = true,
			        Privileges = new List<UserRoleRelationUpdateValue>
			        {
			         	new UserRoleRelationUpdateValue {Privilege = 2, Selected = true},
			         	new UserRoleRelationUpdateValue {Privilege = 4, Selected = true},
			        }
			    }
			};

			this.UserRoleService.UpdatePrivileges(new User { Id = 1 }, values);

			var privileges = this.UserRoleService.GetPrivileges(new UserRoleRelationUserSpecification(1));

			Assert.AreEqual(1, privileges.Count, "Count");
			Assert.AreEqual(Privileges.ViewAny | Privileges.Create, privileges[0].Privilege, "Privilege");
		}

		[Test]
		public void Test_UserRoleService_Delete()
		{
			var role = this.UserRoleService.GetById(1);

			this.UserRoleService.Delete(role);

			Assert.AreEqual(0, this.UserRoleService.Count(), "Count");
		}
	}
}