using RMS.Business.DTOs.CategoryDTOs;
using RMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
