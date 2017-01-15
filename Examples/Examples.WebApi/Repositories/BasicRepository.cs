
namespace Examples.WebApi.Repositories
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public interface IBasicRepository<T> where T : class, new()
    {
        IQueryable<T> View { get; }
    }

    public class BasicRepository<T> : IDisposable, IBasicRepository<T> where T : class, new()
    {
        #region IDisposable Implements
        protected virtual void Dispose(bool flag)
        {
            //if (!flag)
            //{
            //    return;
            //}

            //db.Dispose();
        }

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
        #endregion

        #region IBasicRepository Implements
        public IQueryable<T> View
        {
            get { return db.Set<T>(); }
        }
        #endregion

        private readonly DbContext db;

        public BasicRepository(DbContext db)
        {
            this.db = db;
        }
    }

    public interface IUserRepository : IBasicRepository<UserEntity> { }

    public class UserRepository : BasicRepository<UserEntity>, IUserRepository
    {
        #region BasicRepository Implements
        public UserRepository(DbContext db) : base(db) { }
        #endregion
    }
}