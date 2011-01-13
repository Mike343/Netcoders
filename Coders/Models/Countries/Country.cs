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
#endregion

namespace Coders.Models.Countries
{
	public class Country : EntityBase
	{
		// countries cache
		private static readonly IList<Country> _countries = new List<Country>();

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public virtual string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the slug.
		/// </summary>
		/// <value>The slug.</value>
		public virtual string Slug
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>The code.</value>
		public virtual string Code
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the countries.
		/// </summary>
		/// <value>The countries.</value>
		public static IList<Country> Countries
		{
			get
			{
				return _countries;
			}
		}

		/// <summary>
		/// Caches the specified countries.
		/// </summary>
		/// <param name="countries">The countries.</param>
		public static void Cache(IList<Country> countries)
		{
			if (countries == null)
			{
				throw new ArgumentNullException("countries");
			}

			lock (_countries)
			{
				_countries.Clear();

				foreach (var country in countries)
				{
					_countries.Add(country);
				}
			}
		}
	}
}