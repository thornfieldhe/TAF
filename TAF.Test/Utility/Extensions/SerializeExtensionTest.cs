using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.Test
{
    using TAF.Utility;

    /// <summary>
    /// ��֤������չ
    /// </summary>
    [TestClass]
    public class SerializeTest
    {
        /// <summary>
        /// soap���л�
        /// </summary>
        [TestMethod]
        public void TestSoapSerialize()
        {
            var user = new User() { Name = "xxx" };
            var serialize = user.SerializeObjectToString();
            Assert.AreEqual(serialize.DeserializeStringToObject<User>().Name, user.Name);
        }

        /// <summary>
        /// xml���л�
        /// �̳л���BaseEntity��ҵ���಻����xml���л���
        /// ֻ��ʹ���ֽ����л�
        /// </summary>
        [TestMethod]
        public void TestXMlSerialize()
        {
            var user = new User() { Name = "xxx" };
            var serialize = user.XMLSerializer();
            Assert.AreEqual(serialize.XMLDeserializeFromString<User>().Name, user.Name);
        }

    }
}
