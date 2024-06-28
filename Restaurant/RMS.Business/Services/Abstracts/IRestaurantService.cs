using RMS.Core.Entities;
using System.Linq.Expressions;

namespace RMS.Business.Services.Abstracts
{
    public interface IRestaurantService
    {
        Task Create(Restaurant CreateDTO);
        Task DeleteTable(int id);
        Task SoftDeleteTable(int id);
        Task Update(int id, Restaurant UpdateDTO);
        Task<Restaurant> GetTable(Func<Restaurant, bool>? func = null);
        Task<List<Restaurant>> GetAllTables(
            Expression<Func<Restaurant, bool>>? func = null,
            Expression<Func<Restaurant, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes);
    }
}
