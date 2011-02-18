using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Attachments;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeAttachmentRuleService : IAttachmentRuleService
	{
		public AttachmentRule GetBy(ISpecification<AttachmentRule> specification)
		{
			return new AttachmentRule();
		}

		public AttachmentRule GetById(int id)
		{
			return id == 1 ? new AttachmentRule() : null;
		}

		public IList<string> GetGroups()
		{
			return new List<string>();
		}

		public IList<AttachmentRule> GetAll()
		{
			return new List<AttachmentRule>();
		}

		public IList<AttachmentRule> GetAll(ISpecification<AttachmentRule> specification)
		{
			return new List<AttachmentRule>();
		}

		public IPagedCollection<AttachmentRule> GetPaged(ISpecification<AttachmentRule> specification)
		{
			return new PagedCollection<AttachmentRule>(new List<AttachmentRule>(), 1, 1, 0);
		}

		public AttachmentRule Create()
		{
			return new AttachmentRule();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<AttachmentRule> specification)
		{
			return 0;
		}

		public void Insert(AttachmentRule entity)
		{

		}

		public void Update(AttachmentRule entity)
		{

		}

		public void Delete(AttachmentRule entity)
		{

		}

		public void InsertOrUpdate(AttachmentRule rule)
		{

		}
	}
}