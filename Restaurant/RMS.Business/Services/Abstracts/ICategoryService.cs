using RMS.Business.DTOs.CategoryDTOs;
using RMS.Core.Entities;
using System.Linq.Expressions;

namespace RMS.Business.Services.Abstracts
{
    public interface ICategoryService
    {
        Task Create(CategoryCreateDTO categoryCreateDTO);
        Task DeleteCategory(int id);
        Task SoftDeleteCategory(int id);
        Task<CategoryUpdateDTO> GetCategoryForUpdate(int id);
        Task<CategoryGetDTO> GetCategory(Func<Category, bool>? func = null);
        Task<List<CategoryGetDTO>> GetAllCategories(
            Expression<Func<Category, bool>>? func = null,
            Expression<Func<Category, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes );
        Task Update(int id,CategoryUpdateDTO categoryUpdateDTO);
    }
}
