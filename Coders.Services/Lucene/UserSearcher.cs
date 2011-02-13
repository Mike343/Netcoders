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
using System.Globalization;
using Coders.Extensions;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Specifications;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
#endregion

namespace Coders.Services.Lucene
{
	public class UserSearcher
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserSearcher"/> class.
		/// </summary>
		/// <param name="userService">The user service.</param>
		public UserSearcher(IUserService userService)
		{
			this.UserService = userService;
		}

		/// <summary>
		/// Gets or sets the total.
		/// </summary>
		/// <value>The total.</value>
		public int Total
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>The results.</value>
		public IList<int> Results
		{
			get;
			private set;
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
		/// Queries the index using the specified search.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="specification">The specification.</param>
		public void Query(UserSearch search, ISpecification<UserSearch> specification)
		{
			var searcher = new IndexSearcher(Setting.UserSearchIndexPath.Value);
			var query = new BooleanQuery();

			NameQuery(search, query);
			EmailAddressQuery(search, query);
			ReputationQuery(search, query);
			CreatedQuery(search, query);
			LastVisitQuery(search, query);
			LastLogOnQuery(search, query);

			var hits = searcher.Search(query);

			this.Total = hits.Length();
			this.Results = new List<int>();

			for (var k = specification.First; k < specification.Last && k < hits.Length(); k++)
			{
				var doc = hits.Doc(k);

				if (doc == null)
				{
					continue;
				}

				this.Results.Add(doc.Get("id").AsInt());
			}

			searcher.Close();
		}

		/// <summary>
		/// Builds the name query.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="query">The query.</param>
		private void NameQuery(UserSearch search, BooleanQuery query)
		{
			if (string.IsNullOrEmpty(search.Name))
			{
				return;
			}

			if (search.NameExact)
			{
				var user = this.UserService.GetBy(new UserNameSpecification(search.Name));

				if (user == null)
				{
					return;
				}

				query.Add(new TermQuery(new Term("id", user.Id.ToString())), BooleanClause.Occur.MUST);
			}
			else
			{
				query.Add(new QueryParser("name", new SimpleAnalyzer()).Parse(search.Name), BooleanClause.Occur.SHOULD);
			}
		}

		/// <summary>
		/// Builds the email address query.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="query">The query.</param>
		private static void EmailAddressQuery(UserSearch search, BooleanQuery query)
		{
			if (string.IsNullOrEmpty(search.EmailAddress))
			{
				return;
			}

			query.Add(new QueryParser("email", new SimpleAnalyzer()).Parse(search.EmailAddress), BooleanClause.Occur.SHOULD);
		}

		/// <summary>
		/// Builds the reputation query.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="query">The query.</param>
		private static void ReputationQuery(UserSearch search, BooleanQuery query)
		{
			if (!search.Reputation.HasValue)
			{
				return;
			}

			if (search.ReputationAtLeast)
			{
				query.Add(
					new RangeQuery(
							new Term("reputation", search.Reputation.ToString()),
							new Term("reputation", int.MaxValue.ToString()),
							false
						),
						BooleanClause.Occur.SHOULD
					);
			}
			else
			{
				query.Add(
					new RangeQuery(
							new Term("reputation", "0"),
							new Term("reputation", search.Reputation.ToString()),
							false
						),
						BooleanClause.Occur.SHOULD
					);
			}
		}

		/// <summary>
		/// Builds the created query.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="query">The query.</param>
		private static void CreatedQuery(UserSearch search, BooleanQuery query)
		{
			if (search.CreatedBefore.HasValue)
			{
				query.Add(
					new RangeQuery(
							new Term("created", DateTime.MinValue.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							new Term("created", search.CreatedBefore.Value.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							false
						),
						BooleanClause.Occur.MUST
					);
			}

			if (search.CreatedAfter.HasValue)
			{
				query.Add(
					new RangeQuery(
							new Term("created", search.CreatedAfter.Value.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							new Term("created", DateTime.Now.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							false
						),
						BooleanClause.Occur.MUST
					);
			}
		}

		/// <summary>
		/// Builds the last visit query.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="query">The query.</param>
		private static void LastVisitQuery(UserSearch search, BooleanQuery query)
		{
			if (search.LastVisitBefore.HasValue)
			{
				query.Add(
					new RangeQuery(
							new Term("created", DateTime.MinValue.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							new Term("created", search.LastVisitBefore.Value.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							false
						),
						BooleanClause.Occur.MUST
					);
			}

			if (search.LastVisitAfter.HasValue)
			{
				query.Add(
					new RangeQuery(
							new Term("last_visit", search.LastVisitAfter.Value.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							new Term("last_visit", DateTime.Now.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							false
						),
						BooleanClause.Occur.MUST
					);
			}
		}

		/// <summary>
		/// Builds the last logon query.
		/// </summary>
		/// <param name="search">The search.</param>
		/// <param name="query">The query.</param>
		private static void LastLogOnQuery(UserSearch search, BooleanQuery query)
		{
			if (search.LastLogOnBefore.HasValue)
			{
				query.Add(
					new RangeQuery(
							new Term("last_logon", DateTime.MinValue.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							new Term("last_logon", search.LastLogOnBefore.Value.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							false
						),
						BooleanClause.Occur.MUST
					);
			}

			if (search.LastLogOnAfter.HasValue)
			{
				query.Add(
					new RangeQuery(
							new Term("created", search.LastLogOnAfter.Value.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							new Term("created", DateTime.Now.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture)),
							false
						),
						BooleanClause.Occur.MUST
					);
			}
		}
	}
}