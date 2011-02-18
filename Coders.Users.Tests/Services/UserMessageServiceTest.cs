using System.Linq;
using System.Threading;
using Coders.Models.Common;
using Coders.Models.Users;
using Coders.Specifications;
using Coders.Tests.Authentication;
using Coders.Users.Models;
using Coders.Users.Services;
using Coders.Users.Tests.Services.Fakes;
using NUnit.Framework;
using Rhino.Mocks;

namespace Coders.Users.Tests.Services
{
	[TestFixture]
	public class UserMessageServiceTest
	{
		public IUserMessageService UserMessageService
		{
			get; 
			private set;
		}

		[SetUp]
		public void Initialize()
		{
			var authentication = MockRepository.GenerateMock<IAuthenticationService>();

			authentication.Stub(x => x.Identity).Return(new FakeUserIdentity());

			var user = MockRepository.GenerateMock<IUserService>();

			user.Stub(x => x.GetById(1)).Return(new User());

			this.UserMessageService = new UserMessageService(
				authentication,
				MockRepository.GenerateMock<ITextFormattingService>(), 
				user,
				new FakeUserMessageRepository(),
				new FakeUserMessageFolderRepository()
			);
		}

		[Test]
		public void Test_UserMessageService_GetFolderBy()
		{
			var folder = this.UserMessageService.GetFolderBy(new Specification<UserMessageFolder>());

			Assert.IsNotNull(folder, "Folder");
		}

		[Test]
		public void Test_UserMessageService_GetFolderById()
		{
			var folder = this.UserMessageService.GetFolderById(1);

			Assert.IsNotNull(folder, "Folder");
		}

		[Test]
		public void Test_UserMessageService_GetFolders()
		{
			var folders = this.UserMessageService.GetFolders(new Specification<UserMessageFolder>());

			Assert.IsNotNull(folders, "NotNull");
			Assert.IsNotEmpty(folders.ToList(), "NotEmpty");
		}

		[Test]
		public void Test_UserMessageService_Create()
		{
			var folder = this.UserMessageService.Create();

			Assert.IsNotNull(folder, "Folder");
			Assert.AreEqual(folder.Created, folder.Updated, "Updated");
		}

		[Test]
		public void Test_UserMessageService_MarkRead()
		{
			var folder = this.UserMessageService.Create();

			this.UserMessageService.MarkRead(folder);

			Assert.IsTrue(folder.HasReceiverRead, "HasReceiverRead");
			Assert.IsNotNull(folder.Read, "Read");
		}

		[Test]
		public void Test_UserMessageService_Insert()
		{
			var message = this.UserMessageService.Create();

			message.Title = "test 123";
			message.Body = "test";

			this.UserMessageService.Insert(message);

			Assert.AreEqual("test-123", message.Slug, "Slug");
			Assert.IsNotNull(message.Sender, "Sender");
		}

		[Test]
		public void Test_UserMessageService_Insert_With_Name()
		{
			var message = this.UserMessageService.Create();

			message.Title = "test 123";
			message.Body = "test";

			this.UserMessageService.Insert(message, "Test,Test2");

			Assert.AreEqual("test-123", message.Slug, "Slug");
			Assert.IsNotNull(message.Sender, "Sender");
		}

		[Test]
		public void Test_UserMessageService_InsertFolder()
		{
			var folder = new UserMessageFolder
			{
				Title = "test 123"
			};

			this.UserMessageService.InsertFolder(folder);

			Assert.AreEqual(1, folder.UserId, "UserId");
			Assert.AreEqual("test-123", folder.Slug, "Slug");
		}

		[Test]
		public void Test_UserMessageService_Update()
		{
			var message = this.UserMessageService.Create();
			var date = message.Updated;

			Thread.Sleep(1000);

			this.UserMessageService.Update(message);

			Assert.AreNotEqual(date, message.Updated, "Updated");
		}

		[Test]
		public void Test_UserMessageService_UpdateFolder()
		{
			var folder = new UserMessageFolder
			{
				Title = "test 123", 
				Slug = "test"
			};

			this.UserMessageService.UpdateFolder(folder);

			Assert.AreEqual("test-123", folder.Slug, "Slug");
		}
	}
}