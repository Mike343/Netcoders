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
using Coders.Authentication;
#endregion

namespace Coders.Models.Countries
{
	public class CountryPrivilege : Privilege<Country>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryPrivilege"/> class.
		/// </summary>
		public CountryPrivilege()
			: base(Roles.Countries)
		{

		}
	}
}