
namespace Examples.WebApi.Repositories
{
    using Models;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    public interface IUserRepository : IBasicRepository<UserEntity> { }

    public class UserRepository : BasicRepository<UserEntity>, IUserRepository
    {
        #region BasicRepository Implements
        [ExcludeFromCodeCoverage]
        public UserRepository(DbContext db) : base(db) { }
        #endregion
    }
}