using RMS.Business.DTOs.ReservationDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Core.Entities;
using System.Linq.Expressions;

namespace RMS.Business.Services.Abstracts
{
    public interface IReservationService
    {
        Task Create (ReservCreateDto reservation, string userId);
        Task<IEnumerable<TableGetDto>> GetAvailableTablesAsync();
        Task<ReservGetDto> GetReserv(Func<Reservation, bool>? func = null);
        Task<List<ReservGetDto>> GetAllReservs(
            Expression<Func<Reservation, bool>>? func = null,
            Expression<Func<Reservation, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params string[]? includes);
        Task DeleteRezerv(int id);
        Task Update(int id, ReservUpdateDto reservation);
    }
}
