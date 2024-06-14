using Microsoft.EntityFrameworkCore;
using RMS.Core.Models;
using RMS.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly RMSAppContext _context;
        DbSet<T> _table;

        public Repository(RMSAppContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
        }

        public int Commit()
        {
           return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public T Get(Func<T, bool>? func = null)
        {
            return func == null ? 
                _table.FirstOrDefault() :
                _table.Where(func).FirstOrDefault();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>>? func)
        {
            return func == null ?
                await _table.FirstOrDefaultAsync() :
                await _table.FirstOrDefaultAsync(func);
        }

        public async Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? func = null,
            Expression<Func<T, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes)
        {
            IQueryable<T> data = _table;
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    data = data.Include(includes[i]);
                }
            }
            if (orderBy is not null)
            {
                data = isOrderByDesting ?
                     data.OrderByDescending(orderBy):
                     data.OrderBy(orderBy);
            }

            return func == null ? data : data.Where(func);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? func = null, Expression<Func<T, object>>? orderBy = null, bool isOrderByDesting = false, params string[]? includes)
        {
            IQueryable<T> data = _table;
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    data = data.Include(includes[i]);
                }
            }
            if (orderBy is not null)
            {
                data = isOrderByDesting ?
                     data.OrderByDescending(orderBy) :
                     data.OrderBy(orderBy);
            }

            return func == null ? data : data.Where(func);
        }
    }
}
