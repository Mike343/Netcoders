using System.Collections.Generic;
using System.IO;
using System.Web;
using Coders.Collections;
using Coders.Models.Attachments;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeAttachmentService : IAttachmentService
	{
		public Attachment GetBy(ISpecification<Attachment> specification)
		{
			return new Attachment();
		}

		public Attachment GetById(int id)
		{
			return id == 1 ? new Attachment() : null;
		}

		public IList<string> GetFileTypes()
		{
			return new List<string>();
		}

		public AttachmentPending GetPendingById(int id)
		{
			return new AttachmentPending();
		}

		public IList<Attachment> GetAll()
		{
			return new List<Attachment>();
		}

		public IList<Attachment> GetAll(ISpecification<Attachment> specification)
		{
			return new List<Attachment>();
		}

		public IList<AttachmentPending> GetPending(ISpecification<AttachmentPending> specification)
		{
			return new List<AttachmentPending>();
		}

		public IPagedCollection<Attachment> GetPaged(ISpecification<Attachment> specification)
		{
			return new PagedCollection<Attachment>(new List<Attachment>(), 1, 1, 0);
		}

		public Attachment Create()
		{
			return new Attachment();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<Attachment> specification)
		{
			return 0;
		}

		public FileInfo GetThumbnail(Attachment attachment, int width, int height)
		{
			return null;
		}

		public bool Process(AttachmentPending pending, IList<AttachmentPending> items, HttpPostedFileBase source)
		{
			return true;
		}

		public string GetPath()
		{
			return string.Empty;
		}

		public void Insert(Attachment entity)
		{

		}

		public void Update(Attachment entity)
		{

		}

		public void Delete(Attachment entity)
		{

		}

		public void InsertPending(AttachmentPending pending)
		{

		}

		public void Delete(Attachment attachment, bool soft)
		{

		}

		public void DeletePending(AttachmentPending pending)
		{

		}

		public void DeletePending(AttachmentPending pending, bool cascade)
		{

		}
	}
}