using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Reservations
{
    public record ReservationsQuery() : IRequest<List<ReservationDto>>;
}
