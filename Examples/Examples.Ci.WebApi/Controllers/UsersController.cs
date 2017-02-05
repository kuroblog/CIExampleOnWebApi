
namespace Examples.Ci.WebApi.Controllers
{
    using Core.Utilities;
    using Ef.Repositories;
    using Models;
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class UsersController : ApiController
    {
        #region IDisposable Implements
        [ExcludeFromCodeCoverage]
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
                return userRepo.View.Select(UserDto.UserEntityParse);
            });

            if (result.HasError)
            {
                return InternalServerError(result.Error);
            }
            else if (result.Content.Count() <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(result.Content);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUsersByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(nameof(userName));
            }

            var result = await Runner.Execute(() =>
            {
                return userRepo.View.Select(UserDto.UserEntityParse).Where(p => p.UserName.Contains(userName));
            });

            if (result.HasError)
            {
                return InternalServerError(result.Error);
            }
            else if (result.Content.Count() <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(result.Content);
            }
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
            });

            if (result.HasError)
            {
                return InternalServerError(result.Error);
            }
            else if (result.Content == null || string.IsNullOrEmpty(result.Content.UserNo))
            {
                return NotFound();
            }
            else
            {
                return Ok(result.Content);
            }
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
