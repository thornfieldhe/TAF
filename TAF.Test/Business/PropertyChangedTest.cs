namespace TAF.Test.Business
{

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
            Ioc.Register(new IocConfig());
            var product = new Product { Name = "xxx" };
            Assert.AreEqual(product.CurrentValues["Name"], "xxx");
            Assert.IsFalse(product.OriginalValues.ContainsKey("Name"));
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
            AddDescription(nameof(Name), Name.ToStr());
        }
    }
}