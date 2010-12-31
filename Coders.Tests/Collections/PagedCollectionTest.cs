using System.Collections.Generic;
using Coders.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Collections
{
	[TestClass]
	public class PagedCollectionTest
	{
		private IPagedCollection<string> _collection;

		[TestInitialize]
		public void Initialize()
		{
			_collection = new PagedCollection<string>(new List<string> { "one" }, 1, 1, 3);
		}

		[TestMethod]
		public void Test_PagedCollection_Items()
		{
			Assert.AreEqual(1, _collection.Items.Count);
		}

		[TestMethod]
		public void Test_PagedCollection_Page()
		{
			Assert.AreEqual(1, _collection.Page);
		}

		[TestMethod]
		public void Test_PagedCollection_Limit()
		{
			Assert.AreEqual(1, _collection.Limit);
		}

		[TestMethod]
		public void Test_PagedCollection_Total()
		{
			Assert.AreEqual(3, _collection.Total);
		}

		[TestMethod]
		public void Test_PagedCollection_Pages()
		{
			Assert.AreEqual(3, _collection.Pages);
		}

		[TestMethod]
		public void Test_PagedCollection_FirstIndex()
		{
			Assert.AreEqual(0, _collection.FirstIndex);
		}

		[TestMethod]
		public void Test_PagedCollection_LastIndex()
		{
			Assert.AreEqual(0, _collection.LastIndex);
		}

		[TestMethod]
		public void Test_PagedCollection_StartIndex()
		{
			Assert.AreEqual(1, _collection.StartIndex);
		}

		[TestMethod]
		public void Test_PagedCollection_EndIndex()
		{
			Assert.AreEqual(1, _collection.EndIndex);
		}

		[TestMethod]
		public void Test_PagedCollection_PreviousPage()
		{
			Assert.AreEqual(0, _collection.PreviousPage);
		}

		[TestMethod]
		public void Test_PagedCollection_NextPage()
		{
			Assert.AreEqual(2, _collection.NextPage);
		}

		[TestMethod]
		public void Test_PagedCollection_HasPrevious()
		{
			Assert.IsFalse(_collection.HasPrevious);
		}

		[TestMethod]
		public void Test_PagedCollection_HasNext()
		{
			Assert.IsTrue(_collection.HasNext);
		}
	}
}