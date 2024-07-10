using Booking.Domain.Dtos;
using Booking.Domain.Services;
using MediatR;

namespace Booking.Application.Reservations
{
    public class ReservationQueryHandler : IRequestHandler<ReservationQuery, ReservationDto>
    {
        private readonly ReservationService _service;
        private readonly string DateFormat = "yyyy-MM-dd";

        public ReservationQueryHandler(ReservationService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<ReservationDto> Handle(ReservationQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _service.SingleReservationAsync(request.Id, cancellationToken);

            return new ReservationDto(reservation.Id, reservation.HotelId, reservation.RoomId, reservation.CheckInDate.ToString(DateFormat), reservation.CheckOutDate.ToString(DateFormat), reservation.GuestCount, reservation.EmergencyContactName, reservation.EmergencyContactPhone,
                reservation.Guests.Select(g => new GuestDto(g.Name, g.Birthdate.ToString(DateFormat), g.Gender, g.DocumentType, g.DocumentNumber, g.Email, g.Phone)).ToList());
        }
    }
}
