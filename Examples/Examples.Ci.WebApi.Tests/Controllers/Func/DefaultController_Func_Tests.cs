
namespace Examples.Ci.WebApi.Tests.Controllers.Func
{
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DefaultController_Func_Tests
    {
        [TestMethod]
        public void DefaultController_Test_Successed()
        {
            using (var server = TestServer.Create(app => { }))
            {
                var result = server.HttpClient.GetAsync("api/default").Result;
            }


            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test()
        {
            Assert.IsTrue(true);
        }
    }
}
