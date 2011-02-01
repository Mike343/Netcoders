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

namespace Coders.Models.Logs
{
	public class LogGroupSpecification : LogSpecification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogGroupSpecification"/> class.
		/// </summary>
		/// <param name="group">The group.</param>
		public LogGroupSpecification(string group) 
			: base(x => x.GroupKey == group)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogGroupSpecification"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="group">The group.</param>
		public LogGroupSpecification(int id, string group)
			: base(x => x.GroupId == id && x.GroupKey == group)
		{

		}
	}
}