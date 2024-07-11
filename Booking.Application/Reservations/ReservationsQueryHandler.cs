using Booking.Domain.Dtos;
using Booking.Domain.Entities;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Reservations
{
    public class ReservationsQueryHandler : IRequestHandler<ReservationsQuery, List<ReservationDto>>
    {
        private readonly ReservationService _service;
        private readonly string DateFormat = "yyyy-MM-dd";

        public ReservationsQueryHandler(ReservationService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<List<ReservationDto>> Handle(ReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _service.AllReservationAsync(cancellationToken);

            return reservations.Select(MapReservationToDto).ToList();
        }

        private ReservationDto MapReservationToDto(Reservation reservation)
        {
            return new ReservationDto(reservation.Id, reservation.HotelId, reservation.Hotel.Name, reservation.RoomId, reservation.Room.Location, reservation.CheckInDate.ToString(DateFormat), reservation.CheckOutDate.ToString(DateFormat), reservation.GuestCount, reservation.EmergencyContactName, reservation.EmergencyContactPhone,
                reservation.Guests.Select(g => new GuestDto(g.Name, g.Birthdate.ToString(DateFormat), g.Gender, g.DocumentType, g.DocumentNumber, g.Email, g.Phone)).ToList());
        }
    }
}
