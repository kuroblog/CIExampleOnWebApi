
namespace Examples.WebApi.Repositories.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    [TestClass]
    public class BasicRepository_Tests
    {
        //[TestMethod]
        //public void Dispose_Test()
        //{
        //    Assert.Fail();
        //}

        private Mock<DbContext> mockDbContext;
        private BasicRepository<UserEntity> testRepo;
        private List<UserEntity> testItems;

        [TestInitialize]
        public void Initialize()
        {
            mockDbContext = new Mock<DbContext>();
            testRepo = new BasicRepository<UserEntity>(mockDbContext.Object);

            testItems = new List<UserEntity> {
                new UserEntity { CreatedAt = DateTime.Now, UserNo = "001", UserName = "test1" },
                new UserEntity { CreatedAt = DateTime.Now, UserNo = "002", UserName = "test2" },
                new UserEntity { CreatedAt = DateTime.Now, UserNo = "003", UserName = "test3" }
            };
        }

        [TestMethod]
        public void Select_Test_Successed()
        {
            var expected = testItems[1];

            mockDbContext.Setup(m => m.Set<UserEntity>().Find(It.IsAny<object[]>())).Returns((object[] args) => testItems.FirstOrDefault(p => p.UserNo == args[0].ToString()));

            var actual = testRepo.Select(expected.UserNo);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.UserNo, actual.UserNo);
        }

        [TestMethod]
        public void Create_Test_Successed()
        {
            mockDbContext.Setup(m => m.Set<UserEntity>().Create()).Returns(() => testItems.LastOrDefault());

            var actual = testRepo.Create();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Insert_Test_Successed()
        {
            mockDbContext.Setup(m => m.Set<UserEntity>().Add(It.IsAny<UserEntity>())).Callback((UserEntity arg) => testItems.Add(arg));

            var count = testItems.Count;
            mockDbContext.Setup(m => m.SaveChanges()).Returns(() => testItems.Count - count);

            var expectedItem = new UserEntity
            {
                CreatedAt = DateTime.Now,
                UserNo = Guid.NewGuid().ToString("N"),
                UserName = "test999"
            };
            var actual = testRepo.Insert(expectedItem);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual);
            Assert.AreEqual(expectedItem.UserNo, testItems.LastOrDefault().UserNo);
        }

        [TestMethod]
        public void Delete_Test_Successed()
        {
            mockDbContext.Setup(m => m.Set<UserEntity>().Remove(It.IsAny<UserEntity>())).Callback((UserEntity arg) => testItems.Remove(arg));

            var count = testItems.Count;
            mockDbContext.Setup(m => m.SaveChanges()).Returns(() => testItems.Count - count);

            var actual = testRepo.Delete(testItems.LastOrDefault());

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, Math.Abs(actual));
        }

        [TestMethod]
        public void Delete_Test_Successed_When_Args_Is_Valid()
        {
            var expected = testItems.LastOrDefault();

            mockDbContext.Setup(m => m.Set<UserEntity>().Find(It.IsAny<object[]>())).Returns((object[] args) => testItems.FirstOrDefault(p => p.UserNo == args[0].ToString()));

            mockDbContext.Setup(m => m.Set<UserEntity>().Remove(It.IsAny<UserEntity>())).Callback((UserEntity arg) => testItems.Remove(arg));

            var count = testItems.Count;
            mockDbContext.Setup(m => m.SaveChanges()).Returns(() => testItems.Count - count);

            var actual = testRepo.Delete(expected.UserNo);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, Math.Abs(actual));
        }

        [TestMethod]
        public void Delete_Test_Successed_When_Args_Is_Invalid()
        {
            var expected = string.Empty;

            mockDbContext.Setup(m => m.Set<UserEntity>().Find(It.IsAny<object[]>())).Returns((object[] args) => testItems.FirstOrDefault(p => p.UserNo == args[0].ToString()));

            // 应为走了分支，所以这里不会走到，注意单元测试也是有代码覆盖率的
            //mockDbContext.Setup(m => m.Set<UserEntity>().Remove(It.IsAny<UserEntity>())).Callback((UserEntity arg) => testItems.Remove(arg));
            //var count = testItems.Count;
            //mockDbContext.Setup(m => m.SaveChanges()).Returns(() => testItems.Count - count);

            var actual = testRepo.Delete(expected);

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual);
        }

        //[TestMethod]
        //public void Update_Test_Successed()
        //{
        //    var expected = new UserEntity
        //    {
        //        CreatedAt = DateTime.Now,
        //        UserNo = testItems.LastOrDefault().UserNo,
        //        UserName = "test999"
        //    };

        //    var mockTmp = new Mock<DbEntityEntry<UserEntity>>();
        //    mockTmp.Setup(m => m.State).Returns(EntityState.Unchanged);

        //    mockDbContext.Setup(m => m.Entry(expected).State).Returns(() => mockTmp.Object.State);
        //    //mockDbContext.SetupProperty(m => m.Entry(expected).State, status);

        //    mockDbContext.Setup(m => m.SaveChanges()).Callback(() =>
        //    {
        //        testItems.Remove(testItems.LastOrDefault());
        //        testItems.Add(expected);
        //    }).Returns(() => testItems.LastOrDefault().UserNo == expected.UserNo ? 1 : 0);

        //    var actual = testRepo.Update(expected);

        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual(1, actual);
        //    Assert.AreEqual(expected.UserNo, testItems.LastOrDefault().UserNo);
        //    Assert.AreEqual(expected.UserName, testItems.LastOrDefault().UserName);
        //}

        //[TestMethod()]
        //public void Update_Test1()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void View_Test_Successed()
        {
            var mockUsers = new Mock<DbSet<UserEntity>>();
            mockUsers.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator()).Returns(() => testItems.GetEnumerator());

            mockDbContext.Setup(m => m.Set<UserEntity>()).Returns(() => mockUsers.Object);

            var actual = testRepo.View;

            Assert.IsNotNull(actual);
            Assert.AreNotEqual(0, actual.ToList().Count());
        }

        //[TestMethod()]
        //public void BasicRepository_Test()
        //{
        //    Assert.Fail();
        //}
    }
}