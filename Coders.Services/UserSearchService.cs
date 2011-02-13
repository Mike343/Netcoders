#region License
//	Author: Mike Geise - http://www.netcoders.net
//	Copyright (c) 2010, Mike Geise
//
//	Licensed under the Apache License, Version 2.0 (the "License");
//	you may not use this file except in compliance with the License.
//	You may obtain a copy of the License at
//
//		http://www.apache.org/licenses/LICENSE-2.0
//
//	Unless required by applicable law or agreed to in writing, software
//	distributed under the License is distributed on an "AS IS" BASIS,
//	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//	See the License for the specific language governing permissions and
//	limitations under the License.
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using Coders.Collections;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Services.Lucene;
using Coders.Specifications;
#endregion

namespace Coders.Services
{
	public class UserSearchService : EntityService<UserSearch>, IUserSearchService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserSearchService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="userService">The user service.</param>
		/// <param name="settingService">The setting service.</param>
		/// <param name="userSearchRepository">The user search repository.</param>
		public UserSearchService(
			IAuthenticationService authenticationService,
			IUserService userService,
			ISettingService settingService,
			IRepository<UserSearch> userSearchRepository)
			: base(userSearchRepository)
		{
			this.AuthenticationService = authenticationService;
			this.UserService = userService;
			this.SettingService = settingService;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user service.
		/// </summary>
		/// <value>The user service.</value>
		public IUserService UserService
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets or sets the setting service.
		/// </summary>
		/// <value>The setting service.</value>
		public ISettingService SettingService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the search results.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public IPagedCollection<User> GetResults(UserSearch search, ISpecification<UserSearch> specification)
		{
			if (search == null)
			{
				throw new ArgumentNullException("search");
			}

			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			search.Updated = DateTime.Now;

			this.Update(search);

			var searcher = new UserSearcher(this.UserService);

			searcher.Query(search, specification);

			if (searcher.Results != null && searcher.Results.Count > 0)
			{
				var users = this.UserService.GetAll(new UserIdentifiersSpecification(searcher.Results));

				return new PagedCollection<User>(
					users, 
					specification.PageOrDefault, 
					specification.LimitOrDefault, 
					searcher.Total
				);
			}

			return new PagedCollection<User>(
				new List<User>(), 
				specification.PageOrDefault, 
				specification.LimitOrDefault, 
				0
			);
		}

		/// <summary>
		/// Rebuilds the search index.
		/// </summary>
		public void Rebuild()
		{
			var users = this.UserService.GetAll(
				new UserUpdatedGreaterThanSpecification(
					Setting.UserSearchIndexUpdated.Value
				)
			);

			var created = Setting.UserSearchIndexCreated.Value;
			var indexer = new UserIndexer();

			indexer.CreateOrUpdate(users, created);

			if (!created)
			{
				this.SettingService.Update(Setting.UserSearchIndexCreated.Key, true);
			}

			this.SettingService.Update(Setting.UserSearchIndexUpdated.Key, DateTime.Now);
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns></returns>
		public override UserSearch Create()
		{
			var search = new UserSearch
			{
				Created = DateTime.Now
			};

			search.Updated = search.Created;

			return search;
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(UserSearch entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			var identity = this.AuthenticationService.Identity;

			if (identity.IsAuthenticated())
			{
				if (!string.IsNullOrEmpty(entity.Title))
				{
					entity.UserId = identity.Id;
				}
			}

			base.Insert(entity);
		}

		/// <summary>
		/// Deletes the expired searches.
		/// </summary>
		public void DeleteExpired()
		{
			var searches = this.GetAll(new UserSearchExpiredSpecification());

			foreach (var search in searches)
			{
				this.Delete(search);
			}
		}
	}
}