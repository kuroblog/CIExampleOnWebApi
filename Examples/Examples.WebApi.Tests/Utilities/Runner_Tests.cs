
namespace Examples.WebApi.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using WebApi;

    [TestClass()]
    public class Runner_Tests
    {
        [TestMethod]
        public void Execute_Test_Successed()
        {
            var expected = 0;
            var actual = Runner.Execute(() => { return expected; }).Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void Execute_Test_Successed_By_Exception()
        {
            var expected = "test";

            var actual = Runner.Execute(() =>
            {
                throw new ArgumentException(expected);

#pragma warning disable
                return 0;
#pragma warning restore
            }).Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(true, actual.HasError);
            Assert.AreEqual(expected, actual.Error.Message);
        }
    }
}