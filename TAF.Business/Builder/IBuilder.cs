namespace TAF.Business
{
    /// <summary>
    /// 装配器，用于动态组装对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<T> where T : class
    {
        T BuildUp();

        T TearDown();
    }
}