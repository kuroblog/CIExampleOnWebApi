using Microsoft.VisualStudio.TestTools.UnitTesting;
using Examples.WebApi.Controllers;

namespace Examples.WebApi.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Examples.WebApi.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using Repositories;
    using Models;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Web.Http.Results;

    [TestClass]
    public class UsersController_Tests
    {
        [TestInitialize]
        public void Initialize()
        {
            testItems = new List<UserEntity> {
                new UserEntity { CreatedAt = DateTime.Now, UserNo = "001", UserName = "test1" },
                new UserEntity { CreatedAt = DateTime.Now, UserNo = "002", UserName = "test2" },
                new UserEntity { CreatedAt = DateTime.Now, UserNo = "003", UserName = "test3" }
            };

            mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(m => m.View).Returns(() => testItems.AsQueryable());

            controller = new UsersController(mockUserRepo.Object);
        }

        private UsersController controller;
        private List<UserEntity> testItems;
        private Mock<IUserRepository> mockUserRepo;

        [TestMethod]
        public void GetUsers_Test_Successed()
        {
            var actual = controller.GetUsers().Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(testItems.Count, actualResult.Content.Count());
        }

        [TestMethod()]
        public void GetUserByNo_Test_Successed()
        {
            var expected = testItems[1];

            var actual = controller.GetUserByNo(expected.UserNo).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<UserDto>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expected.UserNo, actualResult.Content.UserNo);
        }

        [TestMethod()]
        public void GetUserByNo_Test_Not_Found()
        {
            var expected = "999";

            var actual = controller.GetUserByNo(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetUserByNo_Test_Bad_Request_With_Message_When_Args_Is_Empty()
        {
            var expected = string.Empty;

            var actual = controller.GetUserByNo(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod()]
        public void GetUserByName_Test_Successed_When_Single_Matched()
        {
            var expected = testItems[1];

            var actual = controller.GetUserByName(expected.UserName).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult.Content.Count());
            Assert.AreEqual(expected.UserName, actualResult.Content.FirstOrDefault().UserName);
        }

        [TestMethod()]
        public void GetUserByName_Test_Successed_When_Multiple_Matched()
        {
            var expected = "test";

            var actual = controller.GetUserByName(expected).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(testItems.Count, actualResult.Content.Count());
        }

        [TestMethod()]
        public void GetUserByName_Test_Successed_When_Zero_Matched()
        {
            var expected = "999";

            var actual = controller.GetUserByName(expected).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(0, actualResult.Content.Count());
        }

        [TestMethod()]
        public void GetUserByName_Test_Bad_Request_With_Message_When_Args_Is_Empty()
        {
            var expected = string.Empty;

            var actual = controller.GetUserByName(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }
    }
}