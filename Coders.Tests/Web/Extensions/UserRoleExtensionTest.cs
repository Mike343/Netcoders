using System.Collections.Generic;
using System.Linq;
using Coders.Models.Users;
using Coders.Web.Extensions;
using NUnit.Framework;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class UserRoleExtensionTest
	{
		public IList<UserRoleRelation> Privileges
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			this.Privileges = new List<UserRoleRelation>
			{
				new UserRoleRelation
				{
					Id = 1,
					Privilege = Coders.Authentication.Privileges.Create,
					Role = new UserRole {Title = "Test"},
					User = new User {Id = 1, Name = "Test"}
				}
			};
		}

		[Test]
		public void Test_UserRoleExtension_Has()
		{
			var result = this.Privileges.Has("Test");
			var result2 = this.Privileges.Has("Test", Coders.Authentication.Privileges.Create);

			Assert.IsTrue(result, "First Result");
			Assert.IsTrue(result2, "Second Result");
		}

		[Test]
		public void Test_UserRoleExtension_Has_Dictionary()
		{
			var dictionary = this.Privileges.ToDictionary(x => x.Role.Title);

			var result = dictionary.Has("Test", 4);
			var result2 = dictionary.Has("Test", Coders.Authentication.Privileges.Create);

			Assert.IsTrue(result, "First Result");
			Assert.IsTrue(result2, "Second Result");
		}

		[Test]
		public void Test_UserRoleExtension_GetTitleClass()
		{
			var value = new UserRoleRelationUpdate
			{
				Role = new UserRole()
			};

			Assert.AreEqual(
				"role", 
				value.GetTitleClass(), 
				"Not Selected"
			);

			value.Selected = true;

			Assert.AreEqual(
				"role role-selected", 
				value.GetTitleClass(), 
				"Selected"
			);

			value.Selected = false;
			value.Role.IsAdministrator = true;

			Assert.AreEqual(
				"role role-administrator", 
				value.GetTitleClass(), 
				"Administrator Not Selected"
			);

			value.Selected = true;

			Assert.AreEqual(
				"role role-administrator role-administrator-selected", 
				value.GetTitleClass(), 
				"Administrator Selected"
			);
		}

		[Test]
		public void Test_UserRoleExtension_GetCheckBoxClass()
		{
			var value = new UserRoleRelationUpdate
			{
				Role = new UserRole()
			};

			var value2 = new UserRoleRelationUpdateValue();

			Assert.AreEqual(
				"checkbox", 
				value.GetCheckBoxClass(value2), 
				"Not Selected"
			);

			value2.Selected = true;

			Assert.AreEqual(
				"checkbox checkbox-selected", 
				value.GetCheckBoxClass(value2), 
				"Selected"
			);

			value.Role.IsAdministrator = true;
			value2.Selected = false;

			Assert.AreEqual(
				"checkbox checkbox-administrator", 
				value.GetCheckBoxClass(value2), 
				"Administrator Not Selected"
			);

			value2.Selected = true;

			Assert.AreEqual(
				"checkbox checkbox-administrator checkbox-administrator-selected", 
				value.GetCheckBoxClass(value2), 
				"Administrator Selected"
			);
		}
	}
}