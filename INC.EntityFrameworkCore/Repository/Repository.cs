using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Concurrent;

namespace INC.EntityFrameworkCore
{
    public class Repository<T> : IRepository<T> where T: class 
    {
        [ThreadStatic]
        private ConcurrentQueue<object> _includeExpression = new ConcurrentQueue<object>();

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

        public void AddRange(params T[] entities)
        {
            var set = this._context.Set<T>();
            foreach (var entity in entities)
            {
                set.Add(entity);
            }
        }
        public IQueryable<T> All()
        {
            return BuildInclude().AsNoTracking();
        }

        public IQueryable<T> All(Expression<Func<T, bool>> perdicate)
        {
            return BuildInclude().Where(perdicate).AsNoTracking();
        }

        public IQueryable<T> All(Expression<Func<T, bool>> perdicate, string sort, int skip, int take)
        {
            return BuildInclude().Where(perdicate).SortBy(sort).Skip(skip).Take(take).AsNoTracking();
        }

        public int Count(Expression<Func<T, bool>> perdicate)
        {
            return this._context.Set<T>().Where(perdicate).Count();
        }

        public int Count()
        {
            return this._context.Set<T>().Count();
        }

        public T Get(Expression<Func<T, bool>> perdicate)
        {
            return this.BuildInclude().AsNoTracking().FirstOrDefault(perdicate);
        }

        public Repository<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            this._includeExpression.Enqueue(navigationPropertyPath);
            return this;
        }

        public Repository<T> Include(string navigationPropertyPath)
        {
            this._includeExpression.Enqueue(navigationPropertyPath);
            return this;
        }

        public void Remove(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }

        public void RemoveRange(params T[] entities)
        {
            var set = this._context.Set<T>();
            foreach (var entity in entities)
            {
                set.Remove(entity);
            }
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

        public void UpdateRange(params T[] entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        private IQueryable<T> BuildInclude()
        {
            IQueryable<T> query = this._context.Set<T>();
            if (this._includeExpression.Count > 0)
            {
                object includeExpression = null;
                while (this._includeExpression.TryDequeue(out includeExpression))
                {
                    var includeExpType = includeExpression.GetType();
                    var include = string.Empty;
                    if (includeExpType != typeof(string))
                        include = GetFieldNameByLambda(includeExpression as Expression);

                    query = query.Include(include);
                }
            }
            return query;
        }
        private string GetFieldNameByLambda(Expression exprBody)
        {
            var property = "";
            if (exprBody is LambdaExpression)
            {
                property = ((MemberExpression)((LambdaExpression)exprBody).Body).Member.Name;
            }else if (exprBody is UnaryExpression)
            {
                property = ((MemberExpression)((UnaryExpression)exprBody).Operand).Member.Name;
            }
            else if (exprBody is MemberExpression)
            {
                property = ((MemberExpression)exprBody).Member.Name;
            }
            else if (exprBody is ParameterExpression)
            {
                property = ((ParameterExpression)exprBody).Type.Name;
            }
            return property;
        }
    }
}
