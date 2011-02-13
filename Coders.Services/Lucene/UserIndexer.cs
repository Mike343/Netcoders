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
using System.Collections.Generic;
using System.Globalization;
using Coders.Models.Settings;
using Coders.Models.Users;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
#endregion

namespace Coders.Services.Lucene
{
	public class UserIndexer
	{
		/// <summary>
		/// Runs the indexing process.
		/// </summary>
		/// <param name="users">The users.</param>
		/// <param name="created">if set to <c>true</c> [created].</param>
		public void CreateOrUpdate(IList<User> users, bool created)
		{
			var modifier = new IndexModifier(
				Setting.UserSearchIndexPath.Value,
				new StandardAnalyzer(),
				(!created) ? true : false
			);

			foreach (var user in users)
			{
				if (created)
				{
					modifier.DeleteDocuments(new Term("id", user.Id.ToString()));
				}

				var document = new Document();

				UserToDocument(user, document);

				modifier.AddDocument(document);
			}

			modifier.Optimize();
			modifier.Close();
		}

		/// <summary>
		/// Copies the specified user to the specified document.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="document">The document.</param>
		private static void UserToDocument(User user, Document document)
		{
			document.Add(new Field("id", user.Id.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));
			document.Add(new Field("name", user.Name, Field.Store.YES, Field.Index.TOKENIZED));
			document.Add(new Field("email", user.EmailAddress, Field.Store.YES, Field.Index.TOKENIZED));
			document.Add(new Field("reputation", user.Reputation.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));
			document.Add(new Field("status", user.Status.ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED));
			document.Add(new Field("created", user.Created.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.UN_TOKENIZED));
			document.Add(new Field("last_visit", user.LastVisit.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.UN_TOKENIZED));
			document.Add(new Field("last_logon", user.LastLogOn.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.UN_TOKENIZED));
		}
	}
}