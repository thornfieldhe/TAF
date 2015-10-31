// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayTest.cs" company="">
//   
// </copyright>
// <summary>
//   数组扩展测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TAF.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utility;

    /// <summary>
    /// The array test.
    /// </summary>
    [TestClass]
    public class ArrayTest
    {
        /// <summary>
        /// 转换为用分隔符拼接的字符串
        /// </summary>
        [TestMethod]
        public void TestSplice()
        {
            Assert.AreEqual("1,2,3", new List<int> { 1, 2, 3 }.Splice());
            Assert.AreEqual("'1','2','3'", new List<int> { 1, 2, 3 }.Splice("'"));
            Assert.AreEqual("1,2,3", new List<int> { 1, 2, 3 }.ToString(","));
            Assert.AreEqual("2pp,3pp,4pp", new List<int> { 1, 2, 3 }.ToString(r => ((++r) + "pp").ToString(), ","));
        }

        /// <summary>
        /// The test_ foreach.
        /// </summary>
        [TestMethod]
        public void Test_Foreach()
        {
            List<string> data = new List<string> { "A", "B", "C", "D" };
            var count = 0;
            data.ForEach(item => { count++; });
            Assert.AreEqual(count, 4);
        }

        /// <summary>
        /// The test_ random.
        /// </summary>
        [TestMethod]
        public void Test_Random()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.IsTrue(list.Contains(list.Random()));
        }

        /// <summary>
        /// The test_ contains.
        /// </summary>
        [TestMethod]
        public void Test_Contains()
        {
            var list = new List<int> { 1, 2, 3 };
            Assert.IsTrue(list.Random().In(list));
            Assert.IsTrue(5.NotIn(list));
        }

        /// <summary>
        /// The test_ is empty.
        /// </summary>
        [TestMethod]
        public void Test_IsEmpty()
        {
            var list = new List<int>();
            Assert.IsTrue(list.IsNullOrEmpty());
            list.Add(2);
            Assert.IsFalse(list.IsNullOrEmpty());
            list = null;
            Assert.IsTrue(list.IsNullOrEmpty());
        }

        /// <summary>
        /// The test_ max or min value.
        /// </summary>
        [TestMethod]
        public void Test_MaxOrMinValue()
        {
            var u1 = new User { Name = "AB", Email = "234" };
            var u2 = new User { Name = "BC", Email = "123" };
            var list = new List<User> { u1, u2 };
            Assert.AreEqual(u1.Name, list.Min(u => u.Name)); // User需要继承IComparable<User>接口
            Assert.AreEqual(u2.Name, list.Max(u => u.Name));
            Assert.AreEqual(u2.Email, list.Min(u => u.Email));
            Assert.AreEqual(u1.Email, list.Max(u => u.Email));

            Assert.AreEqual(u1.Name, list.MinBy(u => u.Name).Name); // User不需要继承IComparable接口即可实现
            Assert.AreEqual(u2.Name, list.MaxBy(u => u.Name).Name);
            Assert.AreEqual(u2.Email, list.MinBy(u => u.Email).Email);
            Assert.AreEqual(u1.Email, list.MaxBy(u => u.Email).Email);

            var list2 = new List<int> { 1, 2, 3 };
            Assert.AreEqual(1, list2.Min());
            Assert.AreEqual(3, list2.Max());
        }

        /// <summary>
        /// The test_ shuffle.
        /// </summary>
        [TestMethod]
        public void Test_Shuffle()
        {
            var list = new List<int> { 1, 2, 3 };
            var newList = list.Shuffle().ToList();
            Assert.IsFalse((newList[0] == list[0]).And(newList[1] == list[1]).And(newList[2] == list[2]));
        }

        /// <summary>
        /// The test_ reversal.
        /// </summary>
        [TestMethod]
        public void Test_Reversal()
        {
            var list = new[] { 1, 2, 3 };
            list.Reversal();
            Assert.AreEqual(list[0], 3);
        }

        /// <summary>
        /// The test_ swap.
        /// </summary>
        [TestMethod]
        public void Test_Swap()
        {
            var list = new[] { 1, 2, 3 };
            list.Swap(0, 2);
            Assert.AreEqual(list[0], 3);
        }
    }

}