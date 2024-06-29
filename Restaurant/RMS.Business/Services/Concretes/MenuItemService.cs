using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using System.Linq.Expressions;

namespace RMS.Business.Services.Concretes
{
    public class MenuItemService : IMenuItemService
    {
        readonly IMenuItemRepository _menuItemRepository;
        readonly ICategoryRepository _categoryRepository;
        readonly IWebHostEnvironment _webHostEnvironment;
        public MenuItemService(IMenuItemRepository repository, IWebHostEnvironment webHostEnvironment, ICategoryRepository categoryRepository)
        {
            _menuItemRepository = repository;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;

        }
        public async Task<IEnumerable<CategoryGetDTO>> GetAvailableCategoryAsync()
        {
            var cate = await _categoryRepository.GetAllAsync(t => !t.IsDeleted);
            return cate.Select(t => new CategoryGetDTO { Id = t.Id, Name = t.Name, }).ToList();

        }
        public async Task Create(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new EntityNullReferenceException("", "MenuItem null");
            if (menuItem.Name.Length < 2 || menuItem.Name.Length > 100)
                throw new NameSizeException("Name", "MenuItem Name size 2-100 char");
            if (menuItem.Price < 0.1)
                throw new PriceException("Price", "Qiymet musbet olmalidir,minumum 0,1manat");
            if (menuItem.ImageFile == null)
                throw new Exceptions.FileNotFoundException("ImageFile", "Image file not found");
            if (!menuItem.ImageFile.ContentType.Contains("image/"))
                throw new FileContentypeException("ImageFile", "Image file contenttype exception");
            if (menuItem.ImageFile.Length > 2097152)
                throw new FileSizeException("ImageFile", "Image file Size error!!");
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(menuItem.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\menu\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                menuItem.ImageFile.CopyTo(stream);
            }
            menuItem.ImageUrl = filename;
            _menuItemRepository.Add(menuItem);
            await _menuItemRepository.CommitAsync();
        }

        public async Task DeleteMenuItem(int id)
        {
            var menu = await _menuItemRepository.GetAsync(x => x.Id == id);
            if (menu == null)
                throw new EntityNotFoundException("", "menu item not found");
            string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\menu\" + menu.ImageUrl;
            if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image File not found!");
            {
                File.Delete(oldpath);
            }
            _menuItemRepository.Delete(menu);
            await _menuItemRepository.CommitAsync();
        }

        public async Task<List<MenuItem>> GetAllMenuItems(Expression<Func<MenuItem, bool>>? func = null, Expression<Func<MenuItem, object>>? orderBy = null, bool isOrderByDesting = false, params string[]? includes)
        {
            var queryable = await _menuItemRepository.GetAllAsync(func, orderBy, isOrderByDesting, includes);
            return queryable.ToList();
        }

        public async Task<MenuItem> GetMenuItem(Func<MenuItem, bool>? func = null)
        {
            var entity = _menuItemRepository.Get(func);
            if (entity == null) throw new EntityNotFoundException("", "menu item not Found");

            return entity;
        }

        public async Task SoftDeleteMenuItem(int id)
        {
            var entity = await _menuItemRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "Menu item not found");
            _menuItemRepository.SoftDelete(entity);
            await _menuItemRepository.CommitAsync();
        }

        public async Task Update(int id, MenuItem UpdateDTO)
        {
           var oldEntity = await _menuItemRepository.GetAsync(x => x.Id==id);
            if (oldEntity == null)
                throw new EntityNotFoundException("", "Menu Item not found!");
            if (UpdateDTO == null)
                throw new EntityNullReferenceException("", "Table null reference");

            if (UpdateDTO.Price < 0.1)
                throw new PriceException("Price", "Qiymet  minimum 0,1 ");

            if (UpdateDTO.Name.Length < 2 || UpdateDTO.Name.Length > 100)
                throw new NameSizeException("Name", "MenuItem Name size 2-100 char");
            if (UpdateDTO.ImageFile != null)
            {
                if (!UpdateDTO.ImageFile.ContentType.Contains("image/"))
                    throw new FileContentypeException("ImageFile", "Image file contenttype exception");
                if (UpdateDTO.ImageFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "Image file Size error!!");

                string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\menu\" + oldEntity.ImageUrl;
                if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image File not found!");
                {
                    File.Delete(oldpath);
                }

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(UpdateDTO.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\menu\" + filename;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    UpdateDTO.ImageFile.CopyTo(stream);
                }
                oldEntity.ImageUrl = filename;
            }
                
                oldEntity.Name = UpdateDTO.Name;
                oldEntity.Price = UpdateDTO.Price;
                oldEntity.CategoryId = UpdateDTO.CategoryId;
                oldEntity.Category = UpdateDTO.Category;
                oldEntity.Description = UpdateDTO.Description;
                await _menuItemRepository.CommitAsync();
            
        }
    }
}
