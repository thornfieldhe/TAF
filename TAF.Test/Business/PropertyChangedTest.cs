namespace TAF.Test.Business
{
    using System.Collections.Specialized;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TAF.Utility;

    /// <summary>
    /// BuilderTest 的摘要说明
    /// </summary>
    [TestClass]
    public class BuilderTest
    {
        [TestMethod]
        public void PropertyChangedTest()
        {
            var product = new Product();
            var str1 = product.ToString();
            product.Name = "xxx";
            Assert.AreNotEqual(product.ToString(), str1);
            product.Name = null;
            Assert.AreEqual(product.ToString(), str1);
        }
    }



    public class Product : BaseBusiness<Product>
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref this._name, value);
            }
        }

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Name" + Name.ToStr());
        }


    }
}