using AutoMapper;
using RMS.Business.DTOs.ReservationDTOs;
using RMS.Business.DTOs.TableDTOs;
using RMS.Business.Exceptions;
using RMS.Business.Helpers.Email;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using RMS.Business.Helpers.QRCodeGeneratorHelper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RMS.Business.Services.Concretes
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IQRCodeGeneratorHelper _qRCodeGeneratorHelper;
        private readonly IMailService _mailService;
        readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, ITableRepository tableRepository, IMapper mapper, IQRCodeGeneratorHelper qRCodeGeneratorHelper, IMailService mailService)
        {
            _reservationRepository = reservationRepository;
            _qRCodeGeneratorHelper = qRCodeGeneratorHelper;
            _tableRepository = tableRepository;
            _mapper = mapper;
            _mailService = mailService;
        }
        public async Task<IEnumerable<TableGetDto>> GetAvailableTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync(t=>!t.IsDeleted);
            return tables.Select(t => new TableGetDto { Id = t.Id, Name = t.Name, Capacity = t.Capacity, ImageUrl = t.ImageUrl });
        }
        public async Task Create(ReservCreateDto reservation, string userId)
        {

            if (reservation == null) throw new EntityNullReferenceException("", "Reservation null reference");
            if(reservation.ReservationDate == null) throw new EntityNullReferenceException("ReservDate", "Reserv Date null reference");
            if (reservation.ReservationTime == null) throw new EntityNullReferenceException("ReservTime", "Reserv Date null reference");
            if (string.IsNullOrWhiteSpace(reservation.FirstName)) throw new NameFormatException("FirstName", "Name null");
            if (string.IsNullOrWhiteSpace(reservation.LastName)) throw new NameFormatException("LastName", "Surname null");
            
            if (reservation.NumberOfGuests < 1 || reservation.NumberOfGuests > 16)
            {
                throw new CountException("GuestCount", "Guest count minimum 1 maximum 16");
            }
            if (!Regex.IsMatch(reservation.Phone, @"^\d{10}$"))
                throw new PhoneFormatException("Phone", "Phone by ten digits");

            var existingReservations = await _reservationRepository.GetAllAsync(r =>
              r.TableId == reservation.TableId &&
             r.ReservationDate == reservation.ReservationDate
             );

            var conflictingReservations = existingReservations
       .AsEnumerable()
       .Where(r => Math.Abs((r.ReservationTime - reservation.ReservationTime).TotalHours) < 2);


            if (conflictingReservations.Any())
            {
                throw new TimeConflictException("ReservationTime", "The table is already reserved for this time period.");
            }
            var entity = _mapper.Map<Reservation>(reservation);
            entity.UserId = userId;
            var table = await _tableRepository.GetAsync(t => t.Id == reservation.TableId);
            if (table == null) throw new EntityNotFoundException("TableId", "Table not found");
            entity.Table=table;
            table.IsReserved = true;
            string qrText = $"Reservation for {reservation.FirstName} {reservation.LastName} on {reservation.ReservationDate.ToShortDateString()} at {reservation.ReservationTime}. Yours table {table.Name}";
            byte[] qrCode = _qRCodeGeneratorHelper.GenerateQRCode(qrText);
            entity.QrCodeData = Convert.ToBase64String(qrCode);
            
            _reservationRepository.Add(entity);
            await _reservationRepository.CommitAsync();

            

            var mailRequest = new MailRequest
            {
                ToEmail = reservation.Email,
                Subject = "Your Reservation Confirmation",
                Body = "<h1>Salam sizin reservation testiqlenmisdir!</h1>"
            };
            if (_mailService == null)
                throw new InvalidOperationException("MailService is not initialized");
            // Send email with QR code
            await _mailService.SendEmailAsync(mailRequest, qrCode);
        }
       
        public async Task DeleteRezerv(int id)
        {
            var rezerv = await _reservationRepository.GetAsync(x => x.Id == id);
            if (rezerv == null)
                throw new EntityNotFoundException("", "Rezerv not found");
            var table = await _tableRepository.GetAsync(x => x.Id == rezerv.TableId);
            if (table == null)
                throw new EntityNotFoundException("", "Table not found");
            table.IsReserved = false;
            _reservationRepository.Delete(rezerv);
            await _reservationRepository.CommitAsync();
        }

        public async Task<List<ReservGetDto>> GetAllReservs(Expression<Func<Reservation, bool>>? func = null, Expression<Func<Reservation, object>>? orderBy = null, bool isOrderByDesting = false, params string[]? includes)
        {
            var additionalIncludes = new List<string>(includes ?? Array.Empty<string>());
            if (!additionalIncludes.Contains("Table"))
                additionalIncludes.Add("Table");

            var queryable = await _reservationRepository.GetAllAsync(func, orderBy, true, additionalIncludes.ToArray());
            return _mapper.Map<List<ReservGetDto>>(queryable);
        }

        public async Task<ReservGetDto> GetReserv(Func<Reservation, bool>? func = null)
        {
            var entity = _reservationRepository.Get(func);
            if (entity == null) throw new EntityNotFoundException("", "entity not Found");
            return _mapper.Map<ReservGetDto>(entity);
        }

        public async Task Update(int id, ReservUpdateDto reservation)
        {
            var existingReservation = await _reservationRepository.GetAsync(x => x.Id == id);
            if (existingReservation == null)
                throw new EntityNotFoundException("", "Reservation not found");
            if (reservation.NumberOfGuests < 1 || reservation.NumberOfGuests > 16)
            {
                throw new CountException("GuestCount", "Guest count minimum 1 maximum 16");
            }
            existingReservation.NumberOfGuests = reservation.NumberOfGuests;

            if (reservation.ReservationDate!=null)
               existingReservation.ReservationDate = reservation.ReservationDate;

            if(reservation.ReservationTime!=null)
                existingReservation.ReservationTime = reservation.ReservationTime;

            if(reservation.Note !=null)
                existingReservation.Note = reservation.Note;
            
            if (reservation.TableId != null)
            {
                var existingReservations = await _reservationRepository.GetAllAsync(r =>
                r.TableId == reservation.TableId &&
                 r.ReservationDate == reservation.ReservationDate
                 );

                var conflictingReservations = existingReservations
                  .AsEnumerable()
                  .Where(r => Math.Abs((r.ReservationTime - reservation.ReservationTime).TotalHours) < 2);


                if (conflictingReservations.Any())
                {
                    throw new TimeConflictException("ReservationTime", "The table is already reserved for this time period.");
                }

                var table = await _tableRepository.GetAsync(t => t.Id == reservation.TableId);
                if (table == null) throw new EntityNotFoundException("TableId", "Table not found");

                table.IsReserved = true;
                existingReservation.TableId = (int)reservation.TableId;
                existingReservation.Table = table;
                
            }

            string qrText = $"Reservation for {existingReservation.FirstName} {existingReservation.LastName} on {reservation.ReservationDate.ToShortDateString()} at {reservation.ReservationTime}.Yours table {existingReservation.Table.Name}";
            byte[] qrCode = _qRCodeGeneratorHelper.GenerateQRCode(qrText);

            existingReservation.QrCodeData = Convert.ToBase64String(qrCode);

            await _reservationRepository.CommitAsync();
            var mailRequest = new MailRequest
            {
                ToEmail = existingReservation.Email,
                Subject = "Your Reservation Confirmation",
                Body = "<h1>Salam sizin reservation yenilendi!</h1>"
            };

            if (_mailService == null)
                throw new InvalidOperationException("MailService is not initialized");
            
            await _mailService.SendEmailAsync(mailRequest, qrCode);
        }
    }
}
