// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumTest.cs" company="">
//   
// </copyright>
// <summary>
//   测试枚举
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TAF.Utility;
    using TAF.Test;

    /// <summary>
    /// 测试枚举
    /// </summary>
    [TestClass]
    public class EnumTest
    {
        #region 常量

        /// <summary>
        /// LogType.Debug实例
        /// </summary>
        public const LogType DEBUG_INSTANCE = LogType.Debug;

        /// <summary>
        /// LogType.Debug的名称：Debug
        /// </summary>
        public const string DEBUG_NAME = "Debug";

        /// <summary>
        /// LogType.Debug的值：5
        /// </summary>
        public const int DEBUG_VALUE = 5;

        /// <summary>
        /// LogType.Debug的描述："调试"
        /// </summary>
        public const string DEBUG_DESCRIPTION = "调试";

        #endregion

        #region GetInstance(获取实例)

        /// <summary>
        /// 1. 功能：获取实例,
        /// 2. 场景：参数为null
        /// 3. 预期：抛出异常
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetInstance_ArgumentIsNull_Throw()
        {
            try
            {
                EnumExt.GetInstance<LogType>(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("member"));
                throw;
            }
        }

        /// <summary>
        /// 1. 功能：获取实例,
        /// 2. 场景：参数为空字符串
        /// 3. 预期：抛出异常
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetInstance_ArgumentIsEmpty_Throw()
        {
            try
            {
                EnumExt.GetInstance<LogType>(string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("member"));
                throw;
            }
        }

        /// <summary>
        /// 通过成员名获取实例
        /// </summary>
        [TestMethod]
        public void GetInstance_Name()
        {
            Assert.AreEqual(DEBUG_INSTANCE, EnumExt.GetInstance<LogType>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取实例
        /// </summary>
        [TestMethod]
        public void GetInstance_Value()
        {
            Assert.AreEqual(DEBUG_INSTANCE, EnumExt.GetInstance<LogType>(DEBUG_VALUE));
        }

        /// <summary>
        /// 通过成员名获取可空枚举实例
        /// </summary>
        [TestMethod]
        public void GetInstance_Name_Nullable()
        {
            Assert.AreEqual(DEBUG_INSTANCE, EnumExt.GetInstance<LogType?>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取可空枚举实例
        /// </summary>
        [TestMethod]
        public void GetInstance_Value_Nullable()
        {
            Assert.AreEqual(DEBUG_INSTANCE, EnumExt.GetInstance<LogType?>(DEBUG_VALUE));
        }

        #endregion

        #region GetName(获取成员名)

        /// <summary>
        /// 1. 功能：获取成员名,
        /// 2. 场景：参数为空，
        /// 3. 预期：返回空字符串
        /// </summary>
        [TestMethod]
        public void GetName_ArgumentIsNull_ReturnEmpty()
        {
            Assert.AreEqual(string.Empty, EnumExt.GetName<LogType>(null));
        }

        /// <summary>
        /// 通过成员名获取成员名
        /// </summary>
        [TestMethod]
        public void GetName_Name()
        {
            Assert.AreEqual(DEBUG_NAME, EnumExt.GetName<LogType>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取成员名
        /// </summary>
        [TestMethod]
        public void GetName_Value()
        {
            Assert.AreEqual(DEBUG_NAME, EnumExt.GetName<LogType>(DEBUG_VALUE));
        }

        /// <summary>
        /// 通过实例获取成员名
        /// </summary>
        [TestMethod]
        public void GetName_Instance()
        {
            Assert.AreEqual(DEBUG_NAME, EnumExt.GetName<LogType>(DEBUG_INSTANCE));
        }

        /// <summary>
        /// 通过成员值获取可空枚举成员名
        /// </summary>
        [TestMethod]
        public void GetName_Value_Nullable()
        {
            Assert.AreEqual(DEBUG_NAME, EnumExt.GetName<LogType?>(DEBUG_VALUE));
        }

        #endregion

        #region GetValue(获取成员值)

        /// <summary>
        /// 1. 功能：获取成员值,
        /// 2. 场景：参数为null
        /// 3. 预期：抛出异常
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_ArgumentIsNull_Throw()
        {
            try
            {
                EnumExt.GetValue<LogType>(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("member"));
                throw;
            }
        }

        /// <summary>
        /// 1. 功能：获取成员值,
        /// 2. 场景：参数为空字符串，
        /// 3. 预期：抛出异常
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_ArgumentIsEmpty_Throw()
        {
            try
            {
                EnumExt.GetValue<LogType>(string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("member"));
                throw;
            }
        }

        /// <summary>
        /// 通过成员名获取成员值
        /// </summary>
        [TestMethod]
        public void GetValue_Name()
        {
            int actual = EnumExt.GetValue<LogType>(DEBUG_NAME);
            Assert.AreEqual(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过成员值获取成员值
        /// </summary>
        [TestMethod]
        public void GetValue_Value()
        {
            int actual = EnumExt.GetValue<LogType>(DEBUG_VALUE);
            Assert.AreEqual(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过实例获取成员值
        /// </summary>
        [TestMethod]
        public void GetValue_Instance()
        {
            int actual = EnumExt.GetValue<LogType>(DEBUG_INSTANCE);
            Assert.AreEqual(DEBUG_VALUE, actual);
            Assert.AreEqual(DEBUG_VALUE, DEBUG_INSTANCE.Value());
            Assert.AreEqual(DEBUG_VALUE, DEBUG_INSTANCE.Value<int>());
            Assert.AreEqual(DEBUG_VALUE.ToString(), DEBUG_INSTANCE.Value<string>());
        }

        /// <summary>
        /// 通过成员名获取可空枚举成员值
        /// </summary>
        [TestMethod]
        public void GetValue_Name_Nullable()
        {
            int actual = EnumExt.GetValue<LogType?>(DEBUG_NAME);
            Assert.AreEqual(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过成员值获取可空枚举成员值
        /// </summary>
        [TestMethod]
        public void GetValue_Value_Nullable()
        {
            int actual = EnumExt.GetValue<LogType?>(DEBUG_VALUE);
            Assert.AreEqual(DEBUG_VALUE, actual);
        }

        /// <summary>
        /// 通过实例获取可空枚举成员值
        /// </summary>
        [TestMethod]
        public void GetValue_Instance_Nullable()
        {
            int actual = EnumExt.GetValue<LogType?>(DEBUG_INSTANCE);
            Assert.AreEqual(DEBUG_VALUE, actual);
        }

        #endregion

        #region GetMemberDescription(获取描述)

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：参数为空，
        /// 3. 预期：返回空字符串
        /// </summary>
        [TestMethod]
        public void GetDescription_ArgumentIsNull_ReturnEmpty()
        {
            Assert.AreEqual(string.Empty, EnumExt.GetDescription<LogType>(null));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：参数为空字符串，
        /// 3. 预期：返回空字符串
        /// </summary>
        [TestMethod]
        public void GetDescription_ArgumentIsEmpty_ReturnEmpty()
        {
            Assert.AreEqual(string.Empty, EnumExt.GetDescription<LogType>(string.Empty));
        }

        /// <summary>
        /// 通过成员名获取描述
        /// </summary>
        [TestMethod]
        public void GetDescription_Name()
        {
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_NAME));
        }

        /// <summary>
        /// 通过成员值获取描述
        /// </summary>
        [TestMethod]
        public void GetDescription_Value()
        {
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_VALUE));
        }

        /// <summary>
        /// 通过实例获取描述
        /// </summary>
        [TestMethod]
        public void GetDescription_Instance()
        {
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_INSTANCE));
            Assert.AreEqual(DEBUG_DESCRIPTION, DEBUG_INSTANCE.Description());
        }

        /// <summary>
        /// 通过成员名获取可空枚举描述
        /// </summary>
        [TestMethod]
        public void GetDescription_Name_Nullable()
        {
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType?>(DEBUG_NAME));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效成员名，
        /// 3. 预期：返回空字符串
        /// </summary>
        [TestMethod]
        public void GetDescription_InvalidName_ReturnEmpty()
        {
            Assert.AreEqual(string.Empty, EnumExt.GetDescription<LogType>("Debug1"));
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>("Debug"));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效成员值，
        /// 3. 预期：返回空字符串
        /// </summary>
        [TestMethod]
        public void GetDescription_InvalidValue_ReturnEmpty()
        {
            Assert.AreEqual(string.Empty, EnumExt.GetDescription<LogType>(100));
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(5));
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效枚举，
        /// 3. 预期：返回空字符串
        /// </summary>
        [TestMethod]
        public void GetDescription_InvalidEnum_ReturnEmpty()
        {
            Assert.AreEqual("Fatal", EnumExt.GetDescription<LogType>(LogType.Fatal));
            Assert.AreEqual(DEBUG_DESCRIPTION, EnumExt.GetDescription<LogType>(DEBUG_INSTANCE));
        }

        #endregion

        #region GetItems(获取描述项集合)

        /// <summary>
        /// 获取描述项集合
        /// </summary>
        [TestMethod]
        public void GetItems_Success()
        {
            var items = EnumExt.GetItems<LogType>();
            Assert.AreEqual(5, items.Count);
            Assert.AreEqual("Fatal", items[0].Text);
            Assert.AreEqual("1", items[0].Value);
            Assert.AreEqual("错误", items[3].Text);
            Assert.AreEqual("2", items[3].Value);
            Assert.AreEqual("警告", items[4].Text);
            Assert.AreEqual("3", items[4].Value);
        }

        /// <summary>
        /// 获取描述项集合,绑定的不是枚举
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetItems_NotIsEnum()
        {
            try
            {
                EnumExt.GetItems<User>();
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual("类型 TAF.Test.User 不是枚举", ex.Message);
                throw;
            }
        }

        #endregion
    }
}