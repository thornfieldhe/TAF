using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.Test
{
    using System;
    using System.Linq;

    using TAF.Utility;

    /// <summary>
    /// 普通对象提供比较判定的扩展
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

            var comparer = TAF.Utility.Comparison<TestInfo>.CreateComparer(m => m.Name);

            list1.Sort(comparer);
            Assert.AreEqual(list1[0].Name, "a");
        }

        /// <summary>
        /// 去除重复
        /// </summary>
        [TestMethod]
        public void DistinctTest()
        {
            var list1 = new List<TestInfo>
                                       {
                                           new TestInfo { Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "m" },
                                           new TestInfo { Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "b" },
                                           new TestInfo { Id = new Guid("EDF61577-C9AA-46DE-8F1A-EAED37D5298F"), Name = "a" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "w" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "A" },
                                           new TestInfo { Id = Guid.NewGuid(), Name = "a" },
                                       };

            var count = list1.Distinct(r => r.Name).Count();
            Assert.AreEqual(count, 5);
            count = list1.Distinct(r => r.Id).Count();
            Assert.AreEqual(count, 4);
            Assert.AreEqual(list1.Distinct(r => r.Id).First().Name, "m");
        }
    }
}
