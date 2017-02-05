
namespace Examples.Ci.Ef.Repositories
{
    using Entities;
    using System.Data.Entity;

    public interface IUserRepository : IBasicRepository<UserEntity> { }

    public class UserRepository : BasicRepository<UserEntity>, IUserRepository
    {
        #region BasicRepository Implements
        //[ExcludeFromCodeCoverage]
        public UserRepository(DbContext db) : base(db) { }
        #endregion
    }
}