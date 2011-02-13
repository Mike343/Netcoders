using System.Collections.Generic;
using System.Linq;
using Coders.Models.Common;
using Coders.Models.Users;
using NUnit.Framework;

namespace Coders.Tests.Models.Common
{
	[TestFixture]
	public class AuditTypeSpecificationTest
	{
		private IQueryable<Audit> Values
		{
			get;
			set;
		}

		[SetUp]
		public void Initialize()
		{
			var values = new List<Audit>
			{
				new Audit { ParentId = 1, Type = "System.String", User = new User { Id = 1 } }, 
				new Audit { ParentId = 2, Type = "System.Int32", User = new User { Id = 2 } }
			};

			this.Values = values.AsQueryable();
		}

		[Test]
		public void Test_AuditTypeSpecification_SatisfyEntityFrom()
		{
			var specification = new AuditTypeSpecification("System.String");
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("System.String", result.Type);
		}

		[Test]
		public void Test_AuditTypeSpecification_SatisfyEntitiesFrom()
		{
			var specification = new AuditTypeSpecification("System.String");
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}

		[Test]
		public void Test_AuditTypeSpecification_ParentId_SatisfyEntityFrom()
		{
			var specification = new AuditTypeSpecification(1, "System.String");
			var result = specification.SatisfyEntityFrom(this.Values);

			Assert.AreEqual("System.String", result.Type);
		}

		[Test]
		public void Test_AuditTypeSpecification_ParentId_SatisfyEntitiesFrom()
		{
			var specification = new AuditTypeSpecification(1, "System.String");
			var results = specification.SatisfyEntitiesFrom(this.Values);

			Assert.AreEqual(1, results.Count());
		}
	}
}