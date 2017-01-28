
namespace Examples.WebApi.Controllers
{
    using Infrastructures;
    using Models;
    using Repositories;
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    //[ExcludeFromCodeCoverage]
    public class UsersController : ApiController
    {
        //private ExampleDbContext db = new ExampleDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }

            base.Dispose(disposing);
        }

        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        //private readonly DbContext context;

        //public UsersController(DbContext context)
        //{
        //    this.context = context;
        //}

        private IHttpActionResult Get<T>(ExecuteResult<T> result)
        {
            if (result.HasError)
            {
                return InternalServerError(result.Error);
            }
            else if (result.Content == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result.Content);
            }
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
            return Get(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserByNo(string userNo)
        {
            if (string.IsNullOrEmpty(userNo))
            {
                return BadRequest(nameof(userNo));
            }

            var result = await Runner.Execute(() =>
            {
                return userRepo.View.Select(UserDto.UserEntityParse).FirstOrDefault(p => p.UserNo == userNo);

                // 非注入，直接使用 DbContext 来进行操作
                //return db.Users.Select(UserDto.UserEntityParse).FirstOrDefault(p => p.UserNo == userNo);
            });
            return Get(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(nameof(userName));
            }

            var result = await Runner.Execute(() =>
            {
                return userRepo.View.Select(UserDto.UserEntityParse).Where(p => p.UserName.Contains(userName));

                // 非注入，直接使用 DbContext 来进行操作
                //return db.Users.Select(UserDto.UserEntityParse).Where(p => p.UserName.Contains(userName));
            });
            return Get(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostUser(UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(nameof(userDto));
            }

            var result = await Runner.Execute(() =>
            {
                // 非注入，直接使用 DbContext 来进行操作
                //var user = new UserEntity
                //{
                //    CreatedAt = DateTime.Now,
                //    UserNo = userDto.UserNo,
                //    UserName = userDto.UserName
                //};
                //db.Users.Add(user);
                //return db.SaveChanges();                

                var user = userRepo.Create();
                user.UserNo = userDto.UserNo;
                user.UserName = userDto.UserName;
                user.CreatedAt = DateTime.Now;

                return userRepo.Insert(user);
            });

            if (result.HasError)
            {
                if (result.Error is DbUpdateException)
                {
                    var isExists = userRepo.View.Count(p => p.UserNo == userDto.UserNo) > 0;
                    if (isExists)
                    {
                        return Conflict();
                    }
                }

                return InternalServerError(result.Error);
            }

            return CreatedAtRoute("DefaultApi", new { userDto.UserNo }, result.Content);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser(string userNo)
        {
            if (string.IsNullOrEmpty(userNo))
            {
                return BadRequest(nameof(userNo));
            }

            var result = await Runner.Execute(() =>
            {
                // 非注入，直接使用 DbContext 来进行操作
                //var user = db.Users.FirstOrDefault(p => p.UserNo == userNo);
                //if (user == null)
                //{
                //    return 0;
                //}
                //db.Users.Remove(user);
                //return db.SaveChanges();

                var user = userRepo.View.FirstOrDefault(p => p.UserNo == userNo);
                if (user == null)
                {
                    return 0;
                }

                return userRepo.Delete(user);
            });

            if (result.HasError)
            {
                if (result.Error is DbUpdateConcurrencyException)
                {
                    var isDeleted = userRepo.View.Count(p => p.UserNo == userNo) == 0;
                    if (isDeleted)
                    {
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                }

                return InternalServerError(result.Error);
            }
            else if (result.Content == 0)
            {
                return NotFound();
            }

            return Ok(userNo);
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutUser(UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(nameof(userDto));
            }

            var result = await Runner.Execute(() =>
            {
                // 非注入，直接使用 DbContext 来进行操作
                //var user = db.Users.FirstOrDefault(p => p.UserNo == userDto.UserNo);
                //if (user == null)
                //{
                //    return 0;
                //}
                //user.UserName = userDto.UserName;
                //db.Entry(user).State = EntityState.Modified;
                //return db.SaveChanges();

                var user = userRepo.View.FirstOrDefault(p => p.UserNo == userDto.UserNo);
                if (user == null)
                {
                    return 0;
                }

                user.UserName = userDto.UserName;

                return userRepo.Update(user);
            });

            if (result.HasError)
            {
                if (result.Error is DbUpdateConcurrencyException)
                {
                    var isDeleted = userRepo.View.Count(p => p.UserNo == userDto.UserNo) == 0;
                    if (isDeleted)
                    {
                        return NotFound();
                    }
                }

                return InternalServerError(result.Error);
            }
            else if (result.Content == 0)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
