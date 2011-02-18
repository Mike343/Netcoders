using System.Collections.Generic;
using Coders.Collections;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeAuditService<T, TK> : IAuditService<T, TK>
		where T : class, IEntity
		where TK : class, IAuditable<T>, new()
	{
		public IPagedCollection<Audit> GetPaged(ISpecification<Audit> specification)
		{
			return new PagedCollection<Audit>(new List<Audit>(), 1, 1, 0);
		}

		public void Audit(T entity, AuditAction action)
		{

		}
	}
}