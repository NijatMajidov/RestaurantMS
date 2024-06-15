using AutoMapper;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Exceptions.CategoryEx;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;


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
            if (categoryCreateDTO == null) 
                throw new EntityNullReferenceException("", "Category null");
            if (categoryCreateDTO.Name.Length < 3 || categoryCreateDTO.Name.Length>50) 
                throw new CategoryNameSizeException("Name", "Category Name size 3-50 char");
            if (!Regex.IsMatch(categoryCreateDTO.Name, @"^[a-zA-Z]+$"))
                throw new CategoryNameFormatException("Name", "Category Name must contain only letters (a-z, A-Z)");
            var existingCategory = await _categoryRepository.GetAsync(x=>x.Name == categoryCreateDTO.Name);
            if (existingCategory != null)
            {
                throw new DuplicateException("Name", "Eyni adli Categoriya ola bilmez");
            }
            
            var entity = _mapper.Map<Category>(categoryCreateDTO);
            _categoryRepository.Add(entity);
            await _categoryRepository.CommitAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var entity = await _categoryRepository.GetAsync(x=> x.Id == id);
            if(entity == null) throw new EntityNotFoundException("", "Category not found");
            _categoryRepository.Delete(entity);
            await _categoryRepository.CommitAsync();
        }

        public async Task<List<CategoryGetDTO>> GetAllCategories(
            Expression<Func<Category, bool>>? func = null,
            Expression<Func<Category, object>>? orderBy = null,
            bool isOrderByDesting = false, 
            params string[]? includes)
        {
            var queryable = await _categoryRepository.GetAllAsync(func, orderBy, isOrderByDesting, includes);
            return _mapper.Map<List<CategoryGetDTO>>(queryable);
        }

        public async Task<CategoryGetDTO> GetCategory(Func<Category, bool>? func = null)
        {
            var entity = _categoryRepository.Get(func);
            if(entity == null) throw new EntityNotFoundException("", "Category not Found");
            CategoryGetDTO dto = _mapper.Map<CategoryGetDTO>(entity);
            return dto;
        }

        public async Task SoftDeleteCategory(int id)
        {
            var entity = await _categoryRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "Category not found");
            _categoryRepository.SoftDelete(entity);
            await _categoryRepository.CommitAsync();
        }
        public async Task<CategoryUpdateDTO> GetCategoryForUpdate(int id)
        {
            var entity = await _categoryRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "Category not found");

            var categoryUpdateDTO = _mapper.Map<CategoryUpdateDTO>(entity);
            return categoryUpdateDTO;
        }
        public async Task Update(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            if (categoryUpdateDTO == null) 
                throw new EntityNullReferenceException("", "Category bos ola bilmez");
            if (categoryUpdateDTO.Name.Length < 3 || categoryUpdateDTO.Name.Length > 50) 
                throw new CategoryNameSizeException("Name", "Category Name size 3-50 char");
            if (!Regex.IsMatch(categoryUpdateDTO.Name, @"^[a-zA-Z]+$"))
                throw new CategoryNameFormatException("Name", "Category Name must contain only letters (a-z, A-Z)");
            var existingCategory = await _categoryRepository.GetAsync(x => x.Name == categoryUpdateDTO.Name && x.Id!=id);
            if (existingCategory != null)
            {
                throw new DuplicateException("Name", "Eyni adli Categoriya ola bilmez");
            }
            Category oldCategory = _categoryRepository.Get(x=>x.Id == id);
            if (oldCategory == null) throw new EntityNotFoundException("", "Bele bir Category yoxdur");

            oldCategory.Name = categoryUpdateDTO.Name;
            await _categoryRepository.CommitAsync();
        }
    }
}
