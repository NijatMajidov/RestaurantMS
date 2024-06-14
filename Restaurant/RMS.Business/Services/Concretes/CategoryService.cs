using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.Services.Abstracts;
using RMS.Core.Models;
using RMS.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Business.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task Create(CategoryCreateDTO categoryCreateDTO)
        {
            Category entity = _mapper.Map<Category>(categoryCreateDTO);
            _categoryRepository.Add(entity);
            await _categoryRepository.CommitAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var entity = await _categoryRepository.GetAsync(x=> x.Id == id);
            if(entity == null) throw new FileNotFoundException();
            _categoryRepository.Delete(entity);
            await _categoryRepository.CommitAsync();
        }

        public async Task<List<CategoryGetDTO>> GetAllCategories(
            Expression<Func<Category, bool>>? func = null,
            Expression<Func<Category, object>>? orderBy = null,
            bool isOrderByDesting = false, 
            params string[]? includes)
        {
            var queryable = _categoryRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            var result = new List<CategoryGetDTO>();
            foreach(var entity in queryable)
            {
                var getDTO = _mapper.Map<CategoryGetDTO>(entity);
                result.Add(getDTO);
            }
            return result;
        }

        public async Task<CategoryGetDTO> GetCategory(Func<Category, bool>? func = null)
        {
            var entity = _categoryRepository.Get(func);

            CategoryGetDTO dto = _mapper.Map<CategoryGetDTO>(entity);
            return dto;
        }

        public async Task SoftDeleteCategory(int id)
        {
            var entity = await _categoryRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new FileNotFoundException("bosh olmaz");
            _categoryRepository.SoftDelete(entity);
            await _categoryRepository.CommitAsync();
        }
        public async Task<CategoryUpdateDTO> GetCategoryForUpdate(int id)
        {
            var entity = await _categoryRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new FileNotFoundException();

            var categoryUpdateDTO = _mapper.Map<CategoryUpdateDTO>(entity);
            return categoryUpdateDTO;
        }
        public async Task Update(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            Category oldCategory = _categoryRepository.Get(x=>x.Id == id);
            if (oldCategory == null) throw new FileNotFoundException();

            oldCategory.Name = categoryUpdateDTO.Name;
            await _categoryRepository.CommitAsync();
        }
    }
}
