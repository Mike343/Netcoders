using System;
using System.Collections.Generic;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserSearchCreateTest
	{
		[Test]
		public void Test_UserSearchCreate_Initialize()
		{
			var value = new UserSearchCreate();

			value.Initialize(new List<UserSearch>());

			Assert.IsNotNull(value.Searches, "Searches");
		}

		[Test]
		public void Test_UserSearchCreate_ValueToModel()
		{
			var date = DateTime.Now;

			var value = new UserSearchCreate
			{
				Reputation = 1,
				Name = "test",
				EmailAddress = "test2",
				Title = "test3",
				Save = true,
				NameExact = true,
				ReputationAtLeast = true,
				CreatedBefore = date,
				CreatedAfter = date.AddDays(1),
				LastVisitBefore = date.AddDays(2),
				LastVisitAfter = date.AddDays(3),
				LastLogOnBefore = date.AddDays(4),
				LastLogOnAfter = date.AddDays(5),
			};

			var search = new UserSearch();

			value.ValueToModel(search);

			Assert.AreEqual(1, search.Reputation, "Reputation");
			Assert.AreEqual("test", search.Name, "Name");
			Assert.AreEqual("test2", search.EmailAddress, "EmailAddress");
			Assert.AreEqual("test3", search.Title, "Title");
			Assert.IsTrue(search.NameExact, "NameExact");
			Assert.IsTrue(search.ReputationAtLeast, "ReputationAtLeast");

			Assert.IsNotNull(search.CreatedBefore, "CreatedBefore");
			Assert.AreEqual(date, search.CreatedBefore.Value, "CreatedBefore Date");
			Assert.IsNotNull(search.CreatedAfter, "CreatedAfter");
			Assert.AreEqual(date.AddDays(1), search.CreatedAfter.Value, "CreatedAfter Date");

			Assert.IsNotNull(search.LastVisitBefore, "LastVisitBefore");
			Assert.AreEqual(date.AddDays(2), search.LastVisitBefore.Value, "LastVisitBefore Date");
			Assert.IsNotNull(search.LastVisitAfter, "LastVisitAfter");
			Assert.AreEqual(date.AddDays(3), search.LastVisitAfter.Value, "LastVisitAfter Date");

			Assert.IsNotNull(search.LastLogOnBefore, "LastLogOnBefore");
			Assert.AreEqual(date.AddDays(4), search.LastLogOnBefore.Value, "LastLogOnefore Date");
			Assert.IsNotNull(search.LastLogOnAfter, "LastLogOnAfter");
			Assert.AreEqual(date.AddDays(5), search.LastLogOnAfter.Value, "LastLogOnAfter Date");
		}

		[Test]
		public void Test_UserSearchCreate_Validate()
		{
			var value = new UserSearchCreate
			{
				Save = true
			};

			value.Validate();

			Assert.AreEqual(1, value.Errors.Count, "Errors");
		}
	}
}