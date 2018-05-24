using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INC.EntityFramework.Repository;
using System.Data.Entity;

namespace INC.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUowOfWork,IDisposable
    {
        private readonly IRepositoryContainer _repositoryContainer;

        public UnitOfWork(DbContext dbContext)
        {
            this._repositoryContainer = new RepositoryContainer(dbContext);
        }

        public void Dispose()
        {
            this._repositoryContainer.Dispose();
        }

        public IRepository<T> Repositories<T>() where T: class
        {
            return this._repositoryContainer.Get<T>();
        }
    }
}
