using RMS.Business.DTOs.TableDTOs;
using RMS.Core.Entities;
using System.Linq.Expressions;

namespace RMS.Business.Services.Abstracts
{
    public interface ITableService
    {
        Task Create(TableCreateDto CreateDTO);
        Task DeleteTable(int id);
        Task SoftDeleteTable(int id);
        Task<TableUpdateDto> GetTableForUpdate(int id);
        Task Update(int id, TableUpdateDto UpdateDTO);
        Task<TableGetDto> GetTable(Func<Table, bool>? func = null);
        Task<List<TableGetDto>> GetAllTables(
            Expression<Func<Table, bool>>? func = null,
            Expression<Func<Table, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes);
        
    }
}
