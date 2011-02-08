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
using System.Text;
using System.Web;
using System.Web.Security;
using Coders.Authentication;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Models.Users;
#endregion

namespace Coders.Services
{
	public class FormsAuthenticationService : IAuthenticationService
	{
		// fields
		private readonly char[] _consonants = new[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 's', 't', 'v' };
		private readonly char[] _doubleConsonants = new[] { 'c', 'd', 'f', 'g', 'l', 'm', 'n', 'p', 'r', 's', 't' };
		private readonly char[] _vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
		private readonly char[] _numbers = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

		/// <summary>
		/// Initializes a new instance of the <see cref="FormsAuthenticationService"/> class.
		/// </summary>
		/// <param name="emailService">The email service.</param>
		/// <param name="userRepository">The user repository.</param>
		public FormsAuthenticationService(
			IEmailService emailService,
			IRepository<User> userRepository)
		{
			this.EmailService = emailService;
			this.UserRepository = userRepository;

			this.Principal = PrivilegePrincipalPermission.Current;

			if (this.Principal == null)
			{
				return;
			}

			this.Identity = this.Principal.Identity as UserIdentity;
		}

		/// <summary>
		/// Gets or sets the email service.
		/// </summary>
		/// <value>The email service.</value>
		public IEmailService EmailService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the user repository.
		/// </summary>
		/// <value>The user repository.</value>
		public IRepository<User> UserRepository
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets the return URL.
		/// </summary>
		/// <value>The return URL.</value>
		public string ReturnUrl
		{
			get
			{
				return FormsAuthentication.DefaultUrl;
			}
		}

		/// <summary>
		/// Gets the log on URL.
		/// </summary>
		/// <value>The log on URL.</value>
		public string LogOnUrl
		{
			get
			{
				return FormsAuthentication.LoginUrl;
			}
		}

		/// <summary>
		/// Gets or sets the principal.
		/// </summary>
		/// <value>The principal.</value>
		public PrivilegePrincipal Principal
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets the identity.
		/// </summary>
		/// <value>The identity.</value>
		public UserIdentity Identity
		{
			get;
			private set;
		}

		/// <summary>
		/// Generates the password.
		/// </summary>
		/// <returns></returns>
		public string GeneratePassword()
		{
			return GeneratePassword(8);
		}

		/// <summary>
		/// Generates the password using the specified length.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public string GeneratePassword(int length)
		{
			var random = new Random();
			var value = new StringBuilder();
			var consonant = false;

			for (var i = 0; i <= length; i++)
			{
				if (value.Length > 0 && !consonant && random.Next(100) < 10)
				{
					value.Append(_doubleConsonants[random.Next(_doubleConsonants.Length)], 2);
					consonant = true;
				}
				else if (!consonant && random.Next(100) < 90)
				{
					value.Append(_consonants[random.Next(_consonants.Length)]);
					consonant = true;
				}
				else
				{
					value.Append(_vowels[random.Next(_vowels.Length)]);
					consonant = false;
				}
			}

			for (var i = 0; i <= 2; i++)
			{
				value.Append(_numbers[random.Next(_numbers.Length)].ToString());
			}

			value.Length = length + 2;

			return value.ToString();
		}

		/// <summary>
		/// Authenticates the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public bool Authenticate(User user, string password)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			if (user.Password.Equals(password.HashToSha1(user.Salt).Hex()))
			{
				return true;
			}

			#region Legacy Code for Compatibility Reasons
			if (user.Password.Equals(password.HashToMd5().Hex().HashToMd5(user.Salt).Hex()))
			{
				var salt = this.GeneratePassword(Setting.UserPasswordSaltLength.Value);

				user.Password = password.HashToSha1(salt).Hex();
				user.Salt = salt;

				this.UserRepository.Update(user);

				return true;
			}
			#endregion

			return false;
		}

		/// <summary>
		/// Logs the specified user on.
		/// </summary>
		/// <param name="user">The user.</param>
		public void LogOn(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			var context = HttpContext.Current;

			if (context == null)
			{
				return;
			}

			user.LastLogOn = DateTime.Now;

			this.UserRepository.Update(user);

			// get auth cookie
			var auth = FormsAuthentication.GetAuthCookie(user.EmailAddress, true);

			// decrypt auth cookie value
			var current = FormsAuthentication.Decrypt(auth.Value);

			// create new auth ticket
			var ticket = new FormsAuthenticationTicket(
				current.Version,
				current.Name,
				current.IssueDate,
				current.Expiration,
				current.IsPersistent,
				string.Empty);

			// encrypt new auth ticket
			auth.Value = FormsAuthentication.Encrypt(ticket);

			// add auth cookie
			context.Response.Cookies.Add(auth);
		}

		/// <summary>
		/// Logs the current user off.
		/// </summary>
		public void LogOff()
		{
			FormsAuthentication.SignOut();
		}

		/// <summary>
		/// Resets the password for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		public void Reset(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			// reset password
			var password = this.GeneratePassword();
			var salt = this.GeneratePassword(Setting.UserPasswordSaltLength.Value);

			user.Password = password.HashToSha1(salt).Hex();
			user.Salt = salt;
			user.Updated = DateTime.Now;

			// update user
			this.UserRepository.Update(user);

			// send email to user
			this.EmailService.Send(new UserResetPasswordEmail
			{
				Recipient = user.EmailAddress,
				Name = user.Name,
				EmailAddress = user.EmailAddress,
				Password = password
			});
		}

		/// <summary>
		/// Updates the password for the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		public void Update(User user, string password)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			// update password
			var salt = this.GeneratePassword(Setting.UserPasswordSaltLength.Value);

			user.Password = password.HashToSha1(salt).Hex();
			user.Salt = salt;
			user.Updated = DateTime.Now;

			// update user
			this.UserRepository.Update(user);
		}
	}
}