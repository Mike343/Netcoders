using System.Collections.Generic;
using Coders.Collections;
using Coders.Models.Settings;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeSettingService : ISettingService
	{
		public Setting GetBy(ISpecification<Setting> specification)
		{
			return new Setting();
		}

		public Setting GetById(int id)
		{
			return id == 1 ? new Setting() : null;
		}

		public IList<string> GetGroups()
		{
			return new List<string>();
		}

		public IList<Setting> GetAll()
		{
			return new List<Setting> { new Setting { Key = "test", Value = "value" } };
		}

		public IList<Setting> GetAll(ISpecification<Setting> specification)
		{
			return new List<Setting> { new Setting { Key = "test", Value = "value" } };
		}

		public IPagedCollection<Setting> GetPaged(ISpecification<Setting> specification)
		{
			return new PagedCollection<Setting>(new List<Setting> { new Setting { Key = "test", Value = "value" } }, 1, 1, 0);
		}

		public Setting Create()
		{
			return new Setting();
		}

		public int Count()
		{
			return 0;
		}

		public int Count(ISpecification<Setting> specification)
		{
			return 0;
		}

		public void Insert(Setting entity)
		{

		}

		public void Update(Setting entity)
		{

		}

		public void Delete(Setting entity)
		{

		}

		public void Rebuild()
		{
			Setting.Rebuild(this.GetAll());
		}

		public void InsertOrUpdate(Setting setting)
		{

		}

		public void Update(string key, object value)
		{

		}
	}
}