using System;

namespace TAF.Test.Utility.Data
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    
    [TestClass]
    public class CombHelperTest
    {
        [TestMethod]
        public void NewCombTest()
        {
            DateTime now = DateTime.Now;
            Guid id = CombHelper.NewComb();
            DateTime time = CombHelper.GetDateFromComb(id);
            Assert.IsTrue(time.Subtract(now).TotalSeconds < 1);
        }
    }
}
