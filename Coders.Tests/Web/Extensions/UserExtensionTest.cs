using System.Collections.Generic;
using System.Web;
using Coders.Models.Settings;
using Coders.Models.Users;
using Coders.Web.Extensions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Tests.Web.Extensions
{
	[TestFixture]
	public class UserExtensionTest
	{
		public HttpContextBase Context
		{
			get;
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var context = MockRepository.GenerateMock<HttpContextBase>();
			var request = MockRepository.GenerateMock<HttpRequestBase>();

			request.Stub(x => x.ApplicationPath).Return("/");

			context.Stub(x => x.Request).Return(request);

			var settings = new List<Setting>
			{
			    new Setting { Key = "user.avatar.path", Value = "images/avatars" },
				new Setting { Key = "user.avatar.default", Value = "avatar.jpg" }
			};

			Setting.Rebuild(settings);

			this.Context = context;
		}

		[Test]
		public void Test_UserExtension_AvatarImage()
		{
			var user = new User 
			{ 
				Name = "Test",
				Avatar = new UserAvatar
				{
					FileDiskName = "test.jpg",
					FilePath = "images/avatars"
				} 
			};

			var result = user.AvatarImage(null, this.Context);

			Assert.AreEqual("<img alt=\"Test\" src=\"/images/avatars/test.jpg\" />", result.ToString());
		}

		[Test]
		public void Test_UserExtension_AvatarImage_Null()
		{
			var user = new User
			{
				Name = "Test"
			};

			var result = user.AvatarImage(null, this.Context);

			Assert.AreEqual("<img alt=\"Test\" src=\"/images/avatars/avatar.jpg\" />", result.ToString());
		}
	}
}