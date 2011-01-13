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
using System.IO;
using System.Web.Mvc;
using Coders.Extensions;
using Coders.Models.Attachments;
#endregion

namespace Coders.Web.Controllers
{
	public class AttachmentController : DefaultController
	{
		public AttachmentController(IAttachmentService attachmentService)
		{
			this.AttachmentService = attachmentService;
		}

		public IAttachmentService AttachmentService
		{
			get;
			private set;
		}

		[HttpGet]
		public ActionResult Detail(int id)
		{
			var attachment = AttachmentService.GetById(id);

			if (attachment == null)
			{
				return HttpNotFound();
			}

			var path = attachment.FullPath.AsPath();
			var file = new FileInfo(path);

			if (!file.Exists)
			{
				return HttpNotFound();
			}

			using (var stream = file.OpenRead())
			{
				return File(stream, attachment.FileType);
			}
		}

		[HttpGet]
		public ActionResult Image(int id, int width, int height)
		{
			var attachment = AttachmentService.GetById(id);

			if (attachment == null)
			{
				return HttpNotFound();
			}

			var image = AttachmentService.GetThumbnail(attachment, width, height);

			using (var stream = image.OpenRead())
			{
				return File(stream, attachment.FileType);
			}
		}
	}
}