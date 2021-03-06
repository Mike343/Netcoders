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

namespace Coders.Models.Users
{
	public interface IUser
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		int Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the slug.
		/// </summary>
		/// <value>
		/// The slug.
		/// </value>
		string Slug { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		string Title { get; set; }

		/// <summary>
		/// Gets or sets the signature parsed.
		/// </summary>
		/// <value>
		/// The signature parsed.
		/// </value>
		string SignatureParsed { get; set; }

		/// <summary>
		/// Gets or sets the avatar.
		/// </summary>
		/// <value>
		/// The avatar.
		/// </value>
		UserAvatar Avatar { get; set; }
	}
}