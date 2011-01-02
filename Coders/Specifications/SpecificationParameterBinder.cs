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
using System.Linq.Expressions;
#endregion

namespace Coders.Specifications
{
	public class SpecificationParameterBinder : ExpressionVisitor
	{
		// private fields
		private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

		/// <summary>
		/// Initializes a new instance of the <see cref="SpecificationParameterBinder"/> class.
		/// </summary>
		/// <param name="map">The map.</param>
		public SpecificationParameterBinder(Dictionary<ParameterExpression, ParameterExpression> map)
		{
			_map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
		}

		/// <summary>
		/// Replaces the parameters.
		/// </summary>
		/// <param name="map">The map.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression expression)
		{
			return new SpecificationParameterBinder(map).Visit(expression);
		}

		/// <summary>
		/// Visits the <see cref="T:System.Linq.Expressions.ParameterExpression"/>.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>
		/// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
		/// </returns>
		protected override Expression VisitParameter(ParameterExpression node)
		{
			ParameterExpression replacement;

			if (_map.TryGetValue(node, out replacement))
			{
				node = replacement;
			}

			return base.VisitParameter(node);
		}
	}
}