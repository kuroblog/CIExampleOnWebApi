
namespace Examples.Ci.WebApi.Controllers.Tests
{
    using Controllers;
    using Ef.Entities;
    using Ef.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http.Results;

    [TestClass]
    public class UsersController_Tests
    {
        #region Initialize
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
        #endregion

        #region GetUsers
        [TestMethod]
        public void GetUsers_Test_Successed()
        {
            var actual = controller.GetUsers().Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(testItems.Count, actualResult.Content.Count());
        }

        [TestMethod]
        public void GetUsers_Test_Not_Found()
        {
            mockUserRepo.Setup(m => m.View).Returns(() => new List<UserEntity>().AsQueryable());

            var actual = controller.GetUsers().Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetUsers_Test_Exception()
        {
            mockUserRepo.Setup(m => m.View).Callback(() =>
            {
                throw new Exception("mock test");
            });

            var actual = controller.GetUsers().Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }
        #endregion

        #region GetUsersByName
        [TestMethod]
        public void GetUsersByName_Test_Successed_When_Single_Matched()
        {
            var expected = testItems[1];

            var actual = controller.GetUsersByName(expected.UserName).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult.Content.Count());
            Assert.AreEqual(expected.UserName, actualResult.Content.FirstOrDefault().UserName);
        }

        [TestMethod]
        public void GetUsersByName_Test_Successed_When_Multiple_Matched()
        {
            var expected = "test";

            var actual = controller.GetUsersByName(expected).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<IQueryable<UserDto>>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(testItems.Count, actualResult.Content.Count());
        }

