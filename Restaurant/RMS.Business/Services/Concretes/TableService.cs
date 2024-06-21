using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RMS.Business.DTOs.TableDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Exceptions.TableEx;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
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
            var existingTable = await _tableRepository.GetAsync(x => x.Name == CreateDTO.Name);
            if (existingTable != null)
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
                throw new EntityNotFoundException("", "Table not found");
            string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\Tables\" + table.ImageUrl;
            if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image File not found!");
            {
                File.Delete(oldpath);
            }
            _tableRepository.Delete(table);
            await _tableRepository.CommitAsync();
        }

        public async Task<List<TableGetDto>> GetAllTables(
            Expression<Func<Table, bool>>? 
            func = null, Expression<Func<Table, object>>? 
            orderBy = null, bool isOrderByDesting = false, 
            params string[]? includes)
        {
            var queryable = await _tableRepository.GetAllAsync(func, orderBy, isOrderByDesting, includes);
            return _mapper.Map<List<TableGetDto>>(queryable);
        }

        public async Task<TableGetDto> GetTable(Func<Table, bool>? func = null)
        {
            var entity = _tableRepository.Get(func);
            if (entity == null) throw new EntityNotFoundException("", "Table not Found");
            
            return _mapper.Map<TableGetDto>(entity);
        }

        public async Task<TableUpdateDto> GetTableForUpdate(int id)
        {
            var entity = await _tableRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "Table not found");

            return _mapper.Map<TableUpdateDto>(entity);
        }

        public async Task SoftDeleteTable(int id)
        {
            var entity = await _tableRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "Table not found");
            _tableRepository.SoftDelete(entity);
            await _tableRepository.CommitAsync();
        }

        public async Task Update(int id, TableUpdateDto UpdateDTO)
        {
            var oldTable = await _tableRepository.GetAsync(x => x.Id == id);

            if (oldTable == null)
                throw new EntityNotFoundException("", "Bele bir Table yoxdur");

            if (UpdateDTO == null)
                throw new EntityNullReferenceException("", "Table null reference");

            if (UpdateDTO.Capacity < 1 || UpdateDTO.Capacity > 16)
                throw new TableCapacityException("Capacity", "Table Capacity minimum 3 maximum 16");

            if (UpdateDTO.Name.Length != 3)
                throw new NameSizeException("Name", "Table Name uzunlugu 3 char olmalidir: A12");

            if (!Regex.IsMatch(UpdateDTO.Name, @"^[A-Z]\d{2}$"))
                throw new NameFormatException("Name", "Table Name must start with one uppercase letter followed by two digits");

            var existingTable = await _tableRepository.GetAsync(x => x.Name == UpdateDTO.Name && x.Id!=id);
            if (existingTable != null)
                throw new DuplicateException("Name", "Eyni adli Table ola bilmez");


            if (UpdateDTO.ImageFile != null)
            { 
                if (!UpdateDTO.ImageFile.ContentType.Contains("image/"))
                  throw new FileContentypeException("ImageFile", "Image file contenttype exception");
                if (UpdateDTO.ImageFile.Length > 2097152)
                 throw new FileSizeException("ImageFile", "Image file Size error!!");

                string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\Tables\" + oldTable.ImageUrl;
                  if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image File not found!");
                  {
                         File.Delete(oldpath);
                  }

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(UpdateDTO.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\Tables\" + filename;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    UpdateDTO.ImageFile.CopyTo(stream);
                }

                oldTable.ImageUrl = filename;
            }
            
            oldTable.Name = UpdateDTO.Name;
            oldTable.Capacity = UpdateDTO.Capacity;
            await _tableRepository.CommitAsync();

        }
    }
}
