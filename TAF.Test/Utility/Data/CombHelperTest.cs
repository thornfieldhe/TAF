using System;

namespace TAF.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TAF.Test.Utility;

    [TestClass]
    public class CombHelperTest
    {
        [TestMethod]
        public void NewCombTest()
        {
            DateTime now = DateTime.Now;
            Guid id = Comb.NewComb();
            DateTime time = Comb.GetDateFromComb(id);
            Assert.IsTrue(time.Subtract(now).TotalSeconds < 1);
        }
    }
}
