
namespace Examples.Ci.WebApi.Controllers
{
    using Castle.MicroKernel;
    using Infrastructures.Ioc;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Utils;

    [ExcludeFromCodeCoverage]
    public class TestController : ApiController
    {
        private readonly ITest test;

        #region enable priority from ioc
        //public TestController(ITest test)
        //{
        //    this.test = test;
        //}
        #endregion

        public TestController(ISettings settings)
        {
            test = BootstrapContainer.Container.Resolve<ITest>(new Arguments(new { message = settings.DefaultConnectionString }));
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
