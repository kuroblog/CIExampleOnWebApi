
namespace Examples.Ci.Ef.Repositories
{
    using Entities;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    public interface IUserRepository : IBasicRepository<UserEntity> { }

    public class UserRepository : BasicRepository<UserEntity>, IUserRepository
    {
        #region BasicRepository
        [ExcludeFromCodeCoverage]
        public UserRepository(DbContext db) : base(db) { }
        #endregion
    }
}