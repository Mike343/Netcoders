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
using Coders.Collections;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Specifications;
#endregion

namespace Coders.Services
{
	public class UserHostSearchService : EntityService<UserHostSearch>, IUserHostSearchService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserHostSearchService"/> class.
		/// </summary>
		/// <param name="authenticationService">The authentication service.</param>
		/// <param name="userHostService">The user host service.</param>
		/// <param name="userHostSearchRepository">The user host search repository.</param>
		public UserHostSearchService(
			IAuthenticationService authenticationService, 
			IUserHostService userHostService,
			IRepository<UserHostSearch> userHostSearchRepository)
			: base(userHostSearchRepository)
		{
			this.AuthenticationService = authenticationService;
			this.UserHostService = userHostService;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			private set;
		}

		public IUserHostService UserHostService
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
		public IPagedCollection<UserHost> GetResults(UserHostSearch search, ISpecification<UserHostSearch> specification)
		{
			if (search == null)
			{
				throw new ArgumentNullException("search");
			}

			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			ISpecification<UserHost> spec = null;

			if (search.UserId.HasValue)
			{
				spec = new UserHostUserSpecification(search.UserId.Value);
			}

			if (!string.IsNullOrEmpty(search.HostAddress))
			{
				if (spec == null)
				{
					spec = new UserHostAddressSpecification(search.HostAddress);
				}
				else
				{
					spec = new AndSpecification<UserHost>(
						spec,
						new UserHostAddressSpecification(search.HostAddress)
					);
				}
			}

			if (spec == null)
			{
				spec = new UserHostSpecification();
			}

			spec.Page = specification.Page;
			spec.Limit = specification.Limit;

			return this.UserHostService.GetPaged(spec);
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns></returns>
		public override UserHostSearch Create()
		{
			var search = new UserHostSearch
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
		public override void Insert(UserHostSearch entity)
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
			throw new NotImplementedException();
		}
	}
}