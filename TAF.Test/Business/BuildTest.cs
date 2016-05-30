using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TAF.Test.Business
{
    using System.Collections.Generic;

    using TAF.Business;

    [TestClass]
    public class BuildTest
    {
        [TestMethod]
        public void TestBuildUp()
        {
            var builder = new Builder<Car>();
            var car = builder.BuildUp();
            Assert.IsNotNull(car);
            Assert.AreEqual<int>(2 + 1, car.Log.Count);
            Assert.AreEqual<string>(car.Log[2],"wheel");
        }
    }


    public class Car
    {
        public IList<string> Log = new List<string>();

        [BuildStep(2)]
        public void AddWeel()
        {
            this.Log.Add("wheel");
        }

        public void AddEngine()
        {
            this.Log.Add("engine");
        }

        [BuildStep(1, 2)]
        public void AddBody()
        {
            this.Log.Add("body");
        }
    }
}
