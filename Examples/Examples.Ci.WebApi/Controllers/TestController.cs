
namespace Examples.Ci.WebApi.Controllers
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Web.Http;

    [ExcludeFromCodeCoverage]
    public class TestController : ApiController
    {
        private readonly ITest test;

        public TestController(ITest test)
        {
            this.test = test;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Hello()
        {
            test.Print();
            await Task.FromResult(0);

            return Ok();
        }
    }

    public interface ITest
    {
        void Print();
    }

    [ExcludeFromCodeCoverage]
    public class Test : ITest
    {
        public Test(string message)
        {
            this.message = message;
        }

        private string message;

        public void Print()
        {
            Debug.WriteLine(message);
        }
    }
}
