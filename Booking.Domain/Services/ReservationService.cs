﻿using Booking.Domain.Entities;
using Booking.Domain.Exception;
using Booking.Domain.Ports;
using System.Linq.Expressions;

namespace Booking.Domain.Services
{
    [DomainService]
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HotelService _hotelService;
        private readonly RoomService _roomService;

        public ReservationService(IReservationRepository reservationRepository, IUnitOfWork unitOfWork, HotelService hotelService, RoomService roomService) =>
            (_reservationRepository, _unitOfWork, _hotelService, _roomService) = (reservationRepository, unitOfWork, hotelService, roomService);

        public async Task<Reservation> SaveReservationAsync(Reservation r, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            await Validations(r, token);

            var returnReservation = await _reservationRepository.SaveReservation(r);
            await _unitOfWork.SaveAsync(token);
            return returnReservation;
        }

        public async Task<IEnumerable<Reservation>> AllReservationAsync(CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            return await _reservationRepository.AllReservation();
        }

        public async Task<Reservation> SingleReservationAsync(int id, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            return await _reservationRepository.SingleReservation(id) ?? throw new NotFoundException("The specified reservation could not be found");
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation r, CancellationToken? cancellationToken = null)
        {
            var token = cancellationToken ?? new CancellationTokenSource().Token;

            await Validations(r, token);

            await _reservationRepository.UpdateReservation(r);
            await _unitOfWork.SaveAsync(token);

            return r;
        }

        public async Task Validations(Reservation r, CancellationToken? cancellationToken = null) 
        {
            await _hotelService.CheckIfExistsHotelAsync(r.HotelId, cancellationToken);
            await _roomService.CheckIfExistsRoomAsync(r.RoomId, cancellationToken);
            await CheckAvailability(r);
        }

        public async Task CheckAvailability(Reservation r)
        {
            Expression<Func<Reservation, bool>> filter = x => x.RoomId == r.RoomId && x.CheckInDate <= r.CheckOutDate && x.CheckOutDate >= r.CheckInDate;
            if (!await _reservationRepository.CheckAvailability(filter)) throw new CoreBusinessException("The specified room is not available in that date range");
        }
    }
}
