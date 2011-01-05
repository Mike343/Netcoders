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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using Coders.Extensions;
using Coders.Models.Attachments;
using Coders.Models.Common;
using Coders.Models.Settings;
using Coders.Specifications;
#endregion

namespace Coders.Services
{
	public class AttachmentService : EntityService<Attachment>, IAttachmentService
	{
		// private constants
		private const string ThumbFormat = "{0}_{1}_{2}{3}";

		/// <summary>
		/// Initializes a new instance of the <see cref="AttachmentService"/> class.
		/// </summary>
		/// <param name="fileService">The file service.</param>
		/// <param name="imageService">The image service.</param>
		/// <param name="attachmentRuleService">The attachment rule service.</param>
		/// <param name="attachmentPendingRepository">The attachment pending repository.</param>
		/// <param name="repository">The repository.</param>
		public AttachmentService(
			IFileService fileService,
			IImageService imageService,
			IAttachmentRuleService attachmentRuleService, 
			IAttachmentPendingRepository attachmentPendingRepository, 
			IAttachmentRepository repository)
			: base(repository)
		{
			this.FileService = fileService;
			this.ImageService = imageService;
			this.AttachmentRuleService = attachmentRuleService;
			this.AttachmentPendingRepository = attachmentPendingRepository;
		}

		/// <summary>
		/// Gets or sets the file service.
		/// </summary>
		/// <value>The file service.</value>
		public IFileService FileService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the image service.
		/// </summary>
		/// <value>The image service.</value>
		public IImageService ImageService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the attachment rule service.
		/// </summary>
		/// <value>The attachment rule service.</value>
		public IAttachmentRuleService AttachmentRuleService
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the attachment pending repository.
		/// </summary>
		/// <value>The attachment pending repository.</value>
		public IAttachmentPendingRepository AttachmentPendingRepository
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the pending attachment using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public AttachmentPending GetPendingById(int id)
		{
			return this.AttachmentPendingRepository.GetById(id);
		}

		/// <summary>
		/// Gets the pending attachments using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		public IList<AttachmentPending> GetPending(ISpecification<AttachmentPending> specification)
		{
			return this.AttachmentPendingRepository.GetAll(specification);
		}

		/// <summary>
		/// Gets the attachment thumbnail using the specified width and height.
		/// </summary>
		/// <param name="attachment">The attachment.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public FileInfo GetThumbnail(Attachment attachment, int width, int height)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}

			if (attachment.FileWidth <= width && attachment.FileHeight <= height)
			{
				return new FileInfo(attachment.FullPath.AsPath());
			}

			var name = ThumbFormat.FormatInvariant(
				Path.GetFileNameWithoutExtension(attachment.FileDiskName),
				width,
				height,
				Path.GetExtension(attachment.FileDiskName)
			);

			var path = Path.Combine(attachment.FilePath, name);
			var file = new FileInfo(path.AsPath());

			if (!file.Exists)
			{
				this.ImageService.Resize(
					attachment.FullPath,
					path,
					width,
					height,
					Setting.AttachmentThumbAspect.Value
				);
			}

			return file;
		}

		/// <summary>
		/// Processes the specified pending attachment.
		/// </summary>
		/// <param name="pending">The pending.</param>
		/// <param name="items">The items.</param>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public bool Process(AttachmentPending pending, IList<AttachmentPending> items, HttpPostedFileBase source)
		{
			if (pending == null)
			{
				throw new ArgumentNullException("pending");
			}

			if (items == null)
			{
				throw new ArgumentNullException("items");
			}

			if (source != null && source.ContentLength > 0)
			{
				var path = GetPath();
				var file = this.FileService.Save(source, path);
				var dimensions = this.ImageService.GetImageDimensions(source);

				var attachment = this.Create();

				attachment.FileName = file.FileName;
				attachment.FileDiskName = file.FileDiskName;
				attachment.FileType = file.FileType;
				attachment.FileExtension = file.FileExtension;
				attachment.FilePath = file.FilePath.Replace("\\", "/");
				attachment.FileSize = file.FileSize;
				attachment.FileWidth = dimensions[0];
				attachment.FileHeight = dimensions[1];

				this.Insert(attachment);

				pending.Attachment = attachment;

				this.InsertPending(pending);

				items.Add(pending);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Inserts the pending attachment.
		/// </summary>
		/// <param name="pending">The pending.</param>
		public void InsertPending(AttachmentPending pending)
		{
			this.AttachmentPendingRepository.Insert(pending);
		}

		/// <summary>
		/// Deletes the pending attachment.
		/// </summary>
		/// <param name="pending">The pending.</param>
		public void DeletePending(AttachmentPending pending)
		{
			this.DeletePending(pending, false);
		}

		/// <summary>
		/// Deletes the pending attachment with the option
		/// to delete the actual attachment as well.
		/// </summary>
		/// <param name="pending">The pending.</param>
		/// <param name="cascade">if set to <c>true</c> [cascade].</param>
		public void DeletePending(AttachmentPending pending, bool cascade)
		{
			if (pending == null)
			{
				throw new ArgumentNullException("pending");
			}

			var attachment = pending.Attachment;

			this.AttachmentPendingRepository.Delete(pending);

			if (cascade)
			{
				this.Delete(attachment);
			}
		}

		private static string GetPath()
		{
			var date = DateTime.Now;

			// month/day/year
			var year = date.Year.ToString(CultureInfo.InvariantCulture);
			var month = Path.Combine(year, date.Month.ToString(CultureInfo.InvariantCulture));
			var day = Path.Combine(month, date.Day.ToString(CultureInfo.InvariantCulture));

			// path_to_attachments/month/day/year
			var path = Path.Combine(Setting.AttachmentPath.Value, day);
			var directory = new DirectoryInfo(path.AsPath());

			if (!directory.Exists)
			{
				directory.Create();
			}

			return path;
		}
	}
}