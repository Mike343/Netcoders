using System.Collections.Generic;
using Coders.Collections;
using NUnit.Framework;

namespace Coders.Tests.Collections
{
	[TestFixture]
	public class PagedCollectionTest
	{
		private IPagedCollection<string> _collection;

		[SetUp]
		public void Initialize()
		{
			_collection = new PagedCollection<string>(new List<string> { "one" }, 1, 1, 3);
		}

		[Test]
		public void Test_PagedCollection_Items()
		{
			Assert.AreEqual(1, _collection.Items.Count);
		}

		[Test]
		public void Test_PagedCollection_Page()
		{
			Assert.AreEqual(1, _collection.Page);
		}

		[Test]
		public void Test_PagedCollection_Limit()
		{
			Assert.AreEqual(1, _collection.Limit);
		}

		[Test]
		public void Test_PagedCollection_Total()
		{
			Assert.AreEqual(3, _collection.Total);
		}

		[Test]
		public void Test_PagedCollection_Pages()
		{
			Assert.AreEqual(3, _collection.Pages);
		}

		[Test]
		public void Test_PagedCollection_StartIndex()
		{
			Assert.AreEqual(1, _collection.StartIndex);
		}

		[Test]
		public void Test_PagedCollection_EndIndex()
		{
			Assert.AreEqual(1, _collection.EndIndex);
		}

		[Test]
		public void Test_PagedCollection_PreviousPage()
		{
			Assert.AreEqual(0, _collection.PreviousPage);
		}

		[Test]
		public void Test_PagedCollection_NextPage()
		{
			Assert.AreEqual(2, _collection.NextPage);
		}

		[Test]
		public void Test_PagedCollection_HasPrevious()
		{
			Assert.IsFalse(_collection.HasPrevious);
		}

		[Test]
		public void Test_PagedCollection_HasNext()
		{
			Assert.IsTrue(_collection.HasNext);
		}
	}
}