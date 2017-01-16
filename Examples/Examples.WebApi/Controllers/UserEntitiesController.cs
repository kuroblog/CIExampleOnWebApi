
namespace Examples.WebApi.Controllers
{
    using Examples.WebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [ExcludeFromCodeCoverage]
    public class UserEntitiesController : ApiController
    {
        private ExampleDbContext db = new ExampleDbContext();

        // GET: api/UserEntities
        public IQueryable<UserEntity> GetUserEntities()
        {
            return db.Users;
        }

        // GET: api/UserEntities/5
        [ResponseType(typeof(UserEntity))]
        public async Task<IHttpActionResult> GetUserEntity(Guid id)
        {
            UserEntity userEntity = await db.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            return Ok(userEntity);
        }

        // PUT: api/UserEntities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserEntity(Guid id, UserEntity userEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userEntity.Id)
            {
                return BadRequest();
            }

            db.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserEntities
        [ResponseType(typeof(UserEntity))]
        public async Task<IHttpActionResult> PostUserEntity(UserEntity userEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(userEntity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserEntityExists(userEntity.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userEntity.Id }, userEntity);
        }

        // DELETE: api/UserEntities/5
        [ResponseType(typeof(UserEntity))]
        public async Task<IHttpActionResult> DeleteUserEntity(Guid id)
        {
            UserEntity userEntity = await db.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            db.Users.Remove(userEntity);
            await db.SaveChangesAsync();

            return Ok(userEntity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserEntityExists(Guid id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}