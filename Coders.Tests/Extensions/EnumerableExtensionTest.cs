using System.Collections.Generic;
using System.Linq;
using Coders.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coders.Tests.Extensions
{
	[TestClass]
	public class EnumerableExtensionTest
	{
		private IList<Value> Values
		{
			get; 
			set;
		}

		[TestInitialize]
		public void TestInitialize()
		{
			this.Values = new List<Value>
			{
				new Value { Name = "One" },
				new Value { Name = "Two" },
				new Value { Name = "Three" }
			};
		}

		[TestMethod]
		public void Test_EnumerableExtension_ForEach()
		{
			Assert.AreEqual(0, this.Values.Count(x => x.Selected));

			this.Values.ForEach(x => x.Selected = true);

			Assert.AreEqual(3, this.Values.Count(x => x.Selected));
		}

		[TestMethod]
		public void Test_EnumerableExtension_FirstOrDefault()
		{
			var value = this.Values.FirstOrDefault();

			Assert.IsNotNull(value);
			Assert.AreEqual("One", value.Name);
		}

		private class Value
		{
			public string Name
			{
				get; 
				set;
			}

			public bool Selected
			{
				get; 
				set;
			}
		}
	}
}