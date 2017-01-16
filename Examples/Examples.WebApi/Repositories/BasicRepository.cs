
namespace Examples.WebApi.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public interface IBasicRepository<T> where T : class, new()
    {
        IQueryable<T> View { get; }

        T Select(params object[] keys);

        T Create();

        int Insert(T entity);

        int Delete(T entity);

        int Delete(params object[] keys);

        int Update(T entity);

        int Update(T entity, params object[] keys);
    }

    //[ExcludeFromCodeCoverage]
    public class BasicRepository<T> : IDisposable, IBasicRepository<T> where T : class, new()
    {
        #region IDisposable Implements
        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool flag)
        {
            if (flag)
            {
                context.Dispose();
            }
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IBasicRepository Implements
        public IQueryable<T> View
        {
            get { return context.Set<T>(); }
        }

        public T Select(params object[] keys)
        {
            return context.Set<T>().Find(keys);
        }

        public T Create()
        {
            return context.Set<T>().Create();
        }

        public int Insert(T entity)
        {
            context.Set<T>().Add(entity);

            return context.SaveChanges();
            //return IsAutoCommit ? Context.SaveChanges() : 0;
        }

        public int Delete(T entity)
        {
            context.Set<T>().Remove(entity);

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

        [ExcludeFromCodeCoverage]
        public int Update(T entity)
        {
            var state = context.Entry(entity).State;
            if (state != EntityState.Modified)
            {
                return 0;
            }

            return context.SaveChanges();
            //return IsAutoCommit ? Context.SaveChanges() : 0;
        }

        [ExcludeFromCodeCoverage]
        public int Update(T entity, params object[] keys)
        {
            var original = Select(keys);
            if (original == null)
            {
                return 0;
            }

            context.Entry(original).CurrentValues.SetValues(entity);

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