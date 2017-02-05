
namespace Examples.Ci.WebApi.Controllers
{
    using Core.Utilities;
    using Ef.Repositories;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class UsersController : ApiController
    {
        #region IDisposable Implements
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //TODO: release resources
            }

            base.Dispose(disposing);
        }
        #endregion

        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUsers()
        {
            var result = await Runner.Execute(() =>
            {
                //return userRepo.View;

                return userRepo.View.Select(UserDto.UserEntityParse);

                // 使用注入的 DbContext 来进行操作
                //return context.Set<UserEntity>().Select(UserDto.UserEntityParse);

                // 非注入，直接使用 DbContext 来进行操作
                //return db.Users.Select(UserDto.UserEntityParse);
            });

            if (result.HasError)
            {
                return InternalServerError(result.Error);
            }
            else if (result.Content == null || result.Content.Count() <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(result.Content);
            }
        }
    }
}
