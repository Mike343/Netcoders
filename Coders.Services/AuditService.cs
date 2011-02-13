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
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Coders.Collections;
using Coders.Extensions;
using Coders.Models;
using Coders.Models.Common;
using Coders.Models.Common.Enums;
using Coders.Models.Users;
using Coders.Specifications;
#endregion

namespace Coders.Services
{
	public class AuditService<T, TK> : IAuditService<T, TK>
		where T : class, IEntity
		where TK : class, IAuditable<T>, new()
	{
		public AuditService(
			IAuthenticationService authenticationService,
			IRepository<User> userRepository,
			IRepository<Audit> auditRepository)
		{
			this.AuthenticationService = authenticationService;
			this.UserRepository = userRepository;
			this.AuditRepository = auditRepository;
		}

		/// <summary>
		/// Gets or sets the authentication service.
		/// </summary>
		/// <value>The authentication service.</value>
		public IAuthenticationService AuthenticationService
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user repository.
		/// </summary>
		/// <value>
		/// The user repository.
		/// </value>
		public IRepository<User> UserRepository
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the audit repository.
		/// </summary>
		public IRepository<Audit> AuditRepository
		{
			get; 
			private set;
		}

		/// <summary>
		/// Gets the all the audits paged by the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public IPagedCollection<Audit> GetPaged(ISpecification<Audit> specification)
		{
			return this.AuditRepository.GetPaged(specification);
		}

		/// <summary>
		/// Audits the specified value.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="action">The action.</param>
		public void Audit(T entity, AuditAction action)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			var identity = this.AuthenticationService.Identity;

			if (!identity.IsAuthenticated())
			{
				return;
			}

			var user = (identity.Id > 0) 
				? this.UserRepository.GetById(identity.Id) 
				: this.UserRepository.GetBy(new UserEmailAddressSpecification(identity.Name));

			var audit = new Audit
			{
				ParentId = entity.Id,
				Action = action,
				Created = DateTime.Now,
				User = user
			};

			using (var writer = new StringWriter(CultureInfo.InvariantCulture))
			{
				var type = entity.GetType();
				var serializer = new XmlSerializer(typeof(TK));
				var value = new TK();

				value.ValueToAudit(entity);

				serializer.Serialize(writer, value);

				audit.Type = type.ToString();
				audit.Data = writer.ToString();

				writer.Close();
			}

			this.AuditRepository.Insert(audit);
		}
	}
}