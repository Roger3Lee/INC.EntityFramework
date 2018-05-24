using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INC.EntityFramework.Repository
{
    public interface IRepository <T>  where T : class
    {
        T Get(Expression<Func<T, bool>> perdicate);

        List<T> All();

        List<T> All(Expression<Func<T, bool>> perdicate);

        List<T> All(Expression<Func<T, bool>> perdicate, string sort, int skip , int take);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);
    }
}                  