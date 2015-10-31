// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializeExtensionTest.cs" company="">
//   
// </copyright>
// <summary>
//   验证特性扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utility;

    /// <summary>
    /// 验证特性扩展
    /// </summary>
    [TestClass]
    public class SerializeTest
    {
        /// <summary>
        /// soap序列化
        /// </summary>
        [TestMethod]
        public void TestSoapSerialize()
        {
            var user = new User { Name = "xxx" };
            var serialize = user.SerializeObjectToString();
            Assert.AreEqual(serialize.DeserializeStringToObject<User>().Name, user.Name);
        }

        /// <summary>
        /// xml序列化
        /// 继承基类BaseEntity的业务类不适用xml序列化，
        /// 只能使用字节序列化
        /// </summary>
        [TestMethod]
        public void TestXMlSerialize()
        {
            var user = new User { Name = "xxx" };
            var serialize = user.XMLSerializer();
            Assert.AreEqual(serialize.XMLDeserializeFromString<User>().Name, user.Name);
        }
    }
}