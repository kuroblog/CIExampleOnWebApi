
namespace Examples.Ci.WebApi.Tests.Controllers.Func
{
    using Ef;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Data.Entity;
    using System.Net.Http;
    using System.Net.Http.Headers;

    [TestClass]
    public class UsersController_Func_Tests
    {
        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            var contextInitializer = new DropCreateDatabaseAlways<CiContext>();
            Database.SetInitializer(contextInitializer);

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51547/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpClient client;
        #endregion

        #region GetUsers
        [TestMethod]
        public async void GetUsers_Func_Test_Successed()
        {
            var response = await client.GetAsync("api/users");
            response.EnsureSuccessStatusCode();

            //var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
        }

        //[TestMethod]
        //public void GetUsers_Test_Not_Found()
        //{
        //    mockUserRepo.Setup(m => m.View).Returns(() => new List<UserEntity>().AsQueryable());

        //    var actual = controller.GetUsers().Result;
        //    Assert.IsNotNull(actual);
        //    Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        //}

        //[TestMethod]
        //public void GetUsers_Test_Exception()
        //{
        //    mockUserRepo.Setup(m => m.View).Callback(() =>
        //    {
        //        throw new Exception("mock test");
        //    });

        //    var actual = controller.GetUsers().Result;
        //    Assert.IsNotNull(actual);
        //    Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        //}
        #endregion
    }
}
