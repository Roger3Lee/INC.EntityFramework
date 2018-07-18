using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace INC.EntityFrameworkCore
{
    public class RepositoryContainer : IRepositoryContainer, IDisposable
    {
        private readonly DbContext _context;
        private object _lockObject = new object();
        private Dictionary<Type, object> _container = new Dictionary<Type, object>();

        public RepositoryContainer(DbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get The Repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> Get<T>() where T : class
        {
            lock (_lockObject)
            {
                Type type = typeof(T);
                if (_container.ContainsKey(type))
                    return _container[type] as IRepository<T>;
                else
                {
                    var resp = new Repository<T>(_context);
                    _container.Add(type, resp);
                    return resp;
                }
            }
        }
        
        public void Dispose()
        {
            _container = null;
            _lockObject = null;
        }
    }
}
