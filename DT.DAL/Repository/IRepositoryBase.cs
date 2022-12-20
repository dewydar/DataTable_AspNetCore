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
    public interface IRepositoryBase<T> where T : class
    {
        DbContext db { get; }
        Task<T> GetOne(Expression<Func<T, bool>>? filter = null, List<string>? includes = null);
        Task<List<T>> GetList(Expression<Func<T, bool>>? filter = null, List<string>? includes = null);
        Task<List<TType>> GetList<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>>? filter = null, List<string>? includes = null) where TType : class;
        Task<bool> Add(T entity);
        Task<bool> AddRange(List<T> entity);
        Task<bool> Update(T entity);
        Task<bool> UpdateRange(List<T> entity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteRange(List<T> entity);
        //public void Exception(Exception ex, string controllerName, string funName);
    }
}
