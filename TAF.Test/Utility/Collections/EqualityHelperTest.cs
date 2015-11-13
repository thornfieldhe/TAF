using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.Test.Utility.Collections
{
    using System.Linq;

    using TAF.Utility;

    /// <summary>
    /// EqualityHelperTest 的摘要说明
    /// </summary>
    [TestClass]
    public class EqualityHelperTest
    {

        [TestMethod]
        public void CreateComparerTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "b" },
                                       };
            var info = new TestInfo { Id = Guid.NewGuid(), Name = "a" };
            var comparer = EqualityHelper<TestInfo>.CreateComparer(m => m.Name);
            Assert.IsTrue(list1.Contains(info, comparer));


        }
    }
}
