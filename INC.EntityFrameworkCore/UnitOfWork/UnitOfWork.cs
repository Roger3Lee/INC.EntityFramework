using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace INC.EntityFrameworkCore
{
    public class UnitOfWork : UnitOfWork<DbContext>
    {
        public UnitOfWork(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class UnitOfWork<T> : IUowOfWork, IDisposable where T : DbContext
    {
        private T _context;

        public T Context => this._context;

        private readonly IRepositoryContainer _repositoryContainer;

        public UnitOfWork(T dbContext)
        {
            this._context = dbContext;
            this._repositoryContainer = new RepositoryContainer(dbContext);
        }

        public void Dispose()
        {
            this._repositoryContainer.Dispose();
        }

        public IRepository<T> Repositories<T>() where T : class
        {
            return this._repositoryContainer.Get<T>();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
    }
}
