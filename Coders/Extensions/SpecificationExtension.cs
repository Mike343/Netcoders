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
using System.Linq;
using System.Linq.Expressions;
using Coders.Specifications;
#endregion

namespace Coders.Extensions
{
	public static class SpecificationExtension
	{
		/// <summary>
		/// Composes the specified first.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="merge">The merge.</param>
		/// <returns></returns>
		public static Expression<T> Compose<T>(this LambdaExpression first, Expression<T> second, Func<Expression, Expression, Expression> merge)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}

			if (merge == null)
			{
				throw new ArgumentNullException("merge");
			}

			var map = first.Parameters.Select((v, i) => new { v, p = second.Parameters[i] }).ToDictionary(x => x.p, k => k.v);
			var body = SpecificationParameterBinder.ReplaceParameters(map, second.Body);

			return Expression.Lambda<T>(merge(first.Body, body), first.Parameters);
		}

		/// <summary>
		/// Ands the specified first.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> And<T>(this LambdaExpression first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.And);

		}

		/// <summary>
		/// Ors the specified first.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> Or<T>(this LambdaExpression first, Expression<Func<T, bool>> second)
		{
			return first.Compose(second, Expression.Or);
		}
	} 
}