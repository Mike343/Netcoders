﻿#region License
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
using System.Linq;
#endregion

namespace Coders.Repositories.Strategies
{
	public interface IFetchingStrategy<TEntity>
	{
		/// <summary>
		/// Gets the fetching strategy for the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		/// <returns></returns>
		IQueryable<TEntity> Get(IQueryable<TEntity> entities);
	}
}