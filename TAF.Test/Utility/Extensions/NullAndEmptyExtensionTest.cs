// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullAndEmptyExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   The null and empty test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Test
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TAF.Utility;

    /// <summary>
    /// The null and empty test.
    /// </summary>
    [TestClass]
    public class NullAndEmptyTest
    {
        /// <summary>
        /// The test is null.
        /// </summary>
        [TestMethod]
        public void TestIsNull()
        {
            string a = null;
            Assert.IsTrue(a.IsNull());
            Assert.IsFalse(a.IsNotNull());
        }

        /// <summary>
        /// The test string is empty.
        /// </summary>
        [TestMethod]
        public void TestStringIsEmpty()
        {
            var a = string.Empty;
            Assert.IsTrue(a.IsEmpty());
            a = null;
            Assert.IsTrue(a.IsEmpty());
        }

        /// <summary>
        /// The test guid is empty.
        /// </summary>
        [TestMethod]
        public void TestGuidIsEmpty()
        {
            Guid? a = null;
            Assert.IsTrue(a.IsEmpty());
            a = Guid.Empty;
            Assert.IsTrue(a.IsEmpty());
        }

        /// <summary>
        /// 测试可空整型
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Int()
        {
            int? value = null;
            Assert.AreEqual(0, value.SafeValue());

            value = 1;
            Assert.AreEqual(1, value.SafeValue());
            List<int> b = null;
            Assert.AreEqual(b.SafeValue().Count, 0);
        }

        /// <summary>
        /// 测试可空DateTime
        /// </summary>
        [TestMethod]
        public void TestSafeValue_DateTime()
        {
            DateTime? value = null;
            Assert.AreEqual(DateTime.MinValue, value.SafeValue());

            value = "2000-1-1".ToDate();
            Assert.AreEqual(value.Value, value.SafeValue());
        }

        /// <summary>
        /// 测试可空bool
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Boolean()
        {
            bool? value = null;
            Assert.AreEqual(false, value.SafeValue());

            value = true;
            Assert.AreEqual(true, value.SafeValue());
        }

        /// <summary>
        /// 测试可空double
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Double()
        {
            double? value = null;
            Assert.AreEqual(0, value.SafeValue());

            value = 1.1;
            Assert.AreEqual(1.1, value.SafeValue());
        }

        /// <summary>
        /// 测试可空decimal
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Decimal()
        {
            decimal? value = null;
            Assert.AreEqual(0, value.SafeValue());

            value = 1.1M;
            Assert.AreEqual(1.1M, value.SafeValue());
        }

        /// <summary>
        /// The test_ lock.
        /// </summary>
        [TestMethod]
        public void Test_Lock()
        {
            string value = "Fluentx";
            value.Lock(x => { });
        }
    }
}