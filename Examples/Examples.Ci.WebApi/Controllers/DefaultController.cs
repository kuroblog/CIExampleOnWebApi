
namespace Examples.Ci.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class DefaultController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return await Task.FromResult(Ok($"{DateTime.Now}: Hello, World!"));
        }
    }
}
