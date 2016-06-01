﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.BusinessEntity.Test
{
    [TestClass]
    public class EntitiyTest
    {
        [TestMethod]
        public void TestInsert()
        {
            var user = new User() { Name = "n1" };

            var result = user.Create();
            Assert.AreEqual(1, result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var user = User.Find(r => r.Name == "n1");
            user.Note = "pp";
            var result = user.Save();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestDelete()
        {
            Assert.AreEqual(1, User.GetAll().Count);
            var user = User.Find(r => r.Name == "n1");
            user.SoftDelete();
            Assert.AreEqual(0, User.GetAll().Count);
        }

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            Ioc.Register(new IocConfig());
        }
    }
}
