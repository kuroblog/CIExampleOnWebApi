
namespace Examples.WebApi.Repositories
{
    using Models;
    using System.Data.Entity;

    public interface IUserRepository : IBasicRepository<UserEntity> { }

    public class UserRepository : BasicRepository<UserEntity>, IUserRepository
    {
        #region BasicRepository Implements
        public UserRepository(DbContext db) : base(db) { }
        #endregion
    }
}