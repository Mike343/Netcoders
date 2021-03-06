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

namespace Coders.Specifications
{
	public abstract class CompositeSpecification<TEntity> : Specification<TEntity> 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeSpecification&lt;TEntity&gt;"/> class.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		protected CompositeSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>
		/// Gets or sets the left.
		/// </summary>
		/// <value>The left.</value>
		public ISpecification<TEntity> Left
		{
			get; 
			private set; 
		}

		/// <summary>
		/// Gets or sets the right.
		/// </summary>
		/// <value>The right.</value>
		public ISpecification<TEntity> Right
		{
			get;
			private set;
		}
	}
}