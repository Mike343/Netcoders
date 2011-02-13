using System;
using System.Runtime.Serialization;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Services.Tests.Fakes;
using Coders.Specifications;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class AuditServiceTest
	{
		public IAuditService<AuditTest, AuditTestAudit> AuditService
		{
			get; 
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			var user = MockRepository.GenerateMock<IRepository<User>>();

			user.Stub(x => x.GetById(1)).Return(new User { Id = 1, Name = "Test", EmailAddress = "Test" });

			this.AuditService = new AuditService<AuditTest, AuditTestAudit>(
				authentication, 
				user, 
				new FakeAuditRepository()
			);
		}

		[Test]
		public void Test_AuditService_Audit()
		{
			var test = new AuditTest
			{
			    Id = 1, 
				Value = "test", 
				Value2 = 10, 
				Value3 = DateTime.Now
			};

			this.AuditService.Audit(test, AuditAction.Create);

			var audit = this.AuditService.GetPaged(new Specification<Audit>());

			Assert.AreEqual(1, audit.Items[0].ParentId);
			Assert.AreEqual(test.GetType().ToString(), audit.Items[0].Type);
			Assert.AreEqual(AuditAction.Create, audit.Items[0].Action);
			Assert.AreEqual("Test", audit.Items[0].User.Name);

			var value = audit.Items[0].GetEntity<AuditTest>();

			Assert.AreEqual("test", value.Value);
			Assert.AreEqual(10, value.Value2);
			Assert.AreEqual(test.Value3, value.Value3);
		}

		public class AuditTest : IEntity
		{
			public int Id
			{
				get;
				set;
			}

			public string Value
			{
				get;
				set;
			}

			public int Value2
			{
				get;
				set;
			}

			public DateTime Value3
			{
				get;
				set;
			}
		}

		[Serializable]
		public class AuditTestAudit : IAuditable<AuditTest>
		{
			public AuditTestAudit()
			{

			}

			protected AuditTestAudit(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}

				this.Value = info.GetString("Value");
				this.Value2 = info.GetInt32("Value2");
				this.Value3 = info.GetDateTime("Value3");
			}

			public string Value
			{
				get;
				set;
			}

			public int Value2
			{
				get;
				set;
			}

			public DateTime Value3
			{
				get;
				set;
			}

			public void ValueToAudit(AuditTest entity)
			{
				if (entity == null)
				{
					throw new ArgumentNullException("entity");
				}

				this.Value = entity.Value;
				this.Value2 = entity.Value2;
				this.Value3 = entity.Value3;
			}

			public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}

				info.AddValue("Value", this.Value);
				info.AddValue("Value2", this.Value2);
				info.AddValue("Value3", this.Value3);
			}
		}
	}
}