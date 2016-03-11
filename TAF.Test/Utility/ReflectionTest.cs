using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAF.Utility;
namespace TAF.Test.Utility.Extensions
{
    using System;

    /// <summary>
    /// DescriptionTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ReflectionTest
    {

        [TestMethod]
        public void TestDescription()
        {
            Assert.AreEqual(typeof(TestDesc).ToDescription(), "测试名称");
            Assert.AreEqual(Reflection.GetMemberDescription<TestDesc>("Name"), "名称");
            Type.GetType("");
        }

        [TestMethod]
        public void TestGetMembers()
        {
            Assert.AreEqual(Reflection.GetMembers<TestDesc>()[0].Item1, "Name");
        }
    }

    [System.ComponentModel.Description("测试名称")]
    public class TestDesc : EfBusiness<TestDesc>
    {
        [System.ComponentModel.Description("名称")]
        public string Name
        {
            get; set;
        }
    }
}
