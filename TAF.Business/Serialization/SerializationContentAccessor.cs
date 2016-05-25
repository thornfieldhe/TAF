namespace TAF.Business.Serialization
{
    using System;

    /// <summary>
    /// 序列化内容访问器接口
    /// </summary>
    public abstract class SerializationContentAccessor
    {
        /// <summary>
        /// 基于本地变量和内容Id来获得对象实例的序列化内容
        /// </summary>
        /// <param name="local"></param>
        /// <param name="id">原内容Id</param>
        /// <param name="newId">新内容Id</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetContent<T>(T local, Guid? id, out Guid? newId)
            where T : class
        {
            if (local == null)
            {
                if (!id.HasValue)
                {
                    newId = null;
                    return null;
                }

                local = Ioc.Create<SerializationContentAccessor>().Read<T>(id.Value);

                //如果数据源中已经不存在此序列化内容，那么让内容Id为空
                if (local == null)
                {
                    newId = null;
                    return null;
                }
            }
            newId = id;
            return local;
        }

        /// <summary>
        /// 基于本地变量和内容Id来设置序列化内容
        /// </summary>
        /// <param name="local"></param>
        /// <param name="modelVersion"></param>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Guid? SetContent<T>(T local, string modelVersion, Guid? id)
        {
            if (local == null)
            {
                if (id.HasValue)
                {
                    Ioc.Create<SerializationContentAccessor>().Delete(id.Value);
                }

                return null;
            }

            if (!id.HasValue)
            {
                id = Guid.NewGuid();
            }

            Ioc.Create<SerializationContentAccessor>().Write(id.Value, local, modelVersion);
            return id;
        }

        /// <summary>
        /// 写入对象的序列化内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <param name="modelVersion"></param>
        /// <typeparam name="T"></typeparam>
        public abstract void Write<T>(Guid key, T model, string modelVersion);

        /// <summary>
        /// 读取对象的序列化内容
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T Read<T>(Guid key);

        /// <summary>
        /// 删除对象序列化内容
        /// </summary>
        /// <param name="key"></param>
        public abstract void Delete(Guid key);
    }
}