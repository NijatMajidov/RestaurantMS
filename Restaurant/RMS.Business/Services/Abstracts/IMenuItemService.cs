using RMS.Business.DTOs.CategoryDTOs;
using RMS.Core.Entities;
using System.Linq.Expressions;

namespace RMS.Business.Services.Abstracts
{
    public interface IMenuItemService
    {
        Task Create(MenuItem menuItemCreateDTO);
        Task DeleteMenuItem(int id);
        Task SoftDeleteMenuItem(int id);
        Task<IEnumerable<CategoryGetDTO>> GetAvailableCategoryAsync();
        Task<MenuItem> GetMenuItem(Func<MenuItem, bool>? func = null);
        Task<List<MenuItem>> GetAllMenuItems(
            Expression<Func<MenuItem, bool>>? func = null,
            Expression<Func<MenuItem, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes);
        Task Update(int id, MenuItem menuItemUpdateDTO);
    }
}
