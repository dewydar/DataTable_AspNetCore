using Microsoft.EntityFrameworkCore;
using DT.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DT.DAL.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected AppDbContext _db;
        protected DbSet<T> _dbSet;
        public RepositoryBase() : this(new AppDbContext()) { }
        public RepositoryBase(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public DbContext db
        {
            get { return _db; }
        }
        public async Task<T> GetOne(Expression<Func<T, bool>>? filter = null, List<string>? includes = null)
        {
            try
            {
                if (filter != null)
                    return await _db.Set<T>().AsNoTracking().WithIncludes(includes).FirstOrDefaultAsync(filter);
                else
                    return await _db.Set<T>().AsNoTracking().WithIncludes(includes).FirstOrDefaultAsync();

            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<List<T>> GetList(Expression<Func<T, bool>>? filter = null, List<string>? includes = null)
        {
            try
            {
                if (filter != null)
                    return await _db.Set<T>().AsNoTracking().WithIncludes(includes).Where(filter).ToListAsync();
                else
                    return await _db.Set<T>().AsNoTracking().WithIncludes(includes).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<TType>> GetList<TType>(Expression<Func<T, TType>> select
             , Expression<Func<T, bool>>? filter = null, List<string>? includes = null) where TType : class
        {
            if (filter != null)
                return await _db.Set<T>().Where(filter).AsNoTracking().WithIncludes(includes).Select(select).ToListAsync();
            else
                return await _db.Set<T>().AsNoTracking().WithIncludes(includes).Select(select).ToListAsync();
        }
        public async Task<bool> Add(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
                if (await _db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> AddRange(List<T> entity)
        {
            try
            {
                await _db.AddRangeAsync(entity);
                if (await _db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> Delete(T entity)
        {
            try
            {
                _db.Remove(entity);
                if (await _db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteRange(List<T> entity)
        {
            try
            {
                _db.RemoveRange(entity);
                if (await _db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update(T entity)
        {
            try
            {
                _db.Update(entity);
                if (await _db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> UpdateRange(List<T> entities)
        {
            try
            {
                _db.UpdateRange(entities);
                if (await _db.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public async void Exception(Exception entity, string controllerName, string funName)
        //{
        //    ExceptionLog e = new ExceptionLog()
        //    {
        //        ErrorDate = DateTime.Now,
        //        Url = "/" + controllerName + "/" + funName,
        //        FunctionName = funName,
        //        InnerException = entity.InnerException?.Message,
        //        Message = entity.Message,
        //        StackTrace = entity.StackTrace,
        //        Source = entity.Source,
        //    };
        //    _db.AddAsync(e);
        //    _db.SaveChangesAsync();
        //}
    }
    public static class LinqExtensions
    {
        public static IQueryable<T> WithIncludes<T>(this IQueryable<T> source, List<string> associations) where T : class
        {
            var query = (IQueryable<T>)source;
            if (associations != null)
                foreach (var assoc in associations)
                {
                    query = query.Include(assoc);
                }
            return query;
        }
    }

}
