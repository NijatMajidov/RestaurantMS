using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RMS.Business.DTOs.CategoryDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Exceptions.CategoryEx;
using RMS.Business.Exceptions.TableEx;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using RMS.Data.Repositories.Implementations;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace RMS.Business.Services.Concretes
{
    public class TableService : ITableService
    {
        readonly ITableRepository _tableRepository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;

        public TableService(ITableRepository tableRepository, 
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Create(TableCreateDto CreateDTO)
        {
            if (CreateDTO == null) 
                throw new EntityNullReferenceException("", "Table null reference");
            if (CreateDTO.ImageFile == null) 
                throw new Exceptions.FileNotFoundException("ImageFile", "Image file not found");
            if (!CreateDTO.ImageFile.ContentType.Contains("image/")) 
                throw new FileContentypeException("ImageFile", "Image file contenttype exception");
            if (CreateDTO.ImageFile.Length > 2097152) 
                throw new FileSizeException("ImageFile", "Image file Size error!!");
            if (CreateDTO.Capacity < 1 || CreateDTO.Capacity > 16)
            {
                throw new TableCapacityException("Capacity", "Table Capacity minimum 3 maximum 16");
            }
            if (CreateDTO.Name.Length != 3)
                throw new NameSizeException("Name", "Table Name uzunlugu 3 char olmalidir: A12");
            if (!Regex.IsMatch(CreateDTO.Name, @"^[A-Z]\d{2}$"))
                throw new NameFormatException("Name", "Table Name must start with one uppercase letter followed by two digits");
            var existingCategory = await _tableRepository.GetAsync(x => x.Name == CreateDTO.Name);
            if (existingCategory != null)
            {
                throw new DuplicateException("Name", "Eyni adli Table ola bilmez");
            }

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(CreateDTO.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\Tables\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                CreateDTO.ImageFile.CopyTo(stream);
            }
            var entity = _mapper.Map<Table>(CreateDTO);
            entity.ImageUrl = filename;
            _tableRepository.Add(entity);
            await _tableRepository.CommitAsync();
        }

        public async Task DeleteTable(int id)
        {
            var table = await _tableRepository.GetAsync(x=>x.Id==id);
            if (table == null)
                throw new EntityNotFoundException("Table", "Table not found");

            _tableRepository.Delete(table);
            await _tableRepository.CommitAsync();
        }

        public Task<List<TableGetDto>> GetAllTables(
            Expression<Func<Category, bool>>? 
            func = null, Expression<Func<Category, object>>? 
            orderBy = null, bool isOrderByDesting = false, 
            params string[]? includes)
        {
            throw new NotImplementedException();
        }

        public Task<TableGetDto> GetTable(Func<Table, bool>? func = null)
        {
            throw new NotImplementedException();
        }

        public Task<TableUpdateDto> GetTableForUpdate(int id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteTable(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, TableUpdateDto UpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
