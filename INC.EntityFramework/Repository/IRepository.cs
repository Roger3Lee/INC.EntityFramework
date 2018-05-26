using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INC.EntityFramework
{
    public interface IRepository <T>  where T : class
    {
        T Get(Expression<Func<T, bool>> perdicate);

        IList<T> All();

        IList<T> All(Expression<Func<T, bool>> perdicate);

        IList<T> All(Expression<Func<T, bool>> perdicate, string sort, int skip , int take);

        IList<T> AllAsNoTracking();

        IList<T> AllAsNoTracking(Expression<Func<T, bool>> perdicate);

        IList<T> AllAsNoTracking(Expression<Func<T, bool>> perdicate, string sort, int skip, int take);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void AddRange(params T[]  entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void RemoveRange(params T[] entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void UpdateRange(params T[] entities);
    }
}                  