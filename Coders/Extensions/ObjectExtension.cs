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
using System.Globalization;
#endregion

namespace Coders.Extensions
{
	public static class ObjectExtension
	{
		/// <summary>
		/// Returns the specified string as an integer.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static int AsInt(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int result;

				var success = int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);

				if (success)
				{
					return result;
				}
			}

			return 0;
		}

		/// <summary>
		/// Returns the specified string as an boolean.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool AsBoolean(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				bool result;

				var success = bool.TryParse(value, out result);

				if (success)
				{
					return result;
				}
			}

			return false;
		}
	}
}