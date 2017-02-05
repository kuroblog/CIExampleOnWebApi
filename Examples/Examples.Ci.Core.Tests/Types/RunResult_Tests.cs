
namespace Examples.Ci.Core.Types.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RunResult_Tests
    {
        #region Initialize
        [TestInitialize]
        public void Initialize() { }
        #endregion

        [TestMethod]
        public void Get_Id_Test_Successed()
        {
            var expected = new RunResult<string>();

            var actual = expected.Id;
            Assert.IsNotNull(actual);
        }
    }
}