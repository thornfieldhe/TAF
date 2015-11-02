using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.BusinessEntity.Test
{
    [TestClass]
    public class EntitiyTest
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            Ioc.Register(new IocConfig());
        }

        [TestMethod]
        public void TestInsert()
        {
            var user = new User() { Name = "n1" };
            
            var result = user.Create();
            Assert.AreEqual(1, result);
        }
    }
}
