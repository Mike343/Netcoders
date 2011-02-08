using Coders.Models;
using Coders.Models.Users;

namespace Coders.Repositories.Strategies.Joins
{
	public class UserMinimum : EntityBase, IUser
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public virtual string Name
		{
			get; 
			set;
		}

		/// <summary>
		/// Gets or sets the slug.
		/// </summary>
		/// <value>
		/// The slug.
		/// </value>
		public virtual string Slug
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public virtual string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the signature parsed.
		/// </summary>
		/// <value>
		/// The signature parsed.
		/// </value>
		public virtual string SignatureParsed
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the avatar.
		/// </summary>
		/// <value>
		/// The avatar.
		/// </value>
		public virtual UserAvatar Avatar
		{
			get;
			set;
		}
	}
}