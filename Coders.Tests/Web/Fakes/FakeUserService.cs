using System.Collections.Generic;
using System.Linq;
using Coders.Collections;
using Coders.Models.Countries;
using Coders.Models.TimeZones;
using Coders.Models.Users;
using Coders.Specifications;

namespace Coders.Tests.Web.Fakes
{
	public class FakeUserService : IUserService
	{
		public FakeUserService()
		{
			this.Values = new List<User>
			{
				new User
				{
					Id = 1, 
					Name = "test", 
					EmailAddress = "test@mail.com", 
					Password = "test",
					Preference = new UserPreference
					{
						Country = new Country(), 
						TimeZone = new TimeZone()
					}
				},
				new User
				{
					Id = 2, 
					Name = "other", 
					EmailAddress = "other@mail.com", 
					Password = "other",
					IsProtected = true,
					Preference = new UserPreference
					{
						Country = new Country(), 
						TimeZone = new TimeZone()
					}
				}
			};
		}

		public IList<User> Values
		{
			get; 
			private set;
		}

		public User GetBy(ISpecification<User> specification)
		{
			return specification.SatisfyEntityFrom(this.Values.AsQueryable());
		}

		public User GetById(int id)
		{
			return this.Values.AsQueryable().FirstOrDefault(x => x.Id == id);
		}

		public IList<User> GetAll()
		{
			return this.Values;
		}

		public IList<User> GetAll(ISpecification<User> specification)
		{
			return this.Values;
		}

		public IPagedCollection<User> GetPaged(ISpecification<User> specification)
		{
			return new PagedCollection<User>(specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).ToList(), 1, 10, 0);
		}

		public User Create()
		{
			return new User();
		}

		public int Count()
		{
			return this.Values.Count;
		}

		public int Count(ISpecification<User> specification)
		{
			return specification.SatisfyEntitiesFrom(this.Values.AsQueryable()).Count();
		}

		public void Insert(User entity)
		{
			entity.Preference = new UserPreference
			{
				Country = new Country(),
				TimeZone = new TimeZone()
			};
		}

		public void Update(User entity)
		{

		}

		public void Delete(User entity)
		{

		}

		public UserPreference GetPreferenceBy(ISpecification<UserPreference> specification)
		{
			return new UserPreference();
		}

		public void Insert(User user, UserPreference preference)
		{
			user.Preference = new UserPreference
			{
				Country = new Country(),
				TimeZone = new TimeZone()
			};
		}

		public void InsertPreference(UserPreference preference)
		{

		}

		public void UpdatePreference(UserPreference preference)
		{

		}
	}
}