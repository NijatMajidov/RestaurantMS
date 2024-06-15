using RMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        void Add (T entity);
        void SoftDelete (T entity);
        void Delete (T entity);
        int Commit();
        Task<int> CommitAsync();
        T Get(Func<T, bool>? func = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? func);
        IQueryable<T> GetAll(
           Expression<Func<T, bool>>? func = null,
           Expression<Func<T, object>>? orderBy = null,
           bool isOrderByDesting = false,
           params string[]? includes);
        Task<IQueryable<T>> GetAllAsync(
           Expression<Func<T, bool>>? func = null,
           Expression<Func<T, object>>? orderBy = null,
           bool isOrderByDesting = false,
           params string[]? includes );
    }
}
