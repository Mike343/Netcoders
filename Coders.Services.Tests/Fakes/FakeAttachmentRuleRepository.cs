using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models.Attachments;
using Coders.Specifications;

namespace Coders.Services.Tests.Fakes
{
	public class FakeAttachmentRuleRepository : IAttachmentRuleRepository
	{
		public FakeAttachmentRuleRepository()
		{
			this.Values = new List<AttachmentRule> { 
				new AttachmentRule
				{
					Id = 1,
					FileSize = 12345678,
					FileHeight = 0,
					FileWidth = 0,
					Group = "any",
					FileType = "application/zip",
					FileExtension = "zip"
				}
			};
		}

		private IList<AttachmentRule> Values
		{
			get; 
			set;
		}

		public AttachmentRule GetBy(ISpecification<AttachmentRule> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public AttachmentRule GetById(int id)
		{
			return this.Values.FirstOrDefault(x => x.Id == id);
		}

		public IList<string> GetGroups()
		{
			var entities = this.Values.AsQueryable()
				.GroupBy(x => x.Group)
				.Select(x => x.Key)
				.Distinct()
				.ToList();

			return entities;
		}

		public IList<AttachmentRule> GetAll()
		{
			return this.Values;
		}

		public IList<AttachmentRule> GetAll(ISpecification<AttachmentRule> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList();
		}

		public IPagedCollection<AttachmentRule> GetPaged(ISpecification<AttachmentRule> specification)
		{
			var entities = specification.SatisfyEntitiesFrom(this.Values.AsQueryable());
			var total = this.Count(specification);

			return new PagedCollection<AttachmentRule>(entities.ToList(), specification.PageOrDefault, specification.LimitOrDefault, total);
		}

		public int Count()
		{
			return this.Values.Count();
		}

		public int Count(ISpecification<AttachmentRule> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(AttachmentRule entity)
		{
			var id = this.Values.OrderByDescending(x => x.Id).Select(x => x.Id).First();

			entity.Id = id + 1;

			this.Values.Add(entity);
		}

		public void Update(AttachmentRule entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}

			this.Values.Add(entity);
		}

		public void Delete(AttachmentRule entity)
		{
			if (this.Values.Contains(entity))
			{
				this.Values.Remove(entity);
			}
		}
	}
}