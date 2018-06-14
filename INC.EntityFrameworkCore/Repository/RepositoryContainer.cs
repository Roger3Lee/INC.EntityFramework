using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace INC.EntityFrameworkCore
{
    public class RepositoryContainer : IRepositoryContainer, IDisposable
    {
        public readonly DbContext _context;

        public RepositoryContainer(DbContext context)
        {
            this._context = context;
        }

        public IRepository<T> Get<T>() where T: class
        {
            return new Repository<T>(_context);
        }
        
        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
