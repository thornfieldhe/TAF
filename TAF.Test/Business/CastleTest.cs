namespace TAF.Test.Business
{
    using System.ComponentModel;

    using Castle.Core.Interceptor;
    using Castle.DynamicProxy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// BuilderTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CastalTest
    {
        [TestMethod]
        public void PropertyChangedTest()
        {
            var viewModel = Proxy.Instance.CreateClassProxy<Product2>(new NotifyPropertyChangedInterceptor());
            Assert.IsNull(viewModel.GetPropetyName());
            viewModel.Name = "ssss";
            Assert.AreEqual(viewModel.GetPropetyName(),"Name");
        }
    }

    public class Product2 : NotifyPropertyChanged
    {
        private string name;

        //必须为virtual
        public virtual string Name
        {
            get; set;
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            this.name = propertyName;
        }

        public string GetPropetyName() { return	name ;  }
    }
}