using AutoMapper;
using RMS.Business.DTOs.ReservationDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace RMS.Business.Services.Concretes
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;
        readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository,ITableRepository tableRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TableGetDto>> GetAvailableTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync(t=>!t.IsDeleted);
            return tables.Select(t => new TableGetDto { Id = t.Id, Name = t.Name, Capacity = t.Capacity, ImageUrl = t.ImageUrl });
        }
        public async Task Create(ReservCreateDto reservation, string userId)
        {

            if (reservation == null) throw new EntityNullReferenceException("", "Reservation null reference");
            if(reservation.ReservDate == null) throw new EntityNullReferenceException("ReservDate", "Reserv Date null reference");
            if (reservation.ReservTime == null) throw new EntityNullReferenceException("ReservTime", "Reserv Date null reference");
            if (string.IsNullOrWhiteSpace(reservation.FirstName)) throw new NameFormatException("FirstName", "Name null");
            if (string.IsNullOrWhiteSpace(reservation.LastName)) throw new NameFormatException("LastName", "Surname null");
            
            if (reservation.GuestCount < 1 || reservation.GuestCount > 16)
            {
                throw new CountException("GuestCount", "Guest count minimum 1 maximum 16");
            }
            if (!Regex.IsMatch(reservation.Phone, @"^\d{10}$"))
                throw new PhoneFormatException("Phone", "Phone by ten digits");
            
            var entity = _mapper.Map<Reservation>(reservation);
            entity.UserId = userId;
            _reservationRepository.Add(entity);
            await _reservationRepository.CommitAsync();
        }

        public async Task DeleteRezerv(int id)
        {
            var table = await _reservationRepository.GetAsync(x => x.Id == id);
            if (table == null)
                throw new EntityNotFoundException("", "Table not found");
            
            _reservationRepository.Delete(table);
            await _reservationRepository.CommitAsync();
        }

        public async Task<List<ReservGetDto>> GetAllReservs(Expression<Func<Reservation, bool>>? func = null, Expression<Func<Reservation, object>>? orderBy = null, bool isOrderByDesting = false, params string[]? includes)
        {
            var queryable = await _reservationRepository.GetAllAsync(func, orderBy, isOrderByDesting, includes);
            return _mapper.Map<List<ReservGetDto>>(queryable);
        }

        public async Task<ReservGetDto> GetReserv(Func<Reservation, bool>? func = null)
        {
            var entity = _reservationRepository.Get(func);
            if (entity == null) throw new EntityNotFoundException("", "Table not Found");

            return _mapper.Map<ReservGetDto>(entity);
        }

        public Task Update(int id, ReservUpdateDto reservation)
        {
            throw new NotImplementedException();
        }
    }
}
