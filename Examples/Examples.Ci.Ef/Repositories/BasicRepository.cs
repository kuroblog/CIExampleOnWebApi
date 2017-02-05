
namespace Examples.Ci.Ef.Repositories
{
    using Core.Infrastructures;
    using System;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public interface IBasicRepository<TEntity> where TEntity : IBasicEntity, new()
    {
        IQueryable<TEntity> View { get; }

        TEntity Select(params object[] keys);

        TEntity Create();

        int Insert(TEntity entity);

        int Delete(TEntity entity);

        int Delete(params object[] keys);

        int Update(TEntity entity);

        int Update(TEntity entity, params object[] keys);
    }

    //TODO: unit test
    [ExcludeFromCodeCoverage]
    public class BasicRepository<TEntity> : IDisposable, IBasicRepository<TEntity> where TEntity : class, IBasicEntity, new()
    {
        #region IDisposable Implements
        //[ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        //[ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IBasicRepository Implements
        public IQueryable<TEntity> View
        {
            get { return context.Set<TEntity>(); }
        }

        public TEntity Select(params object[] keys)
        {
            return context.Set<TEntity>().Find(keys);
        }

        public TEntity Create()
        {
            return context.Set<TEntity>().Create();
        }

        public int Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);

            return context.SaveChanges();
            //return IsAutoCommit ? Context.SaveChanges() : 0;
        }

        public int Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);

            return context.SaveChanges();
            //return IsAutoCommit ? Context.SaveChanges() : 0;
        }

        public int Delete(params object[] keys)
        {
            var original = Select(keys);
            if (original == null)
            {
                return 0;
            }

            return Delete(original);
        }

        //[ExcludeFromCodeCoverage]
        public int Update(TEntity entity)
        {
            // TODO: 使用moq来模拟的时候出错，not supported
            var state = context.Entry(entity).State;
            if (state != EntityState.Modified)
            {
                return 0;
            }

            return context.SaveChanges();
            //return IsAutoCommit ? Context.SaveChanges() : 0;
        }

        //[ExcludeFromCodeCoverage]
        public int Update(TEntity entity, params object[] keys)
        {
            var original = Select(keys);
            if (original == null)
            {
                return 0;
            }

            context.Entry(original).CurrentValues.SetValues(entity);

            // TODO: 该函数没能通过moq来模拟
            return Update(entity);
        }
        #endregion

        private readonly DbContext context;

        public BasicRepository(DbContext context)
        {
            this.context = context;
        }
    }
}
