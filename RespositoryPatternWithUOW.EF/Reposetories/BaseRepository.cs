using Microsoft.EntityFrameworkCore;
using RespositoryPatternWithUOW.Core.Const;
using RespositoryPatternWithUOW.Core.Interfaces;
using System.Linq.Expressions;

namespace RespositoryPatternWithUOW.EF.Reposetories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _Context;

        public BaseRepository(ApplicationDbContext context)
        {
            _Context = context;

        }

        public T Add(T entity)
        {
            _Context.Set<T>().Add(entity);
            return entity;
        }

        public T AddRange(T entity)
        {
            _Context.Set<T>().AddRange(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _Context.Set<T>().AddRange(entities);
            return entities;
        }

        public void Attach(T entity)
        {
            _Context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _Context.Set<T>().AttachRange(entities);

        }

        public int Count()
        {
            return _Context.Set<T>().Count();
        }

        public void Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _Context.Set<T>().RemoveRange(entities);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)

        {
            IQueryable<T> query = _Context.Set<T>();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);


            return query.SingleOrDefault(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _Context.Set<T>();
            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query.Where(criteria);
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip, string[] includes = null)
        {
            IQueryable<T> query = _Context.Set<T>().Where(criteria).Skip(skip).Take(take);
            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);
            return query.ToList();

        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip, Expression<Func<T, object>> orderby = null, string orederByDirection = OrderBy.Ascending, string[] includes = null)
        {
            IQueryable<T> query = _Context.Set<T>().Where(criteria);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            if (orderby != null)
            {
                if (orederByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderby);
                else query = query.OrderByDescending(orderby);


            }
            return query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _Context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return _Context.Set<T>().Find(id);

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public T Update(T entity)
        {
            _Context.Update(entity);
            return entity;
        }
    }
}
