using System.Collections.Generic;
using Coders.Models.Users;
using Coders.Web.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Web.Models.Users
{
	[TestFixture]
	public class UserHostSearchCreateTest
	{
		[Test]
		public void Test_UserHostSearchCreate_Initialize()
		{
			var value = new UserHostSearchCreate();

			value.Initialize(new List<UserHostSearch>());

			Assert.IsNotNull(value.Searches, "Searches");
		}

		[Test]
		public void Test_UserHostSearchCreate_Validate()
		{
			var value = new UserHostSearchCreate
			{
				Save = true
			};

			value.Validate();

			Assert.AreEqual(1, value.Errors.Count, "Errors");
		}
	}
}