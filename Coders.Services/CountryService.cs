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
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Countries;
#endregion

namespace Coders.Services
{
	public class CountryService : EntityService<Country>, ICountryService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryService"/> class.
		/// </summary>
		/// <param name="auditService">The audit service.</param>
		/// <param name="countryRepository">The country repository.</param>
		public CountryService(
			IAuditService<Country, CountryAudit> auditService,
			IRepository<Country> countryRepository)
			: base(countryRepository)
		{
			this.AuditService = auditService;
		}

		/// <summary>
		/// Gets the audit service.
		/// </summary>
		public IAuditService<Country, CountryAudit> AuditService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public override IList<Country> GetAll()
		{
			if (Country.Countries.Count > 0)
			{
				return Country.Countries;
			}

			var countries = base.GetAll();

			Country.Cache(countries);

			return countries;
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Insert(Country entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Insert(entity);

			this.AuditService.Audit(entity, AuditAction.Create);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(Country entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Update(entity);

			this.AuditService.Audit(entity, AuditAction.Update);
		}

		/// <summary>
		/// Inserts or updates the specified country.
		/// </summary>
		/// <param name="country">The country.</param>
		public void InsertOrUpdate(Country country)
		{
			if (country == null)
			{
				throw new ArgumentNullException("country");
			}

			if (country.Id > 0)
			{
				this.Update(country);
			}
			else
			{
				this.Insert(country);
			}
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(Country entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			base.Delete(entity);

			this.AuditService.Audit(entity, AuditAction.Delete);
		}
	}
}