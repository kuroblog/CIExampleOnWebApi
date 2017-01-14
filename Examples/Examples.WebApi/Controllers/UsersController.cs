
namespace Examples.WebApi.Controllers
{
    using Infrastructures;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class UsersController : ApiController
    {
        private ExampleDbContext db = new ExampleDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

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
                return db.Users.Select(UserDto.UserEntityParse);
            });
            return Get(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserByNo(string userNo)
        {
            if (string.IsNullOrEmpty(userNo))
            {
                return BadRequest();
            }

            var result = await Runner.Execute(() =>
            {
                return db.Users.Select(UserDto.UserEntityParse).FirstOrDefault(p => p.UserNo == userNo);
            });
            return Get(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var result = await Runner.Execute(() =>
            {
                return db.Users.Select(UserDto.UserEntityParse).Where(p => p.UserName.Contains(userName));
            });
            return Get(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostUser(UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var result = await Runner.Execute(() =>
            {
                var user = new UserEntity
                {
                    CreatedAt = DateTime.Now,
                    UserNo = userDto.UserNo,
                    UserName = userDto.UserName
                };
                db.Users.Add(user);

                return db.SaveChanges();
            });

            if (result.HasError)
            {
                if (result.Error is DbUpdateException)
                {
                    var isExists = db.Users.Count(p => p.UserNo == userDto.UserNo) > 0;
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
                return BadRequest();
            }

            var result = await Runner.Execute(() =>
            {
                var user = db.Users.FirstOrDefault(p => p.UserNo == userNo);
                if (user == null)
                {
                    return 0;
                }

                db.Users.Remove(user);
                return db.SaveChanges();
            });

            if (result.HasError)
            {
                if (result.Error is DbUpdateConcurrencyException)
                {
                    var isDeleted = db.Users.Count(p => p.UserNo == userNo) == 0;
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
                return BadRequest();
            }

            var result = await Runner.Execute(() =>
            {
                var user = db.Users.FirstOrDefault(p => p.UserNo == userDto.UserNo);
                if (user == null)
                {
                    return 0;
                }

                user.UserName = userDto.UserName;
                db.Entry(user).State = EntityState.Modified;
                return db.SaveChanges();
            });

            if (result.HasError)
            {
                if (result.Error is DbUpdateConcurrencyException)
                {
                    var isDeleted = db.Users.Count(p => p.UserNo == userDto.UserNo) == 0;
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
