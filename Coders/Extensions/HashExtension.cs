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
using System.Security.Cryptography;
using System.Text;
#endregion

namespace Coders.Extensions
{
	public static class HashExtension
	{
		/// <summary>
		/// Hashes to sha1.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static byte[] HashToSha1(this string value)
		{
			using (var algorithm = SHA1.Create())
			{
				return Hash(algorithm, value);
			}
		}

		/// <summary>
		/// Hashes to sha1.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static byte[] HashToSha1(this string value, string salt)
		{
			using (var algorithm = SHA1.Create())
			{
				return Hash(algorithm, value, salt);
			}
		}

		/// <summary>
		/// Hashes to sha256.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static byte[] HashToSha256(this string value)
		{
			using (var algorithm = SHA256.Create())
			{
				return Hash(algorithm, value);
			}
		}

		/// <summary>
		/// Hashes to sha256.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static byte[] HashToSha256(this string value, string salt)
		{
			using (var algorithm = SHA256.Create())
			{
				return Hash(algorithm, value, salt);
			}
		}

		/// <summary>
		/// Hashes to sha384.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static byte[] HashToSha384(this string value)
		{
			using (var algorithm = SHA384.Create())
			{
				return Hash(algorithm, value);
			}
		}

		/// <summary>
		/// Hashes to sha384.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static byte[] HashToSha384(this string value, string salt)
		{
			using (var algorithm = SHA384.Create())
			{
				return Hash(algorithm, value, salt);
			}
		}

		/// <summary>
		/// Hashes to sha512.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static byte[] HashToSha512(this string value)
		{
			using (var algorithm = SHA512.Create())
			{
				return Hash(algorithm, value);
			}
		}

		/// <summary>
		/// Hashes to sha512.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static byte[] HashToSha512(this string value, string salt)
		{
			using (var algorithm = SHA512.Create())
			{
				return Hash(algorithm, value, salt);
			}
		}

		/// <summary>
		/// Hashes to MD5.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static byte[] HashToMd5(this string value)
		{
			using (var algorithm = MD5.Create())
			{
				return Hash(algorithm, value);
			}
		}

		/// <summary>
		/// Hashes to MD5.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static byte[] HashToMd5(this string value, string salt)
		{
			using (var algorithm = MD5.Create())
			{
				return Hash(algorithm, value, salt);
			}
		}

		/// <summary>
		/// Hexes the specified values.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static string Hex(this byte[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			var buffer = new StringBuilder();
			var table = "0123456789abcdef".ToCharArray();

			foreach (var value in values)
			{
				buffer.Append(table[value >> 4]);
				buffer.Append(table[value & 15]);
			}

			return buffer.ToString();
		}

		/// <summary>
		/// Hashes the specified value using the specified algorithm.
		/// </summary>
		/// <param name="algorithm">The algorithm.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		private static byte[] Hash(HashAlgorithm algorithm, string value)
		{
			return algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
		}

		/// <summary>
		/// Hashes the specified value using the specified algorithm and salt.
		/// </summary>
		/// <param name="algorithm">The algorithm.</param>
		/// <param name="value">The value.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		private static byte[] Hash(HashAlgorithm algorithm, string value, string salt)
		{
			return algorithm.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(value, salt)));
		}
	}
}