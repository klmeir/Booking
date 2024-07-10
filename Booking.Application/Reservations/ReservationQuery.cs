using Booking.Domain.Dtos;
using MediatR;

namespace Booking.Application.Reservations
{
    public record ReservationQuery(int Id) : IRequest<ReservationDto>;
}
