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
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Countries;
using Coders.Models.Logs;
using Coders.Strings;
#endregion

namespace Coders.Services
{
	public class CountryService : EntityService<Country>, ICountryService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryService"/> class.
		/// </summary>
		/// <param name="logService">The log service.</param>
		/// <param name="repository">The repository.</param>
		public CountryService(
			ILogService logService, 
			IRepository<Country> repository)
			: base(repository)
		{
			this.LogService = logService;
		}

		/// <summary>
		/// Gets or sets the log service.
		/// </summary>
		/// <value>The log service.</value>
		public ILogService LogService
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

			this.LogService.Log(
				country.Id, 
				Log.Countries,
				((country.Id > 0) ? Logs.CountryUpdated : Logs.CountryCreated).FormatInvariant(country.Title), 
				country
			);
		}
	}
}