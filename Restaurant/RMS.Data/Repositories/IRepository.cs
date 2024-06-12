using RMS.Core.Models;
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
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? func = null,
           Expression<Func<T, object>>? orderBy = null,
           bool isOrderByDesting = false,
           params string[]? includes);
    }
}
