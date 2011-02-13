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
using System.IO;
using System.Web;
using Coders.Specifications;
#endregion

namespace Coders.Models.Attachments
{
	public interface IAttachmentService : IEntityService<Attachment>
	{
		/// <summary>
		/// Gets the pending attachment using the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		AttachmentPending GetPendingById(int id);

		/// <summary>
		/// Gets the file types.
		/// </summary>
		/// <returns></returns>
		IList<string> GetFileTypes();

		/// <summary>
		/// Gets the pending attachments using the specified specification.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <returns></returns>
		IList<AttachmentPending> GetPending(ISpecification<AttachmentPending> specification);

		/// <summary>
		/// Gets the attachment thumbnail using the specified width and height.
		/// </summary>
		/// <param name="attachment">The attachment.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		FileInfo GetThumbnail(Attachment attachment, int width, int height);

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns></returns>
		string GetPath();

		/// <summary>
		/// Processes the specified pending attachment.
		/// </summary>
		/// <param name="pending">The pending.</param>
		/// <param name="items">The items.</param>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		bool Process(AttachmentPending pending, IList<AttachmentPending> items, HttpPostedFileBase source);

		/// <summary>
		/// Inserts the pending attachment.
		/// </summary>
		/// <param name="pending">The pending.</param>
		void InsertPending(AttachmentPending pending);

		/// <summary>
		/// Deletes the specified attachment.
		/// </summary>
		/// <param name="attachment">The attachment.</param>
		/// <param name="soft">if set to <c>true</c> [soft].</param>
		void Delete(Attachment attachment, bool soft);

		/// <summary>
		/// Deletes the pending attachment.
		/// </summary>
		/// <param name="pending">The pending.</param>
		void DeletePending(AttachmentPending pending);

		/// <summary>
		/// Deletes the pending attachment with the option 
		/// to delete the actual attachment as well.
		/// </summary>
		/// <param name="pending">The pending.</param>
		/// <param name="cascade">if set to <c>true</c> [cascade].</param>
		void DeletePending(AttachmentPending pending, bool cascade);
	}
}