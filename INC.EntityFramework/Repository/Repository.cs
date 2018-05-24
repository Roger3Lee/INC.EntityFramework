using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INC.EntityFramework.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            this._context = context;
        }

        public void Add(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this._context.Set<T>().AddRange(entities);
        }

        public List<T> All()
        {
            return this._context.Set<T>().ToList();
        }

        public List<T> All(Expression<Func<T, bool>> perdicate)
        {
            return this._context.Set<T>().Where(perdicate).ToList();
        }

        public List<T> All(Expression<Func<T, bool>> perdicate, string sort, int skip, int take)
        {
            return this._context.Set<T>().Where(perdicate).OrderBy(sort).Skip(skip).Take(take).ToList();
        }

        public T Get(Expression<Func<T, bool>> perdicate)
        {
            return this._context.Set<T>().FirstOrDefault(perdicate);
        }

        public void Remove(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            this._context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
    }
}
