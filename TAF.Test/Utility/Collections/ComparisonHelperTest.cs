using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.Test.Utility.Collections
{
    using System;

    using TAF.Utility;

    /// <summary>
    /// ComparisonHelperTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ComparisonHelperTest
    {
        [TestMethod]
        public void CreateComparerTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = Guid.NewGuid(), Name = "m" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "b" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "w" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "o" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "p" },
                                       };
            var info = new TestInfo { Id = Guid.NewGuid(), Name = "a" };
            var comparer = ComparisonHelper<TestInfo>.CreateComparer(m => m.Name);
            list1.Sort(comparer);
            Assert.AreEqual(list1[0].Name, "a");
        }
    }
}
