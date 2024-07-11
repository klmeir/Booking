using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Reservations
{
    public record ReservationAddCommand(int HotelId, int RoomId, string CheckInDate, string CheckOutDate, int GuestCount, string EmergencyContactName, string EmergencyContactPhone, List<GuestDto> Guests) : IRequest<ReservationDto>;
}
