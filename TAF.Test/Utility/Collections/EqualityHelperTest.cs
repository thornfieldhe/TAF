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

        /// <summary>
        /// 普通对象提供相等判定的扩展
        /// </summary>
        [TestMethod]
        public void CreateComparerTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "b" },
                                       };
            var info = new TestInfo { Id = Guid.NewGuid(), Name = "a" };
            var comparer = Equality<TestInfo>.CreateComparer(m => m.Name);
            Assert.IsTrue(list1.Contains(info, comparer));


        }
    }
}
