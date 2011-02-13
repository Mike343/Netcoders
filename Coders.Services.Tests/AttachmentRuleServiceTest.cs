using Coders.Models.Attachments;
using Coders.Models.Common;
using Coders.Services.Tests.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Services.Tests
{
	[TestFixture]
	public class AttachmentRuleServiceTest
	{
		public IAttachmentRuleService AttachmentRuleService
		{
			get
			{
				return new AttachmentRuleService(
					MockRepository.GenerateMock<IAuditService<AttachmentRule, AttachmentRuleAudit>>(),
					new FakeAttachmentRuleRepository()
				);
			}
		}

		[Test]
		public void Test_AttachmentRuleService_GetGroups()
		{
			var results = this.AttachmentRuleService.GetGroups();

			Assert.AreEqual(1, results.Count, "Count");
			Assert.AreEqual("any", results[0]);
		}

		[Test]
		public void Test_AttachmentRuleService_Insert()
		{
			var rule = new AttachmentRule
			{
				Group = "test"
			};

			this.AttachmentRuleService.Insert(rule);

			Assert.AreEqual(2, rule.Id);
			Assert.AreEqual("test", rule.Group, "Group");
		}

		[Test]
		public void Test_AttachmentRuleService_Update()
		{
			var rule = this.AttachmentRuleService.GetById(1);

			rule.Group = "test3";

			this.AttachmentRuleService.Update(rule);

			Assert.AreEqual(1, rule.Id);
			Assert.AreEqual("test3", rule.Group, "Group");
		}

		[Test]
		public void Test_AttachmentRuleService_InsertOrUpdate()
		{
			var service = this.AttachmentRuleService;
			var rule = new AttachmentRule { Group = "test3" };

			service.InsertOrUpdate(rule);

			Assert.AreEqual(2, rule.Id);
			Assert.AreEqual("test3", rule.Group, "Group");

			var update = service.GetById(2);

			update.Group = "test4";

			service.InsertOrUpdate(update);

			Assert.AreEqual(2, update.Id);
			Assert.AreEqual("test4", update.Group, "Group");
		}

		[Test]
		public void Test_AttachmentRuleService_Delete()
		{
			var service = this.AttachmentRuleService;
			var rule = service.GetById(1);

			service.Delete(rule);

			Assert.AreEqual(0, service.Count());
		}
	}
}