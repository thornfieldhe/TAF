// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateTest.cs" company="">
//   
// </copyright>
// <summary>
//   验证测试
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Test
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 验证测试
    /// </summary>
    [TestClass]
    public class ValidateTest
    {
        /// <summary>
        /// 客户
        /// </summary>
        private User2 user;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            Ioc.Register(new IocConfig());
            this.user = new User2();
        }

        /// <summary>
        /// 基本验证
        /// </summary>
        [TestMethod]
        public void TestSetValidationHandler2()
        {
            user = new User2();
            if (user.IsValidated.Not())
            {
                Assert.AreEqual("姓名不能为空", this.user.ValidationResult.First().ErrorMessage);
            }
        }

        /// <summary>
        /// 外部调用方法AddValidationRule增加验证条件
        /// </summary>
        [TestMethod]
        public void TestAddValidationRule()
        {
            user = new User2 { Name = "123" };
            user.AddValidationRule(new ContainsHXHValidationRule(this.user.Name));
            if (this.user.IsValidated.Not())
            {
                Assert.AreEqual("姓名必须包含HXH", user.ValidationResult.First().ErrorMessage);
            }
        }

        /// <summary>
        /// 重载方法Validate(ValidationResultCollection results)增加验证条件
        /// </summary>
        [TestMethod]
        public void TestSetValidationHandler()
        {
            this.user = new User2 { Name = "3" };
            if (this.user.IsValidated.Not())
            {
                Assert.AreEqual("姓名长度不能小于2", user.ValidationResult.First().ErrorMessage);
            }
        }

        //        /// <summary>
        //        /// 设置验证处理器,不进行任何操作，所以不会抛出异常
        //        /// </summary>
        //        [TestMethod]
        //        public void TestSetValidationHandler_NotThow()
        //        {
        //            this.user = new User2();
        //            this.user.SetValidationHandler(new NothingValidationHandler());
        //            this.user.Validate();
        //        }

        /// <summary>
        /// 继承同一个接口的多个类
        /// </summary>
        [TestMethod]
        public void TestSetmultitudinous()
        {
            var mode1 = Ioc.Create<IModel>("model1");
            Assert.AreEqual("Model1", mode1.Name);
            var mode2 = Ioc.Create<IModel>("xx");
            Assert.AreEqual("Model2", mode2.Name);
        }
    }
}