        [TestMethod]
        public void GetUsersByName_Test_Successed_When_Not_Found()
        {
            var expected = "999";

            var actual = controller.GetUsersByName(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetUsersByName_Test_Bad_Request_With_Message_When_Args_Is_Empty()
        {
            var expected = string.Empty;

            var actual = controller.GetUsersByName(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }
        #endregion

        #region GetUserByNo
        [TestMethod]
        public void GetUserByNo_Test_Successed()
        {
            var expected = testItems[1];

            var actual = controller.GetUserByNo(expected.UserNo).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<UserDto>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expected.UserNo, actualResult.Content.UserNo);
        }

        [TestMethod]
        public void GetUserByNo_Test_Not_Found()
        {
            var expected = "999";

            var actual = controller.GetUserByNo(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetUserByNo_Test_Bad_Request_With_Message_When_Args_Is_Empty()
        {
            var expected = string.Empty;

            var actual = controller.GetUserByNo(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }
        #endregion

        #region PostUser
        [TestMethod]
        public void PostUser_Test_Successed()
        {
            var expected = new UserDto
            {
                UserNo = "999",
                UserName = "test-user"
            };

            mockUserRepo.Setup(m => m.Create()).Returns(() => new UserEntity { });
            mockUserRepo.Setup(m => m.Insert(It.IsAny<UserEntity>())).Returns(() => 1);

            var actual = controller.PostUser(expected).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as CreatedAtRouteNegotiatedContentResult<int>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult.Content);
            Assert.AreEqual(expected.UserNo, actualResult.RouteValues[nameof(expected.UserNo)]);
        }

        [TestMethod]
        public void PostUser_Test_Bad_Request_With_Message_When_Args_Is_Null()
        {
            UserDto expected = null;

            var actual = controller.PostUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void PostUser_Test_Exception()
        {
            var expected = new UserDto { };

            mockUserRepo.Setup(m => m.Create()).Returns(() => new UserEntity { });
            mockUserRepo.Setup(m => m.Insert(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new Exception("mock test");
            });

            var actual = controller.PostUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }

        [TestMethod]
        public void PostUser_Test_Db_Update_Exception()
        {
            var expected = new UserDto { };

            mockUserRepo.Setup(m => m.Create()).Returns(() => new UserEntity { });
            mockUserRepo.Setup(m => m.Insert(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new DbUpdateException("mock test");
            });

            var actual = controller.PostUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }

        [TestMethod]
        public void PostUser_Test_Db_Update_Exception_When_Conflict()
        {
            var expected = new UserDto
            {
                UserNo = testItems[1].UserNo,
                UserName = testItems[1].UserName
            };

            mockUserRepo.Setup(m => m.Create()).Returns(() => new UserEntity { });
            mockUserRepo.Setup(m => m.Insert(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new DbUpdateException("mock test");
            });

            var actual = controller.PostUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ConflictResult));
        }
        #endregion

        #region DeleteUser
        [TestMethod]
        public void DeleteUser_Test_Successed()
        {
            var expected = testItems[1];

            mockUserRepo.Setup(m => m.Delete(It.IsAny<UserEntity>())).Returns(() => 1);

            var actual = controller.DeleteUser(expected.UserNo).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expected.UserNo, actualResult.Content);
        }

        [TestMethod]
        public void DeleteUser_Test_Bad_Request_With_Message_When_Args_Is_Null()
        {
            var actual = controller.DeleteUser(string.Empty).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void DeleteUserByNo_Test_Not_Found()
        {
            var expected = "999";

            var actual = controller.DeleteUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteUser_Test_Exception()
        {
            var expected = testItems[1];

            mockUserRepo.Setup(m => m.Delete(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new Exception("mock test");
            });

            var actual = controller.DeleteUser(expected.UserNo).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }

        [TestMethod]
        public void DeleteUser_Test_Db_Update_Concurrency_Exception()
        {
            var expected = testItems[1];

            mockUserRepo.Setup(m => m.Delete(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new DbUpdateConcurrencyException("mock test");
            });

            var actual = controller.DeleteUser(expected.UserNo).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }

        [TestMethod]
        public void DeleteUser_Test_Db_Update_Exception_When_No_Content()
        {
            var expected = testItems[1];

            mockUserRepo.Setup(m => m.Delete(It.IsAny<UserEntity>())).Callback((UserEntity p) =>
            {
                testItems.Remove(p);
                throw new DbUpdateConcurrencyException("mock test");
            });

            var actual = controller.DeleteUser(expected.UserNo).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(StatusCodeResult));

            var actualResult = actual as StatusCodeResult;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(HttpStatusCode.NoContent, actualResult.StatusCode);
        }
        #endregion

        #region PutUser
        [TestMethod]
        public void PutUser_Test_Successed()
        {
            var expected = new UserDto
            {
                UserNo = testItems[1].UserNo,
                UserName = testItems[1].UserName
            };

            mockUserRepo.Setup(m => m.Update(It.IsAny<UserEntity>())).Returns(() => 1);

            var actual = controller.PutUser(expected).Result;
            Assert.IsNotNull(actual);

            var actualResult = actual as StatusCodeResult;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(HttpStatusCode.NoContent, actualResult.StatusCode);
        }

        [TestMethod]
        public void PutUser_Test_Bad_Request_With_Message_When_Args_Is_Null()
        {
            var actual = controller.PutUser(null).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void PutUserByNo_Test_Not_Found()
        {
            var expected = new UserDto { UserNo = "999" };

            var actual = controller.PutUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutUser_Test_Exception()
        {
            var expected = new UserDto
            {
                UserNo = testItems[1].UserNo,
                UserName = testItems[1].UserName
            };

            mockUserRepo.Setup(m => m.Update(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new Exception("mock test");
            });

            var actual = controller.PutUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }

        [TestMethod]
        public void PutUser_Test_Db_Update_Concurrency_Exception()
        {
            var expected = new UserDto
            {
                UserNo = testItems[1].UserNo,
                UserName = testItems[1].UserName
            };

            mockUserRepo.Setup(m => m.Update(It.IsAny<UserEntity>())).Callback(() =>
            {
                throw new DbUpdateConcurrencyException("mock test");
            });

            var actual = controller.PutUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ExceptionResult));
        }

        [TestMethod]
        public void PutUser_Test_Db_Update_Exception_When_Not_Found()
        {
            var expected = new UserDto
            {
                UserNo = testItems[1].UserNo,
                UserName = testItems[1].UserName
            };

            mockUserRepo.Setup(m => m.Update(It.IsAny<UserEntity>())).Callback((UserEntity p) =>
            {
                testItems.Remove(p);
                throw new DbUpdateConcurrencyException("mock test");
            });

            var actual = controller.PutUser(expected).Result;
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
        #endregion
    }
}