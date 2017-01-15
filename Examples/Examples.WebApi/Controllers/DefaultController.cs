
namespace Examples.WebApi.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;

    [ExcludeFromCodeCoverage]
    public class DefaultController : ApiController
    {
        private readonly IMessages messages;

        public DefaultController(IMessages messages)
        {
            this.messages = messages;
        }

        [HttpGet]
        public string GetHello()
        {
            var result = Runner.Execute(() => { return messages.Hello; }).Result;
            return result.Content;
        }
    }
    
    public interface IMessages
    {
        string Hello { get; }
    }

    [ExcludeFromCodeCoverage]
    public class Messages : IMessages
    {
        public string Hello
        {
            get { return "Helloworld"; }
        }
    }
}
